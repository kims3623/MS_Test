using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Alarm;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Application.Features.Alarms.Commands.CloseAlarm;

/// <summary>
/// Handler for CloseAlarmCommand.
/// </summary>
public class CloseAlarmCommandHandler : IRequestHandler<CloseAlarmCommand, Result<CloseAlarmResponseDto>>
{
    private readonly IAlarmRepository _alarmRepository;
    private readonly ILogger<CloseAlarmCommandHandler> _logger;

    public CloseAlarmCommandHandler(
        IAlarmRepository alarmRepository,
        ILogger<CloseAlarmCommandHandler> logger)
    {
        _alarmRepository = alarmRepository;
        _logger = logger;
    }

    public async Task<Result<CloseAlarmResponseDto>> Handle(CloseAlarmCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Closing alarm {AlarmSysId} with action {ActionId} by user {UserId}",
            request.AlarmSysId, request.ActionId, request.UserId);

        try
        {
            // Close the alarm using repository (which calls stored procedure)
            var result = await _alarmRepository.CloseAlarmAsync(
                request.DivSeq,
                request.AlarmSysId,
                request.ActionId,
                request.StopReason,
                request.UserId,
                request.CustomerIds,
                cancellationToken);

            if (!result.Success)
            {
                _logger.LogWarning(
                    "Failed to close alarm {AlarmSysId}: {Message}",
                    request.AlarmSysId, result.Message);

                return Result<CloseAlarmResponseDto>.Failure(result.Message);
            }

            var response = new CloseAlarmResponseDto
            {
                Success = true,
                Message = "알람이 성공적으로 중지되었습니다.",
                AlarmSysId = request.AlarmSysId,
                NewStatus = result.NewStatus ?? "Closed"
            };

            _logger.LogInformation(
                "Alarm {AlarmSysId} closed successfully",
                request.AlarmSysId);

            return Result<CloseAlarmResponseDto>.Success(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error closing alarm {AlarmSysId}", request.AlarmSysId);
            return Result<CloseAlarmResponseDto>.Failure($"알람 중지 중 오류가 발생했습니다: {ex.Message}");
        }
    }
}
