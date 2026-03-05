using MediatR;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Alarm;

namespace Sphere.Application.Features.Alarms.Queries.GetAlarmHistory;

/// <summary>
/// Query to get alarm history timeline.
/// </summary>
public record GetAlarmHistoryQuery : IRequest<Result<AlarmHistoryResponseDto>>
{
    public string DivSeq { get; init; } = string.Empty;
    public string AlmSysId { get; init; } = string.Empty;
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 50;
}
