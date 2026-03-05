using MediatR;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Alarm;

namespace Sphere.Application.Features.Alarms.Commands.CreateAlarm;

/// <summary>
/// Command to create a new alarm.
/// </summary>
public record CreateAlarmCommand : IRequest<Result<CreateAlarmResponseDto>>
{
    public string DivSeq { get; init; } = string.Empty;
    public string AlmProcId { get; init; } = string.Empty;
    public string Title { get; init; } = string.Empty;
    public string Contents { get; init; } = string.Empty;
    public string VendorId { get; init; } = string.Empty;
    public string MtrlClassId { get; init; } = string.Empty;
    public string? SpecSysId { get; init; }
    public string Severity { get; init; } = "3";
    public string CreateUserId { get; init; } = string.Empty;
}
