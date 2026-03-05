using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Interfaces;
using Sphere.Application.Common.Models;

namespace Sphere.Application.Features.Auth.Commands.ValidateCredentials;

/// <summary>
/// Handler for ValidateCredentialsCommand.
/// </summary>
public class ValidateCredentialsCommandHandler : IRequestHandler<ValidateCredentialsCommand, Result<ValidateCredentialsResponse>>
{
    private readonly IApplicationDbContext _context;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ILogger<ValidateCredentialsCommandHandler> _logger;

    public ValidateCredentialsCommandHandler(
        IApplicationDbContext context,
        IPasswordHasher passwordHasher,
        ILogger<ValidateCredentialsCommandHandler> logger)
    {
        _context = context;
        _passwordHasher = passwordHasher;
        _logger = logger;
    }

    public async Task<Result<ValidateCredentialsResponse>> Handle(ValidateCredentialsCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Validating credentials for user {UserId}", request.UserId);

        // 1. Find user (password is stored in UserInfo table)
        var user = await _context.UserInfos
            .Where(u => u.DivSeq == request.DivSeq && u.UserId == request.UserId && u.UseYn == "Y")
            .FirstOrDefaultAsync(cancellationToken);

        if (user is null)
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

        // 2. Check if account is locked
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

        // 3. Verify password (from UserInfo table)
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

        // 4. Password expiration not supported in current DB schema
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
