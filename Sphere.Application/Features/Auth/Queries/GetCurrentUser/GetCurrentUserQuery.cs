using MediatR;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Auth;

namespace Sphere.Application.Features.Auth.Queries.GetCurrentUser;

/// <summary>
/// Query to get current authenticated user profile.
/// </summary>
public record GetCurrentUserQuery : IRequest<Result<UserProfileDto>>
{
    /// <summary>
    /// User ID from claims.
    /// </summary>
    public string UserId { get; init; } = string.Empty;

    /// <summary>
    /// Division sequence from claims.
    /// </summary>
    public string DivSeq { get; init; } = string.Empty;
}
