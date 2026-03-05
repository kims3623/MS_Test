using MediatR;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Reports;

namespace Sphere.Application.Features.Reports.Queries.GetHomeIssueData;

/// <summary>
/// Query to get home issue data for dashboard.
/// </summary>
public record GetHomeIssueDataQuery : IRequest<Result<List<HomeIssueDataDto>>>
{
    /// <summary>
    /// Division sequence from claims.
    /// </summary>
    public string DivSeq { get; init; } = string.Empty;

    /// <summary>
    /// Optional vendor type filter.
    /// </summary>
    public string? VendorType { get; init; }

    /// <summary>
    /// Optional stat type ID filter.
    /// </summary>
    public string? StatTypeId { get; init; }

    /// <summary>
    /// Optional vendor ID filter.
    /// </summary>
    public string? VendorId { get; init; }
}
