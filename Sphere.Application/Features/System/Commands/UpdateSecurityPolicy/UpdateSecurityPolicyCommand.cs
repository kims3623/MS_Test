using MediatR;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.System;

namespace Sphere.Application.Features.System.Commands.UpdateSecurityPolicy;

/// <summary>
/// Command to update security policy.
/// </summary>
public record UpdateSecurityPolicyCommand : IRequest<Result<UpdateSecurityPolicyResponseDto>>
{
    public string DivSeq { get; init; } = string.Empty;

    // Password Policy
    public int? MinPasswordLength { get; init; }
    public int? MaxPasswordLength { get; init; }
    public string? RequireUppercase { get; init; }
    public string? RequireLowercase { get; init; }
    public string? RequireDigit { get; init; }
    public string? RequireSpecialChar { get; init; }
    public int? PasswordHistoryCount { get; init; }
    public int? PasswordExpiryDays { get; init; }
    public int? PasswordWarningDays { get; init; }

    // Login Policy
    public int? MaxLoginAttempts { get; init; }
    public int? LockoutDurationMinutes { get; init; }
    public int? SessionTimeoutMinutes { get; init; }
    public string? AllowMultipleSessions { get; init; }
    public int? MaxConcurrentSessions { get; init; }

    // OTP Policy
    public string? RequireOtpForLogin { get; init; }
    public string? RequireOtpForSensitiveOps { get; init; }
    public int? OtpValidityMinutes { get; init; }
    public int? OtpMaxAttempts { get; init; }

    // IP Policy
    public string? EnableIpWhitelist { get; init; }
    public string? EnableIpBlacklist { get; init; }
    public List<string>? IpWhitelist { get; init; }
    public List<string>? IpBlacklist { get; init; }

    // Audit Policy
    public string? EnableAuditLog { get; init; }
    public int? AuditLogRetentionDays { get; init; }
    public string? LogSensitiveDataAccess { get; init; }

    public string UpdateUserId { get; init; } = string.Empty;
}
