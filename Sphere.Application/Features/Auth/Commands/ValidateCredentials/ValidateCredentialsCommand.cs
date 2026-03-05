using MediatR;
using Sphere.Application.Common.Models;

namespace Sphere.Application.Features.Auth.Commands.ValidateCredentials;

/// <summary>
/// Command to validate user credentials without logging in.
/// </summary>
public class ValidateCredentialsCommand : IRequest<Result<ValidateCredentialsResponse>>
{
    public string UserId { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string DivSeq { get; set; } = string.Empty;
}

/// <summary>
/// Response for credentials validation.
/// </summary>
public class ValidateCredentialsResponse
{
    public bool IsValid { get; set; }
    public string? FailureReason { get; set; }
    public bool AccountLocked { get; set; }
    public int FailedAttempts { get; set; }
    public bool PasswordExpired { get; set; }
    public bool RequiresPasswordChange { get; set; }
}
