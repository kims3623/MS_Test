using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Models;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Application.Features.Alarms.Commands.EscalateAlarm;

public class EscalateAlarmCommandHandler : IRequestHandler<EscalateAlarmCommand, Result>
{
    private readonly IAlarmRepository _alarmRepository;
    private readonly ILogger<EscalateAlarmCommandHandler> _logger;

    public EscalateAlarmCommandHandler(
        IAlarmRepository alarmRepository,
        ILogger<EscalateAlarmCommandHandler> logger)
    {
        _alarmRepository = alarmRepository;
        _logger = logger;
    }

    public async Task<Result> Handle(EscalateAlarmCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Escalating alarm {AlmSysId} to {EscalateTo} by {UserId}",
            request.AlmSysId, request.EscalateTo, request.UserId);

        var detail = await _alarmRepository.GetDetailAsync(request.DivSeq, request.AlmSysId, cancellationToken);
        if (detail is null)
        {
            return Result.Failure("Alarm not found.");
        }

        // Use update SP to mark as escalated
        var updateRequest = new DTOs.Alarm.UpdateAlarmRequestDto
        {
            DivSeq = request.DivSeq,
            AlmSysId = request.AlmSysId,
            AlmActionId = "ESCALATE",
            UpdateUserId = request.UserId
        };

        var result = await _alarmRepository.UpdateAsync(updateRequest, cancellationToken);

        if (result.Result != "OK" && result.Result != "SUCCESS")
        {
            _logger.LogWarning("Failed to escalate alarm {AlmSysId}: {Message}", request.AlmSysId, result.ResultMessage);
            return Result.Failure(result.ResultMessage ?? "Failed to escalate alarm.");
        }

        _logger.LogInformation("Alarm {AlmSysId} escalated to {EscalateTo}", request.AlmSysId, request.EscalateTo);
        return Result.Success();
    }
}
