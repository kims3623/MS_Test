using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Interfaces;
using Sphere.Application.Common.Models;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Application.Features.Auth.Commands.ChangePassword;

/// <summary>
/// Handler for ChangePasswordCommand.
/// </summary>
public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Result>
{
    private readonly IAuthRepository _authRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ILogger<ChangePasswordCommandHandler> _logger;

    public ChangePasswordCommandHandler(
        IAuthRepository authRepository,
        IPasswordHasher passwordHasher,
        ILogger<ChangePasswordCommandHandler> logger)
    {
        _authRepository = authRepository;
        _passwordHasher = passwordHasher;
        _logger = logger;
    }

    public async Task<Result> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Password change attempt for user {UserId}", request.UserId);

        var user = await _authRepository.GetUserForAuthAsync(request.UserId, request.DivSeq, cancellationToken);

        if (user is null || user.UseYn != "Y")
        {
            _logger.LogWarning("Password change failed: User not found for {UserId}", request.UserId);
            return Result.Failure("User not found.");
        }

        if (!_passwordHasher.Verify(request.CurrentPassword, user.PasswordHash ?? string.Empty))
        {
            _logger.LogWarning("Password change failed: Invalid current password for {UserId}", request.UserId);
            return Result.Failure("Current password is incorrect.");
        }

        var newPasswordHash = _passwordHasher.Hash(request.NewPassword);
        await _authRepository.UpdatePasswordHashAsync(request.UserId, request.DivSeq, newPasswordHash, request.UserId, cancellationToken);

        _logger.LogInformation("Password changed successfully for user {UserId}", request.UserId);
        return Result.Success();
    }
}
