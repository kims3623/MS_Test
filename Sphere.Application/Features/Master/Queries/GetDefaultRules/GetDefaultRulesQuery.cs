using MediatR;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Master;

namespace Sphere.Application.Features.Master.Queries.GetDefaultRules;

public record GetDefaultRulesQuery : IRequest<Result<DefaultRuleListDto>>
{
    public string DivSeq { get; init; } = string.Empty;
    public string? RuleType { get; init; }
    public string? TargetType { get; init; }
    public string? UseYn { get; init; }
    public string? SearchText { get; init; }
}
