using MediatR;
using Sphere.Application.Common.Models;

namespace Sphere.Application.Features.Auth.Commands.ResetPassword;

/// <summary>
/// Command to reset user password.
/// </summary>
public class ResetPasswordCommand : IRequest<Result<ResetPasswordResponse>>
{
    public string UserId { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string DivSeq { get; set; } = string.Empty;
}

/// <summary>
/// Response for password reset.
/// </summary>
public class ResetPasswordResponse
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
}
