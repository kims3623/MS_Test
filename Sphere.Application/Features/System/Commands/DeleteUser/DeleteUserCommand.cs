using MediatR;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.System;

namespace Sphere.Application.Features.System.Commands.DeleteUser;

/// <summary>
/// Command to delete a user.
/// </summary>
public record DeleteUserCommand : IRequest<Result<DeleteUserResponseDto>>
{
    public string DivSeq { get; init; } = string.Empty;
    public string UserId { get; init; } = string.Empty;
    public string DeleteUserId { get; init; } = string.Empty;
}
