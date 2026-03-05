using MediatR;
using Sphere.Application.Common.Models;

namespace Sphere.Application.Features.Auth.Commands.ChangePassword;

/// <summary>
/// Command for changing user password.
/// </summary>
public record ChangePasswordCommand : IRequest<Result>
{
    /// <summary>
    /// User identifier.
    /// </summary>
    public string UserId { get; init; } = string.Empty;

    /// <summary>
    /// Division sequence (partition key).
    /// </summary>
    public string DivSeq { get; init; } = string.Empty;

    /// <summary>
    /// Current password for verification.
    /// </summary>
    public string CurrentPassword { get; init; } = string.Empty;

    /// <summary>
    /// New password to set.
    /// </summary>
    public string NewPassword { get; init; } = string.Empty;

    /// <summary>
    /// Confirmation of new password.
    /// </summary>
    public string ConfirmPassword { get; init; } = string.Empty;
}
