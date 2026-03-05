using MediatR;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Reports;

namespace Sphere.Application.Features.Reports.Queries.GetDashboardData;

/// <summary>
/// Query to get aggregated dashboard data.
/// </summary>
public record GetDashboardDataQuery : IRequest<Result<DashboardDataDto>>
{
    /// <summary>
    /// Division sequence from claims.
    /// </summary>
    public string DivSeq { get; init; } = string.Empty;
}
