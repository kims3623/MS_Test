using MediatR;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.SPC;

namespace Sphere.Application.Features.SPC.Queries.GetDayAnalysis;

/// <summary>
/// Query to get daily analysis data.
/// </summary>
public record GetDayAnalysisQuery : IRequest<Result<DayAnalysisDto>>
{
    public string DivSeq { get; init; } = string.Empty;
    public string SpecSysId { get; init; } = string.Empty;
    public string? StartDate { get; init; }
    public string? EndDate { get; init; }
    public string? Shift { get; init; }
    public bool IncludeShiftBreakdown { get; init; }
}
