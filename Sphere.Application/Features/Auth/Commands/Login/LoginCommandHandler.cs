using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Interfaces;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Auth;
using Sphere.Domain.Entities.Auth;

namespace Sphere.Application.Features.Auth.Commands.Login;

/// <summary>
/// Handler for LoginCommand.
/// </summary>
public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<LoginResponseDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IDateTimeService _dateTimeService;
    private readonly ILogger<LoginCommandHandler> _logger;

    public LoginCommandHandler(
        IApplicationDbContext context,
        IJwtTokenService jwtTokenService,
        IPasswordHasher passwordHasher,
        IDateTimeService dateTimeService,
        ILogger<LoginCommandHandler> logger)
    {
        _context = context;
        _jwtTokenService = jwtTokenService;
        _passwordHasher = passwordHasher;
        _dateTimeService = dateTimeService;
        _logger = logger;
    }

    public async Task<Result<LoginResponseDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Login attempt for user {UserId} in division {DivSeq}", request.UserId, request.DivSeq);

        // 1. Find user
        var user = await _context.UserInfos
            .Where(u => u.DivSeq == request.DivSeq && u.UserId == request.UserId && u.UseYn == "Y")
            .FirstOrDefaultAsync(cancellationToken);

        if (user is null)
        {
            _logger.LogWarning("Login failed: User {UserId} not found", request.UserId);
            return Result<LoginResponseDto>.Failure("Invalid user ID or password.");
        }

        // 2. Check if account is locked
        if (user.IsLocked == "Y")
        {
            _logger.LogWarning("Login failed: User {UserId} account is locked", request.UserId);
            return Result<LoginResponseDto>.Failure("Account is locked. Please contact administrator.");
        }

        // 3. Verify password (from UserInfo table)
        var storedHash = user.PasswordHash ?? string.Empty;
        _logger.LogDebug("Password verification for {UserId}: hash length={Length}, format={Format}",
            request.UserId, storedHash.Length,
            storedHash.Contains(':') ? "PBKDF2" : "legacy-plain");

        if (!_passwordHasher.Verify(request.Password, storedHash))
        {
            // Increment fail count
            user.FailCount++;
            _logger.LogWarning("Login failed: Invalid password for user {UserId} (attempt {FailCount}/5)",
                request.UserId, user.FailCount);
            if (user.FailCount >= 5)
            {
                user.IsLocked = "Y";
                _logger.LogWarning("User {UserId} account locked after {FailCount} failed attempts", request.UserId, user.FailCount);
            }
            await _context.SaveChangesAsync(cancellationToken);

            return Result<LoginResponseDto>.Failure("Invalid user ID or password.");
        }

        // 4. Reset fail count on successful login
        user.FailCount = 0;
        user.LastLoginDate = _dateTimeService.Now;
        await _context.SaveChangesAsync(cancellationToken);

        // 5. OTP requirement (disabled - not in current DB schema)
        var requiresOtp = false;
        string? otpSessionId = null;

        // 6. Load user permissions (disabled - SPC_ROLE_PERMISSION table doesn't exist yet)
        var permissions = new List<string>();

        // 7. Generate tokens
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
