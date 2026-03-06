using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Interfaces;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Auth;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Application.Features.Auth.Commands.VerifyOtp;

/// <summary>
/// Handler for VerifyOtpCommand.
/// </summary>
public class VerifyOtpCommandHandler : IRequestHandler<VerifyOtpCommand, Result<LoginResponseDto>>
{
    private readonly IAuthRepository _authRepository;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly IOtpService _otpService;
    private readonly IDateTimeService _dateTimeService;
    private readonly ILogger<VerifyOtpCommandHandler> _logger;

    public VerifyOtpCommandHandler(
        IAuthRepository authRepository,
        IJwtTokenService jwtTokenService,
        IOtpService otpService,
        IDateTimeService dateTimeService,
        ILogger<VerifyOtpCommandHandler> logger)
    {
        _authRepository = authRepository;
        _jwtTokenService = jwtTokenService;
        _otpService = otpService;
        _dateTimeService = dateTimeService;
        _logger = logger;
    }

    public async Task<Result<LoginResponseDto>> Handle(VerifyOtpCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("OTP verification attempt for session {SessionId}", request.OtpSessionId);

        var otpValidation = await _otpService.ValidateOtpAsync(
            request.OtpSessionId,
            request.OtpCode,
            cancellationToken);

        if (!otpValidation.IsValid)
        {
            _logger.LogWarning("OTP verification failed: {Reason}", otpValidation.FailureReason);
            return Result<LoginResponseDto>.Failure(otpValidation.FailureReason ?? "Invalid OTP code.");
        }

        var user = await _authRepository.GetUserForAuthAsync(request.UserId, request.DivSeq, cancellationToken);

        if (user is null || user.UseYn != "Y")
        {
            _logger.LogWarning("OTP verification failed: User {UserId} not found", request.UserId);
            return Result<LoginResponseDto>.Failure("User not found.");
        }

        var permissions = (await _authRepository.GetRolePermissionsAsync(
            request.DivSeq, user.RoleId ?? string.Empty, cancellationToken)).ToList();

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

        await _authRepository.UpdateLastLoginAsync(user.UserId, _dateTimeService.Now, cancellationToken);
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
