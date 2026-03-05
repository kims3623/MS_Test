using MediatR;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Alarm;

namespace Sphere.Application.Features.Alarms.Commands.ExecuteAlarmAction;

/// <summary>
/// Command to execute an alarm action (state transition).
/// </summary>
public record ExecuteAlarmActionCommand : IRequest<Result<ExecuteAlarmActionResponseDto>>
{
    public string DivSeq { get; init; } = string.Empty;
    public string AlmSysId { get; init; } = string.Empty;
    public string AlmActionId { get; init; } = string.Empty;
    public string? Comment { get; init; }
    public string UserId { get; init; } = string.Empty;
    public List<string>? NotifyUserIds { get; init; }
}
