using MediatR;
using Sphere.Application.Common.Models;

namespace Sphere.Application.Features.SPC.Commands.UpdateControlLimits;

/// <summary>
/// Command to update control limits for a spec.
/// </summary>
public record UpdateControlLimitsCommand : IRequest<Result<bool>>
{
    public string UserId { get; init; } = string.Empty;
    public string SpecSysId { get; init; } = string.Empty;
    public string ChartType { get; init; } = string.Empty;
    public decimal Ucl { get; init; }
    public decimal Cl { get; init; }
    public decimal Lcl { get; init; }
    public decimal? Usl { get; init; }
    public decimal? Lsl { get; init; }
    public decimal? Target { get; init; }
    public string? Reason { get; init; }
}
