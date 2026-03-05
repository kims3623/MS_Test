using MediatR;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Reports;

namespace Sphere.Application.Features.Reports.Queries.GetHomeAlarmData;

/// <summary>
/// Query to get home alarm data for dashboard.
/// </summary>
public record GetHomeAlarmDataQuery : IRequest<Result<HomeAlarmDataDto>>
{
    /// <summary>
    /// Division sequence from claims.
    /// </summary>
    public string DivSeq { get; init; } = string.Empty;

    /// <summary>
    /// Year for alarm data.
    /// </summary>
    public string Year { get; init; } = string.Empty;

    /// <summary>
    /// Optional vendor type filter.
    /// </summary>
    public string? VendorType { get; init; }
}
