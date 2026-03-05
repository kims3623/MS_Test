using MediatR;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.System;

namespace Sphere.Application.Features.System.Commands.UpdateSystemConfig;

/// <summary>
/// Command to update system configuration.
/// </summary>
public record UpdateSystemConfigCommand : IRequest<Result<UpdateSystemConfigResponseDto>>
{
    public string DivSeq { get; init; } = string.Empty;
    public List<SystemConfigUpdateItemDto> Items { get; init; } = new();
    public string UpdateUserId { get; init; } = string.Empty;
}
