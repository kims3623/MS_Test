using MediatR;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Alarm;

namespace Sphere.Application.Features.Alarms.Queries.GetAlarmActions;

/// <summary>
/// Query to get alarm actions for a specific alarm.
/// </summary>
public record GetAlarmActionsQuery : IRequest<Result<List<AlarmActionDto>>>
{
    /// <summary>
    /// Alarm system ID.
    /// </summary>
    public string AlarmSysId { get; init; } = string.Empty;

    /// <summary>
    /// Division sequence.
    /// </summary>
    public string DivSeq { get; init; } = string.Empty;
}
