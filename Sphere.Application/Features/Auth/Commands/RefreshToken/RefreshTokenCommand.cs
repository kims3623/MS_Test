using MediatR;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Auth;

namespace Sphere.Application.Features.Auth.Commands.RefreshToken;

/// <summary>
/// Command for refreshing authentication tokens.
/// </summary>
public record RefreshTokenCommand : IRequest<Result<TokenResponseDto>>
{
    /// <summary>
    /// Current refresh token.
    /// </summary>
    public string RefreshToken { get; init; } = string.Empty;
}
