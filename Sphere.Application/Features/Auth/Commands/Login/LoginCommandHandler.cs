using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Interfaces;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Auth;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Application.Features.Auth.Commands.Login;

/// <summary>
/// Handler for LoginCommand.
/// </summary>
public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<LoginResponseDto>>
{
    private readonly IAuthRepository _authRepository;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IDateTimeService _dateTimeService;
    private readonly ILogger<LoginCommandHandler> _logger;

    public LoginCommandHandler(
        IAuthRepository authRepository,
        IJwtTokenService jwtTokenService,
        IPasswordHasher passwordHasher,
        IDateTimeService dateTimeService,
        ILogger<LoginCommandHandler> logger)
    {
        _authRepository = authRepository;
        _jwtTokenService = jwtTokenService;
        _passwordHasher = passwordHasher;
        _dateTimeService = dateTimeService;
        _logger = logger;
    }

    public async Task<Result<LoginResponseDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Login attempt for user {UserId} in division {DivSeq}", request.UserId, request.DivSeq);

        var user = await _authRepository.GetUserForAuthAsync(request.UserId, request.DivSeq, cancellationToken);

        if (user is null || user.UseYn != "Y")
        {
            _logger.LogWarning("Login failed: User {UserId} not found", request.UserId);
            return Result<LoginResponseDto>.Failure("Invalid user ID or password.");
        }

        if (user.IsLocked == "Y")
        {
            _logger.LogWarning("Login failed: User {UserId} account is locked", request.UserId);
            return Result<LoginResponseDto>.Failure("Account is locked. Please contact administrator.");
        }

        var storedHash = user.PasswordHash ?? string.Empty;
        _logger.LogDebug("Password verification for {UserId}: hash length={Length}, format={Format}",
            request.UserId, storedHash.Length,
            storedHash.Contains(':') ? "PBKDF2" : "legacy-plain");

        if (!_passwordHasher.Verify(request.Password, storedHash))
        {
            var newFailCount = user.FailCount + 1;
            var isLocked = newFailCount >= 5 ? "Y" : "N";
            _logger.LogWarning("Login failed: Invalid password for user {UserId} (attempt {FailCount}/5)",
                request.UserId, newFailCount);
            if (isLocked == "Y")
                _logger.LogWarning("User {UserId} account locked after {FailCount} failed attempts", request.UserId, newFailCount);

            await _authRepository.UpdateLoginFailAsync(request.UserId, request.DivSeq, newFailCount, isLocked, cancellationToken);
            return Result<LoginResponseDto>.Failure("Invalid user ID or password.");
        }

        await _authRepository.UpdateLoginSuccessAsync(request.UserId, request.DivSeq, _dateTimeService.Now, cancellationToken);

        // OTP requirement (disabled - not in current DB schema)
        var requiresOtp = false;
        string? otpSessionId = null;

        // Load user permissions (disabled - SPC_ROLE_PERMISSION table doesn't exist yet)
        var permissions = new List<string>();

        var userProfile = new UserProfileDto
        {
            UserId = user.UserId,
            UserName = user.UserName ?? "Unknown",
            Email = user.Email ?? "",
            DivSeq = user.DivSeq,
            DeptCode = user.DeptId ?? "",
            DeptName = user.DeptName ?? "",
            RoleCode = user.RoleId ?? "",
            RoleName = user.RoleName ?? "",
            Language = user.Locale ?? "ko-KR",
            Timezone = user.Timezone ?? "Asia/Seoul",
            Permissions = permissions
        };

        var (accessToken, refreshToken, expiresAt) = _jwtTokenService.GenerateTokens(userProfile);

        _logger.LogInformation("Login successful for user {UserId}", request.UserId);

        return Result<LoginResponseDto>.Success(new LoginResponseDto
        {
            AccessToken = requiresOtp ? string.Empty : accessToken,
            RefreshToken = requiresOtp ? string.Empty : refreshToken,
            ExpiresAt = expiresAt,
            RequiresOtp = requiresOtp,
            OtpSessionId = otpSessionId,
            User = userProfile
        });
    }
}
