using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Alarm;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Application.Features.Alarms.Commands.UpdateAlarm;

/// <summary>
/// Handler for UpdateAlarmCommand.
/// </summary>
public class UpdateAlarmCommandHandler : IRequestHandler<UpdateAlarmCommand, Result<UpdateAlarmResponseDto>>
{
    private readonly IAlarmRepository _alarmRepository;
    private readonly ILogger<UpdateAlarmCommandHandler> _logger;

    public UpdateAlarmCommandHandler(
        IAlarmRepository alarmRepository,
        ILogger<UpdateAlarmCommandHandler> logger)
    {
        _alarmRepository = alarmRepository;
        _logger = logger;
    }

    public async Task<Result<UpdateAlarmResponseDto>> Handle(UpdateAlarmCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Updating alarm AlmSysId={AlmSysId}",
            request.AlmSysId);

        try
        {
            var updateRequest = new UpdateAlarmRequestDto
            {
                AlmSysId = request.AlmSysId,
                DivSeq = request.DivSeq,
                Title = request.Title,
                Contents = request.Contents,
                AlmActionId = request.AlmActionId,
                Severity = request.Severity,
                UpdateUserId = request.UpdateUserId
            };

            var result = await _alarmRepository.UpdateAsync(updateRequest, cancellationToken);

            if (result.Result != "OK" && result.Result != "SUCCESS")
            {
                _logger.LogWarning("Failed to update alarm: {Message}", result.ResultMessage);
                return Result<UpdateAlarmResponseDto>.Failure(result.ResultMessage);
            }

            _logger.LogInformation("Alarm updated successfully: AlmSysId={AlmSysId}", result.AlmSysId);
            return Result<UpdateAlarmResponseDto>.Success(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating alarm {AlmSysId}", request.AlmSysId);
            return Result<UpdateAlarmResponseDto>.Failure($"알람 수정 중 오류가 발생했습니다: {ex.Message}");
        }
    }
}
