using MediatR;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.SPC;

namespace Sphere.Application.Features.SPC.Queries.GetPChart;

/// <summary>
/// Query to get P chart data.
/// </summary>
public record GetPChartQuery : IRequest<Result<PChartDto>>
{
    public string DivSeq { get; init; } = string.Empty;
    public string SpecSysId { get; init; } = string.Empty;
    public string? StartDate { get; init; }
    public string? EndDate { get; init; }
    public string? Shift { get; init; }
}
