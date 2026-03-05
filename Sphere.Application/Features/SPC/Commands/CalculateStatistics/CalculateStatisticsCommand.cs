using MediatR;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.SPC;

namespace Sphere.Application.Features.SPC.Commands.CalculateStatistics;

/// <summary>
/// Command to calculate statistics for SPC data.
/// </summary>
public record CalculateStatisticsCommand : IRequest<Result<StatisticsCalcResultDto>>
{
    public string DivSeq { get; init; } = string.Empty;
    public string SpecSysId { get; init; } = string.Empty;
    public string? StartDate { get; init; }
    public string? EndDate { get; init; }
    public string? Shift { get; init; }
    public string StatType { get; init; } = string.Empty;
}
