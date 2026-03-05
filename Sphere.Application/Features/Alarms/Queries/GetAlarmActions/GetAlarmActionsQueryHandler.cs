using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Alarm;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Application.Features.Alarms.Queries.GetAlarmActions;

/// <summary>
/// Handler for GetAlarmActionsQuery.
/// </summary>
public class GetAlarmActionsQueryHandler : IRequestHandler<GetAlarmActionsQuery, Result<List<AlarmActionDto>>>
{
    private readonly IAlarmRepository _alarmRepository;
    private readonly ILogger<GetAlarmActionsQueryHandler> _logger;

    public GetAlarmActionsQueryHandler(
        IAlarmRepository alarmRepository,
        ILogger<GetAlarmActionsQueryHandler> logger)
    {
        _alarmRepository = alarmRepository;
        _logger = logger;
    }

    public async Task<Result<List<AlarmActionDto>>> Handle(GetAlarmActionsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Fetching alarm actions for AlarmSysId={AlarmSysId}",
            request.AlarmSysId);

        try
        {
            var actions = await _alarmRepository.GetAlarmActionsAsync(
                request.DivSeq,
                request.AlarmSysId,
                cancellationToken);

            if (actions == null || !actions.Any())
            {
                _logger.LogWarning("No actions found for alarm {AlarmSysId}", request.AlarmSysId);
                return Result<List<AlarmActionDto>>.Success(new List<AlarmActionDto>());
            }

            return Result<List<AlarmActionDto>>.Success(actions.ToList());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching alarm actions for {AlarmSysId}", request.AlarmSysId);
            return Result<List<AlarmActionDto>>.Failure($"알람 액션 조회 중 오류가 발생했습니다: {ex.Message}");
        }
    }
}
