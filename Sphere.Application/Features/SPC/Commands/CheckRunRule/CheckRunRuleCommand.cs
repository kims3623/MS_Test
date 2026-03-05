using MediatR;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.SPC;

namespace Sphere.Application.Features.SPC.Commands.CheckRunRule;

/// <summary>
/// Command to check run rule violations.
/// </summary>
public record CheckRunRuleCommand : IRequest<Result<RunRuleCheckResponseDto>>
{
    public string DivSeq { get; init; } = string.Empty;
    public string SpecSysId { get; init; } = string.Empty;
    public string? StartDate { get; init; }
    public string? EndDate { get; init; }
    public string? Shift { get; init; }
    public List<string>? RuleIds { get; init; }
}
