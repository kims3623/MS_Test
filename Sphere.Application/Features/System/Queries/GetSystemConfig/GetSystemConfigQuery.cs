using MediatR;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.System;

namespace Sphere.Application.Features.System.Queries.GetSystemConfig;

/// <summary>
/// Query to get system configuration.
/// </summary>
public record GetSystemConfigQuery : IRequest<Result<SystemConfigDto>>
{
    public string DivSeq { get; init; } = string.Empty;
}
