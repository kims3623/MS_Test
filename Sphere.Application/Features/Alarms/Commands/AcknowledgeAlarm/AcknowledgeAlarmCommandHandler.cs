using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Models;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Application.Features.Alarms.Commands.AcknowledgeAlarm;

public class AcknowledgeAlarmCommandHandler : IRequestHandler<AcknowledgeAlarmCommand, Result>
{
    private readonly IAlarmRepository _alarmRepository;
    private readonly ILogger<AcknowledgeAlarmCommandHandler> _logger;

    public AcknowledgeAlarmCommandHandler(
        IAlarmRepository alarmRepository,
        ILogger<AcknowledgeAlarmCommandHandler> logger)
    {
        _alarmRepository = alarmRepository;
        _logger = logger;
    }

    public async Task<Result> Handle(AcknowledgeAlarmCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Acknowledging alarm {AlmSysId} by {UserId}", request.AlmSysId, request.UserId);

        var detail = await _alarmRepository.GetDetailAsync(request.DivSeq, request.AlmSysId, cancellationToken);
        if (detail is null)
        {
            return Result.Failure("Alarm not found.");
        }

        // Use update SP to mark as acknowledged
        var updateRequest = new DTOs.Alarm.UpdateAlarmRequestDto
        {
            DivSeq = request.DivSeq,
            AlmSysId = request.AlmSysId,
            AlmActionId = "ACK",
            UpdateUserId = request.UserId
        };

        var result = await _alarmRepository.UpdateAsync(updateRequest, cancellationToken);

        if (result.Result != "OK" && result.Result != "SUCCESS")
        {
            _logger.LogWarning("Failed to acknowledge alarm {AlmSysId}: {Message}", request.AlmSysId, result.ResultMessage);
            return Result.Failure(result.ResultMessage ?? "Failed to acknowledge alarm.");
        }

        _logger.LogInformation("Alarm {AlmSysId} acknowledged by {UserId}", request.AlmSysId, request.UserId);
        return Result.Success();
    }
}
