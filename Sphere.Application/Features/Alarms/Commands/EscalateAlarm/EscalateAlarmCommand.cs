using MediatR;
using Sphere.Application.Common.Models;

namespace Sphere.Application.Features.Alarms.Commands.EscalateAlarm;

/// <summary>
/// Command for escalating an alarm to a supervisor.
/// </summary>
public record EscalateAlarmCommand : IRequest<Result>
{
    public string DivSeq { get; init; } = string.Empty;
    public string AlmSysId { get; init; } = string.Empty;
    public string UserId { get; init; } = string.Empty;
    public string? EscalateTo { get; init; }
    public string? Reason { get; init; }
}
