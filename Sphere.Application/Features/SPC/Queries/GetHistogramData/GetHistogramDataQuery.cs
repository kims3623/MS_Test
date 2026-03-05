using MediatR;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.SPC;

namespace Sphere.Application.Features.SPC.Queries.GetHistogramData;

/// <summary>
/// Query to get histogram data.
/// </summary>
public record GetHistogramDataQuery : IRequest<Result<HistogramDataDto>>
{
    public string DivSeq { get; init; } = string.Empty;
    public string SpecSysId { get; init; } = string.Empty;
    public string? StartDate { get; init; }
    public string? EndDate { get; init; }
    public string? Shift { get; init; }
    public int? BinCount { get; init; }
    public decimal? BinWidth { get; init; }
    public bool IncludeNormalCurve { get; init; } = true;
}
