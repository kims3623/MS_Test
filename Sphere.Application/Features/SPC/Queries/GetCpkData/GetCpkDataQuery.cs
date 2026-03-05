using MediatR;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.SPC;

namespace Sphere.Application.Features.SPC.Queries.GetCpkData;

/// <summary>
/// Query to get Cpk analysis data.
/// </summary>
public record GetCpkDataQuery : IRequest<Result<CpkAnalysisDto>>
{
    public string DivSeq { get; init; } = string.Empty;
    public string SpecSysId { get; init; } = string.Empty;
    public string? StartDate { get; init; }
    public string? EndDate { get; init; }
    public string? Shift { get; init; }
    public int? HistogramBins { get; init; }
    public bool IncludeTrend { get; init; } = true;
}
