using MediatR;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.System;

namespace Sphere.Application.Features.System.Queries.GetSecurityPolicy;

/// <summary>
/// Query to get security policy.
/// </summary>
public record GetSecurityPolicyQuery : IRequest<Result<SecurityPolicyDto>>
{
    public string DivSeq { get; init; } = string.Empty;
}
