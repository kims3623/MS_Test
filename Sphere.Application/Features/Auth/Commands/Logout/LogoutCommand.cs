using MediatR;
using Sphere.Application.Common.Models;

namespace Sphere.Application.Features.Auth.Commands.Logout;

/// <summary>
/// Command for user logout.
/// </summary>
public record LogoutCommand : IRequest<Result>
{
    /// <summary>
    /// Current user ID (set from claims).
    /// </summary>
    public string UserId { get; init; } = string.Empty;

    /// <summary>
    /// Division sequence.
    /// </summary>
    public string DivSeq { get; init; } = string.Empty;

    /// <summary>
    /// Optional refresh token to revoke.
    /// </summary>
    public string? RefreshToken { get; init; }
}
