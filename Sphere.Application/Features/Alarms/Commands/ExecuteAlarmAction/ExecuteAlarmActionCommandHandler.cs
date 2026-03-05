using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Alarm;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Application.Features.Alarms.Commands.ExecuteAlarmAction;

/// <summary>
/// Handler for ExecuteAlarmActionCommand.
/// </summary>
public class ExecuteAlarmActionCommandHandler : IRequestHandler<ExecuteAlarmActionCommand, Result<ExecuteAlarmActionResponseDto>>
{
    private readonly IAlarmRepository _alarmRepository;
    private readonly ILogger<ExecuteAlarmActionCommandHandler> _logger;

    public ExecuteAlarmActionCommandHandler(
        IAlarmRepository alarmRepository,
        ILogger<ExecuteAlarmActionCommandHandler> logger)
    {
        _alarmRepository = alarmRepository;
        _logger = logger;
    }

    public async Task<Result<ExecuteAlarmActionResponseDto>> Handle(ExecuteAlarmActionCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Executing alarm action AlmSysId={AlmSysId}, ActionId={ActionId}, UserId={UserId}",
            request.AlmSysId, request.AlmActionId, request.UserId);

        try
        {
            var executeRequest = new ExecuteAlarmActionRequestDto
            {
                DivSeq = request.DivSeq,
                AlmSysId = request.AlmSysId,
                AlmActionId = request.AlmActionId,
                Comment = request.Comment,
                UserId = request.UserId,
                NotifyUserIds = request.NotifyUserIds
            };

            var result = await _alarmRepository.ExecuteActionAsync(executeRequest, cancellationToken);

            if (result.Result != "OK" && result.Result != "SUCCESS")
            {
                _logger.LogWarning("Failed to execute alarm action: {Message}", result.ResultMessage);
                return Result<ExecuteAlarmActionResponseDto>.Failure(result.ResultMessage);
            }

            _logger.LogInformation(
                "Alarm action executed successfully: AlmSysId={AlmSysId}, NewActionId={NewActionId}",
                result.AlmSysId, result.NewActionId);
            return Result<ExecuteAlarmActionResponseDto>.Success(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error executing alarm action for {AlmSysId}", request.AlmSysId);
            return Result<ExecuteAlarmActionResponseDto>.Failure($"알람 액션 실행 중 오류가 발생했습니다: {ex.Message}");
        }
    }
}
