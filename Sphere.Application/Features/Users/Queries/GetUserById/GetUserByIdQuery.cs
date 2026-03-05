using MediatR;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Auth;

namespace Sphere.Application.Features.Users.Queries.GetUserById;

/// <summary>
/// Query to get user profile by ID.
/// </summary>
public record GetUserByIdQuery : IRequest<Result<UserProfileDto>>
{
    /// <summary>
    /// User identifier.
    /// </summary>
    public string UserId { get; init; } = string.Empty;

    /// <summary>
    /// Division sequence.
    /// </summary>
    public string DivSeq { get; init; } = string.Empty;
}
