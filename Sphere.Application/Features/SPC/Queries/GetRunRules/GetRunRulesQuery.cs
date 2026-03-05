using MediatR;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.SPC;

namespace Sphere.Application.Features.SPC.Queries.GetRunRules;

/// <summary>
/// Query to get run rules configuration for a spec.
/// </summary>
public record GetRunRulesQuery : IRequest<Result<RunRulesConfigDto>>
{
    public string SpecSysId { get; init; } = string.Empty;
}
