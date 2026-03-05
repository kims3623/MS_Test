using MediatR;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Alarm;

namespace Sphere.Application.Features.Alarms.Commands.CloseAlarm;

/// <summary>
/// Command for closing/stopping an alarm.
/// </summary>
public record CloseAlarmCommand : IRequest<Result<CloseAlarmResponseDto>>
{
    /// <summary>
    /// Alarm system ID.
    /// </summary>
    public string AlarmSysId { get; init; } = string.Empty;

    /// <summary>
    /// Division sequence.
    /// </summary>
    public string DivSeq { get; init; } = string.Empty;

    /// <summary>
    /// Action ID for the close action.
    /// </summary>
    public string ActionId { get; init; } = string.Empty;

    /// <summary>
    /// Reason for stopping the alarm.
    /// </summary>
    public string StopReason { get; init; } = string.Empty;

    /// <summary>
    /// Customer IDs to notify (optional).
    /// </summary>
    public List<string>? CustomerIds { get; init; }

    /// <summary>
    /// User ID performing the action.
    /// </summary>
    public string UserId { get; init; } = string.Empty;
}
