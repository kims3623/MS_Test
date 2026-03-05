using MediatR;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Alarm;

namespace Sphere.Application.Features.Alarms.Commands.UpdateAlarm;

/// <summary>
/// Command to update an existing alarm.
/// </summary>
public record UpdateAlarmCommand : IRequest<Result<UpdateAlarmResponseDto>>
{
    public string AlmSysId { get; init; } = string.Empty;
    public string DivSeq { get; init; } = string.Empty;
    public string? Title { get; init; }
    public string? Contents { get; init; }
    public string? AlmActionId { get; init; }
    public string? Severity { get; init; }
    public string UpdateUserId { get; init; } = string.Empty;
}
