using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Interfaces;
using Sphere.Application.Common.Models;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Application.Features.Auth.Commands.ValidateCredentials;

/// <summary>
/// Handler for ValidateCredentialsCommand.
/// </summary>
public class ValidateCredentialsCommandHandler : IRequestHandler<ValidateCredentialsCommand, Result<ValidateCredentialsResponse>>
{
    private readonly IAuthRepository _authRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ILogger<ValidateCredentialsCommandHandler> _logger;

    public ValidateCredentialsCommandHandler(
        IAuthRepository authRepository,
        IPasswordHasher passwordHasher,
        ILogger<ValidateCredentialsCommandHandler> logger)
    {
        _authRepository = authRepository;
        _passwordHasher = passwordHasher;
        _logger = logger;
    }

    public async Task<Result<ValidateCredentialsResponse>> Handle(ValidateCredentialsCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Validating credentials for user {UserId}", request.UserId);

        var user = await _authRepository.GetUserForAuthAsync(request.UserId, request.DivSeq, cancellationToken);

        if (user is null || user.UseYn != "Y")
        {
            _logger.LogWarning("Credentials validation failed: User {UserId} not found", request.UserId);
            return Result<ValidateCredentialsResponse>.Success(new ValidateCredentialsResponse
            {
                IsValid = false,
                FailureReason = "Invalid user ID or password.",
                AccountLocked = false,
                FailedAttempts = 0,
                PasswordExpired = false,
                RequiresPasswordChange = false
            });
        }

        if (user.IsLocked == "Y")
        {
            _logger.LogWarning("Credentials validation: User {UserId} account is locked", request.UserId);
            return Result<ValidateCredentialsResponse>.Success(new ValidateCredentialsResponse
            {
                IsValid = false,
                FailureReason = "Account is locked.",
                AccountLocked = true,
                FailedAttempts = user.FailCount,
                PasswordExpired = false,
                RequiresPasswordChange = false
            });
        }

        var isPasswordValid = _passwordHasher.Verify(request.Password, user.PasswordHash ?? string.Empty);

        if (!isPasswordValid)
        {
            _logger.LogWarning("Credentials validation failed: Invalid password for user {UserId}", request.UserId);
            return Result<ValidateCredentialsResponse>.Success(new ValidateCredentialsResponse
            {
                IsValid = false,
                FailureReason = "Invalid user ID or password.",
                AccountLocked = false,
                FailedAttempts = user.FailCount,
                PasswordExpired = false,
                RequiresPasswordChange = false
            });
        }

        _logger.LogInformation("Credentials validated successfully for user {UserId}", request.UserId);

        return Result<ValidateCredentialsResponse>.Success(new ValidateCredentialsResponse
        {
            IsValid = true,
            FailureReason = null,
            AccountLocked = false,
            FailedAttempts = 0,
            PasswordExpired = false,
            RequiresPasswordChange = false
        });
    }
}
