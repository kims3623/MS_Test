using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Alarm;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Application.Features.Alarms.Queries.GetAlarmAttachments;

/// <summary>
/// Handler for GetAlarmAttachmentsQuery.
/// </summary>
public class GetAlarmAttachmentsQueryHandler : IRequestHandler<GetAlarmAttachmentsQuery, Result<AlarmAttachmentListResponseDto>>
{
    private readonly IAlarmRepository _alarmRepository;
    private readonly ILogger<GetAlarmAttachmentsQueryHandler> _logger;

    public GetAlarmAttachmentsQueryHandler(
        IAlarmRepository alarmRepository,
        ILogger<GetAlarmAttachmentsQueryHandler> logger)
    {
        _alarmRepository = alarmRepository;
        _logger = logger;
    }

    public async Task<Result<AlarmAttachmentListResponseDto>> Handle(GetAlarmAttachmentsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Fetching attachments for alarm AlmSysId={AlmSysId}",
            request.AlmSysId);

        try
        {
            var query = new AlarmAttachmentQueryDto
            {
                DivSeq = request.DivSeq,
                AlmSysId = request.AlmSysId
            };

            var attachments = await _alarmRepository.GetAttachmentsAsync(query, cancellationToken);

            return Result<AlarmAttachmentListResponseDto>.Success(attachments);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching attachments for alarm {AlmSysId}", request.AlmSysId);
            return Result<AlarmAttachmentListResponseDto>.Failure($"첨부파일 조회 중 오류가 발생했습니다: {ex.Message}");
        }
    }
}
