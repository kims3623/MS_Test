using MediatR;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.SPC;

namespace Sphere.Application.Features.SPC.Queries.GetMonthAnalysis;

/// <summary>
/// Query to get monthly analysis data.
/// </summary>
public record GetMonthAnalysisQuery : IRequest<Result<MonthAnalysisDto>>
{
    public string DivSeq { get; init; } = string.Empty;
    public string SpecSysId { get; init; } = string.Empty;
    public string? Year { get; init; }
    public bool IncludeYearComparison { get; init; }
}
