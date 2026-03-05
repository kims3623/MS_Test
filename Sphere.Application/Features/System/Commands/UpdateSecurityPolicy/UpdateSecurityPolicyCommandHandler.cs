using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.System;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Application.Features.System.Commands.UpdateSecurityPolicy;

/// <summary>
/// Handler for UpdateSecurityPolicyCommand.
/// </summary>
public class UpdateSecurityPolicyCommandHandler : IRequestHandler<UpdateSecurityPolicyCommand, Result<UpdateSecurityPolicyResponseDto>>
{
    private readonly ISystemRepository _systemRepository;
    private readonly ILogger<UpdateSecurityPolicyCommandHandler> _logger;

    public UpdateSecurityPolicyCommandHandler(
        ISystemRepository systemRepository,
        ILogger<UpdateSecurityPolicyCommandHandler> logger)
    {
        _systemRepository = systemRepository;
        _logger = logger;
    }

    public async Task<Result<UpdateSecurityPolicyResponseDto>> Handle(UpdateSecurityPolicyCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating security policy for DivSeq={DivSeq}", request.DivSeq);

        try
        {
            var dto = new UpdateSecurityPolicyRequestDto
            {
                DivSeq = request.DivSeq,
                MinPasswordLength = request.MinPasswordLength,
                MaxPasswordLength = request.MaxPasswordLength,
                RequireUppercase = request.RequireUppercase,
                RequireLowercase = request.RequireLowercase,
                RequireDigit = request.RequireDigit,
                RequireSpecialChar = request.RequireSpecialChar,
                PasswordHistoryCount = request.PasswordHistoryCount,
                PasswordExpiryDays = request.PasswordExpiryDays,
                PasswordWarningDays = request.PasswordWarningDays,
                MaxLoginAttempts = request.MaxLoginAttempts,
                LockoutDurationMinutes = request.LockoutDurationMinutes,
                SessionTimeoutMinutes = request.SessionTimeoutMinutes,
                AllowMultipleSessions = request.AllowMultipleSessions,
                MaxConcurrentSessions = request.MaxConcurrentSessions,
                RequireOtpForLogin = request.RequireOtpForLogin,
                RequireOtpForSensitiveOps = request.RequireOtpForSensitiveOps,
                OtpValidityMinutes = request.OtpValidityMinutes,
                OtpMaxAttempts = request.OtpMaxAttempts,
                EnableIpWhitelist = request.EnableIpWhitelist,
                EnableIpBlacklist = request.EnableIpBlacklist,
                IpWhitelist = request.IpWhitelist,
                IpBlacklist = request.IpBlacklist,
                EnableAuditLog = request.EnableAuditLog,
                AuditLogRetentionDays = request.AuditLogRetentionDays,
                LogSensitiveDataAccess = request.LogSensitiveDataAccess,
                UpdateUserId = request.UpdateUserId
            };

            var result = await _systemRepository.UpdateSecurityPolicyAsync(dto, cancellationToken);

            if (result.Result != "S")
            {
                return Result<UpdateSecurityPolicyResponseDto>.Failure(result.ResultMessage);
            }

            _logger.LogInformation("Security policy updated successfully");

            return Result<UpdateSecurityPolicyResponseDto>.Success(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating security policy");
            return Result<UpdateSecurityPolicyResponseDto>.Failure($"보안 정책 수정 중 오류가 발생했습니다: {ex.Message}");
        }
    }
}
