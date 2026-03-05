using MediatR;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Alarm;

namespace Sphere.Application.Features.Alarms.Queries.GetAlarmStatistics;

/// <summary>
/// Query to get alarm statistics summary.
/// </summary>
public record GetAlarmStatisticsQuery : IRequest<Result<AlarmStatisticsDto>>
{
    public string DivSeq { get; init; } = string.Empty;
    public string? StartDate { get; init; }
    public string? EndDate { get; init; }
    public string? VendorId { get; init; }
    public string? MtrlClassId { get; init; }
}
