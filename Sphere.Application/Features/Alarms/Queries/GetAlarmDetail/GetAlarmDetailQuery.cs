using MediatR;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Alarm;

namespace Sphere.Application.Features.Alarms.Queries.GetAlarmDetail;

/// <summary>
/// Query to get alarm detail by ID.
/// </summary>
public record GetAlarmDetailQuery : IRequest<Result<AlarmDetailDto>>
{
    public string DivSeq { get; init; } = string.Empty;
    public string AlmSysId { get; init; } = string.Empty;
}
