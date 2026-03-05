using MediatR;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Reports;

namespace Sphere.Application.Features.Reports.Queries.GetStatisticsReport;

/// <summary>
/// Query to get statistics report data.
/// </summary>
public record GetStatisticsReportQuery : IRequest<Result<StatisticsReportDto>>
{
    /// <summary>
    /// Division sequence from claims.
    /// </summary>
    public string DivSeq { get; init; } = string.Empty;

    /// <summary>
    /// Start date filter (yyyy-MM-dd).
    /// </summary>
    public string? StartDate { get; init; }

    /// <summary>
    /// End date filter (yyyy-MM-dd).
    /// </summary>
    public string? EndDate { get; init; }

    /// <summary>
    /// Report type filter.
    /// </summary>
    public string? ReportType { get; init; }

    /// <summary>
    /// Optional category ID filter.
    /// </summary>
    public string? CategoryId { get; init; }

    /// <summary>
    /// Optional vendor ID filter.
    /// </summary>
    public string? VendorId { get; init; }

    /// <summary>
    /// Group by option.
    /// </summary>
    public string? GroupBy { get; init; }
}
