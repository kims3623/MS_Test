using MediatR;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.SPC;

namespace Sphere.Application.Features.SPC.Queries.GetChartData;

/// <summary>
/// Query to get SPC chart data.
/// </summary>
public record GetChartDataQuery : IRequest<Result<IEnumerable<ChartDataResultDto>>>
{
    public string DivSeq { get; init; } = string.Empty;
    public string? VendorId { get; init; }
    public string? MtrlClassId { get; init; }
    public string? ProjectId { get; init; }
    public string? SpecSysId { get; init; }
    public string? StartDate { get; init; }
    public string? EndDate { get; init; }
    public string? Shift { get; init; }
    public string ChartType { get; init; } = string.Empty;
}
