using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Interfaces;
using Sphere.Application.Common.Models;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Application.Features.Auth.Commands.ResetPassword;

/// <summary>
/// Handler for ResetPasswordCommand.
/// </summary>
public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, Result<ResetPasswordResponse>>
{
    private readonly IAuthRepository _authRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ILogger<ResetPasswordCommandHandler> _logger;

    public ResetPasswordCommandHandler(
        IAuthRepository authRepository,
        IPasswordHasher passwordHasher,
        ILogger<ResetPasswordCommandHandler> logger)
    {
        _authRepository = authRepository;
        _passwordHasher = passwordHasher;
        _logger = logger;
    }

    public async Task<Result<ResetPasswordResponse>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Password reset request for user {UserId}", request.UserId);

        var user = await _authRepository.GetUserForAuthAsync(request.UserId, request.DivSeq, cancellationToken);

        // Validate user exists and email matches
        if (user is null || user.UseYn != "Y" ||
            !string.Equals(user.Email, request.Email, StringComparison.OrdinalIgnoreCase))
        {
            _logger.LogWarning("Password reset failed: User {UserId} not found or email mismatch", request.UserId);
            // Return success to prevent user enumeration
            return Result<ResetPasswordResponse>.Success(new ResetPasswordResponse
            {
                Success = true,
                Message = "If the user exists and email matches, a password reset email will be sent."
            });
        }

        var tempPassword = GenerateTemporaryPassword();
        var newHash = _passwordHasher.Hash(tempPassword);

        await _authRepository.ResetAndUnlockUserAsync(request.UserId, request.DivSeq, newHash, cancellationToken);

        // Note: In production, send email with temporary password
        _logger.LogInformation("Password reset successful for user {UserId}. Temporary password generated.", request.UserId);

        return Result<ResetPasswordResponse>.Success(new ResetPasswordResponse
        {
            Success = true,
            Message = "If the user exists and email matches, a password reset email will be sent."
        });
    }

    private static string GenerateTemporaryPassword()
    {
        const string chars = "ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnpqrstuvwxyz23456789!@#$%";
        var random = new Random();
        return new string(Enumerable.Repeat(chars, 12)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
}
