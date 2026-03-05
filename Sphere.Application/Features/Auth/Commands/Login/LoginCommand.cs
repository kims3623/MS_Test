using MediatR;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Auth;

namespace Sphere.Application.Features.Auth.Commands.Login;

/// <summary>
/// Command for user login authentication.
/// </summary>
public record LoginCommand : IRequest<Result<LoginResponseDto>>
{
    /// <summary>
    /// User identifier.
    /// </summary>
    public string UserId { get; init; } = string.Empty;

    /// <summary>
    /// User password.
    /// </summary>
    public string Password { get; init; } = string.Empty;

    /// <summary>
    /// Division sequence (partition key).
    /// </summary>
    public string DivSeq { get; init; } = string.Empty;
}
