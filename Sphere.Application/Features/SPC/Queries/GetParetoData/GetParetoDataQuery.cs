using MediatR;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.SPC;

namespace Sphere.Application.Features.SPC.Queries.GetParetoData;

/// <summary>
/// Query to get Pareto diagram data.
/// </summary>
public record GetParetoDataQuery : IRequest<Result<ParetoDataDto>>
{
    public string DivSeq { get; init; } = string.Empty;
    public string SpecSysId { get; init; } = string.Empty;
    public string? StartDate { get; init; }
    public string? EndDate { get; init; }
    public string? Shift { get; init; }
    public string AnalysisType { get; init; } = "defect";
    public int? TopN { get; init; }
    public bool GroupOthers { get; init; } = true;
}
