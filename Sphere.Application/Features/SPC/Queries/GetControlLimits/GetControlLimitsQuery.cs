using MediatR;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.SPC;

namespace Sphere.Application.Features.SPC.Queries.GetControlLimits;

/// <summary>
/// Query to get control limits for a spec.
/// </summary>
public record GetControlLimitsQuery : IRequest<Result<ControlLimitsResponseDto>>
{
    public string SpecSysId { get; init; } = string.Empty;
    public string ChartType { get; init; } = string.Empty;
}
