using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Alarm;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Application.Features.Alarms.Commands.UploadAlarmAttachment;

/// <summary>
/// Handler for UploadAlarmAttachmentCommand.
/// </summary>
public class UploadAlarmAttachmentCommandHandler : IRequestHandler<UploadAlarmAttachmentCommand, Result<UploadAlarmAttachmentResponseDto>>
{
    private readonly IAlarmRepository _alarmRepository;
    private readonly ILogger<UploadAlarmAttachmentCommandHandler> _logger;

    public UploadAlarmAttachmentCommandHandler(
        IAlarmRepository alarmRepository,
        ILogger<UploadAlarmAttachmentCommandHandler> logger)
    {
        _alarmRepository = alarmRepository;
        _logger = logger;
    }

    public async Task<Result<UploadAlarmAttachmentResponseDto>> Handle(UploadAlarmAttachmentCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Uploading attachment to alarm AlmSysId={AlmSysId}, FileName={FileName}, Size={Size}",
            request.AlmSysId, request.OriginalFileName, request.FileSize);

        try
        {
            var uploadRequest = new UploadAlarmAttachmentRequestDto
            {
                DivSeq = request.DivSeq,
                AlmSysId = request.AlmSysId,
                FileName = request.FileName,
                OriginalFileName = request.OriginalFileName,
                FileSize = request.FileSize,
                FileType = request.FileType,
                MimeType = request.MimeType,
                FileContent = request.FileContent,
                CreateUserId = request.CreateUserId
            };

            var result = await _alarmRepository.UploadAttachmentAsync(uploadRequest, cancellationToken);

            if (result.Result != "OK" && result.Result != "SUCCESS")
            {
                _logger.LogWarning("Failed to upload attachment: {Message}", result.ResultMessage);
                return Result<UploadAlarmAttachmentResponseDto>.Failure(result.ResultMessage);
            }

            _logger.LogInformation(
                "Attachment uploaded successfully: AttachSeq={AttachSeq}",
                result.AttachSeq);
            return Result<UploadAlarmAttachmentResponseDto>.Success(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error uploading attachment to alarm {AlmSysId}", request.AlmSysId);
            return Result<UploadAlarmAttachmentResponseDto>.Failure($"첨부파일 업로드 중 오류가 발생했습니다: {ex.Message}");
        }
    }
}
