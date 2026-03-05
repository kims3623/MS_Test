using MediatR;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Alarm;

namespace Sphere.Application.Features.Alarms.Queries.GetAlarmTrend;

/// <summary>
/// Query to get alarm trend data over time.
/// </summary>
public record GetAlarmTrendQuery : IRequest<Result<AlarmTrendResponseDto>>
{
    public string DivSeq { get; init; } = string.Empty;
    public string StartDate { get; init; } = string.Empty;
    public string EndDate { get; init; } = string.Empty;
    public string? VendorId { get; init; }
    public string? MtrlClassId { get; init; }
    public string GroupBy { get; init; } = "DAY"; // DAY, WEEK, MONTH
}
