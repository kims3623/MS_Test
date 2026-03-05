using MediatR;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.SPC;

namespace Sphere.Application.Features.SPC.Queries.GetChartConfig;

/// <summary>
/// Query to get chart configuration.
/// </summary>
public record GetChartConfigQuery : IRequest<Result<ChartConfigDto>>
{
    public string UserId { get; init; } = string.Empty;
    public string ChartType { get; init; } = "xbar-r";
}
