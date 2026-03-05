using MediatR;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.SPC;

namespace Sphere.Application.Features.SPC.Queries.GetTrendAnalysis;

/// <summary>
/// Query to get trend analysis data.
/// </summary>
public record GetTrendAnalysisQuery : IRequest<Result<TrendAnalysisDto>>
{
    public string DivSeq { get; init; } = string.Empty;
    public string SpecSysId { get; init; } = string.Empty;
    public string? StartDate { get; init; }
    public string? EndDate { get; init; }
    public string GroupBy { get; init; } = "day";
    public int MovingAvgWindow { get; init; } = 7;
    public bool IncludeForecast { get; init; }
    public int ForecastPeriods { get; init; } = 7;
}
