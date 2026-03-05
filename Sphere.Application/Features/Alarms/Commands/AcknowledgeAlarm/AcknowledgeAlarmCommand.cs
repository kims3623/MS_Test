using MediatR;
using Sphere.Application.Common.Models;

namespace Sphere.Application.Features.Alarms.Commands.AcknowledgeAlarm;

/// <summary>
/// Command for acknowledging an alarm (confirming receipt).
/// </summary>
public record AcknowledgeAlarmCommand : IRequest<Result>
{
    public string DivSeq { get; init; } = string.Empty;
    public string AlmSysId { get; init; } = string.Empty;
    public string UserId { get; init; } = string.Empty;
    public string? Comment { get; init; }
}
