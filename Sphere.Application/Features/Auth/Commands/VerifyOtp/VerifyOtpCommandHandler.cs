using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Interfaces;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Auth;

namespace Sphere.Application.Features.Auth.Commands.VerifyOtp;

/// <summary>
/// Handler for VerifyOtpCommand.
/// </summary>
public class VerifyOtpCommandHandler : IRequestHandler<VerifyOtpCommand, Result<LoginResponseDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly IOtpService _otpService;
    private readonly IDateTimeService _dateTimeService;
    private readonly ILogger<VerifyOtpCommandHandler> _logger;

    public VerifyOtpCommandHandler(
        IApplicationDbContext context,
        IJwtTokenService jwtTokenService,
        IOtpService otpService,
        IDateTimeService dateTimeService,
        ILogger<VerifyOtpCommandHandler> logger)
    {
        _context = context;
        _jwtTokenService = jwtTokenService;
        _otpService = otpService;
        _dateTimeService = dateTimeService;
        _logger = logger;
    }

    public async Task<Result<LoginResponseDto>> Handle(VerifyOtpCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("OTP verification attempt for session {SessionId}", request.OtpSessionId);

        // 1. Validate OTP session
        var otpValidation = await _otpService.ValidateOtpAsync(
            request.OtpSessionId,
            request.OtpCode,
            cancellationToken);

        if (!otpValidation.IsValid)
        {
            _logger.LogWarning("OTP verification failed: {Reason}", otpValidation.FailureReason);
            return Result<LoginResponseDto>.Failure(otpValidation.FailureReason ?? "Invalid OTP code.");
        }

        // 2. Find user
        var user = await _context.UserInfos
            .Where(u => u.DivSeq == request.DivSeq && u.UserId == request.UserId && u.UseYn == "Y")
            .FirstOrDefaultAsync(cancellationToken);

        if (user is null)
        {
            _logger.LogWarning("OTP verification failed: User {UserId} not found", request.UserId);
            return Result<LoginResponseDto>.Failure("User not found.");
        }

        // 3. Load user permissions
        var permissions = await _context.RolePermissions
            .Where(rp => rp.DivSeq == request.DivSeq && rp.RoleCode == user.RoleId && rp.GrantedYn == "Y")
            .Select(rp => $"{rp.ResourceType}:{rp.ResourceId}:{rp.ActionType}")
            .ToListAsync(cancellationToken);

        // 4. Generate tokens
        var userProfile = new UserProfileDto
        {
            UserId = user.UserId,
            UserName = user.UserName,
            Email = user.Email,
            DivSeq = user.DivSeq,
            DeptCode = user.DeptId,
            DeptName = user.DeptName,
            RoleCode = user.RoleId,
            RoleName = user.RoleName,
            Language = user.Locale,
            Timezone = user.Timezone,
            Permissions = permissions
        };

        var (accessToken, refreshToken, expiresAt) = _jwtTokenService.GenerateTokens(userProfile);

        // 5. Update last login
        user.LastLoginDate = _dateTimeService.Now;
        await _context.SaveChangesAsync(cancellationToken);

        // 6. Clear OTP session
        await _otpService.ClearOtpSessionAsync(request.OtpSessionId, cancellationToken);

        _logger.LogInformation("OTP verification successful for user {UserId}", request.UserId);

        return Result<LoginResponseDto>.Success(new LoginResponseDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            ExpiresAt = expiresAt,
            RequiresOtp = false,
            OtpSessionId = null,
            User = userProfile
        });
    }
}
