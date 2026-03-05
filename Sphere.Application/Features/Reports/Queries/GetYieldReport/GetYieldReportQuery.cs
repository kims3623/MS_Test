using MediatR;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Reports;

namespace Sphere.Application.Features.Reports.Queries.GetYieldReport;

/// <summary>
/// Query to get yield report data.
/// </summary>
public record GetYieldReportQuery : IRequest<Result<YieldReportDto>>
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
    /// Optional vendor ID filter.
    /// </summary>
    public string? VendorId { get; init; }

    /// <summary>
    /// Optional material class ID filter.
    /// </summary>
    public string? MtrlClassId { get; init; }

    /// <summary>
    /// Optional spec ID filter.
    /// </summary>
    public string? SpecId { get; init; }

    /// <summary>
    /// Group by option (day, week, month).
    /// </summary>
    public string? GroupBy { get; init; }
}
