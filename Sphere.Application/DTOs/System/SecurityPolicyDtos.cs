namespace Sphere.Application.DTOs.System;

#region Security Policy DTOs

/// <summary>
/// Security policy DTO.
/// </summary>
public class SecurityPolicyDto
{
    public string DivSeq { get; set; } = string.Empty;

    // Password Policy
    public int MinPasswordLength { get; set; } = 8;
    public int MaxPasswordLength { get; set; } = 20;
    public string RequireUppercase { get; set; } = "Y";
    public string RequireLowercase { get; set; } = "Y";
    public string RequireDigit { get; set; } = "Y";
    public string RequireSpecialChar { get; set; } = "Y";
    public int PasswordHistoryCount { get; set; } = 5;
    public int PasswordExpiryDays { get; set; } = 90;
    public int PasswordWarningDays { get; set; } = 14;

    // Login Policy
    public int MaxLoginAttempts { get; set; } = 5;
    public int LockoutDurationMinutes { get; set; } = 30;
    public int SessionTimeoutMinutes { get; set; } = 60;
    public string AllowMultipleSessions { get; set; } = "N";
    public int MaxConcurrentSessions { get; set; } = 1;

    // OTP Policy
    public string RequireOtpForLogin { get; set; } = "N";
    public string RequireOtpForSensitiveOps { get; set; } = "Y";
    public int OtpValidityMinutes { get; set; } = 5;
    public int OtpMaxAttempts { get; set; } = 3;

    // IP Policy
    public string EnableIpWhitelist { get; set; } = "N";
    public string EnableIpBlacklist { get; set; } = "N";
    public List<string> IpWhitelist { get; set; } = new();
    public List<string> IpBlacklist { get; set; } = new();

    // Audit Policy
    public string EnableAuditLog { get; set; } = "Y";
    public int AuditLogRetentionDays { get; set; } = 365;
    public string LogSensitiveDataAccess { get; set; } = "Y";

    // Metadata
    public string UpdateDate { get; set; } = string.Empty;
    public string UpdateUserId { get; set; } = string.Empty;
}

/// <summary>
/// Update security policy request DTO.
/// </summary>
public class UpdateSecurityPolicyRequestDto
{
    public string DivSeq { get; set; } = string.Empty;

    // Password Policy
    public int? MinPasswordLength { get; set; }
    public int? MaxPasswordLength { get; set; }
    public string? RequireUppercase { get; set; }
    public string? RequireLowercase { get; set; }
    public string? RequireDigit { get; set; }
    public string? RequireSpecialChar { get; set; }
    public int? PasswordHistoryCount { get; set; }
    public int? PasswordExpiryDays { get; set; }
    public int? PasswordWarningDays { get; set; }

    // Login Policy
    public int? MaxLoginAttempts { get; set; }
    public int? LockoutDurationMinutes { get; set; }
    public int? SessionTimeoutMinutes { get; set; }
    public string? AllowMultipleSessions { get; set; }
    public int? MaxConcurrentSessions { get; set; }

    // OTP Policy
    public string? RequireOtpForLogin { get; set; }
    public string? RequireOtpForSensitiveOps { get; set; }
    public int? OtpValidityMinutes { get; set; }
    public int? OtpMaxAttempts { get; set; }

    // IP Policy
    public string? EnableIpWhitelist { get; set; }
    public string? EnableIpBlacklist { get; set; }
    public List<string>? IpWhitelist { get; set; }
    public List<string>? IpBlacklist { get; set; }

    // Audit Policy
    public string? EnableAuditLog { get; set; }
    public int? AuditLogRetentionDays { get; set; }
    public string? LogSensitiveDataAccess { get; set; }

    public string UpdateUserId { get; set; } = string.Empty;
}

/// <summary>
/// Update security policy response DTO.
/// </summary>
public class UpdateSecurityPolicyResponseDto
{
    public string Result { get; set; } = string.Empty;
    public string ResultMessage { get; set; } = string.Empty;
}

#endregion

#region OTP Settings DTOs

/// <summary>
/// OTP settings list filter DTO.
/// </summary>
public class OTPSettingsFilterDto
{
    public string DivSeq { get; set; } = string.Empty;
    public string? UserId { get; set; }
    public string? OtpType { get; set; }
    public string? IsActive { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}

/// <summary>
/// OTP settings item DTO.
/// </summary>
public class OTPSettingsItemDto
{
    public string OtpId { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string OtpType { get; set; } = string.Empty;
    public string OtpTypeName { get; set; } = string.Empty;
    public string OtpTarget { get; set; } = string.Empty;
    public string IsActive { get; set; } = string.Empty;
    public string IsVerified { get; set; } = string.Empty;
    public string LastUsedDate { get; set; } = string.Empty;
    public string CreateDate { get; set; } = string.Empty;
    public string UpdateDate { get; set; } = string.Empty;
}

/// <summary>
/// OTP settings list response DTO.
/// </summary>
public class OTPSettingsResponseDto
{
    public List<OTPSettingsItemDto> Items { get; set; } = new();
    public int TotalCount { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}

/// <summary>
/// OTP history filter DTO.
/// </summary>
public class OTPHistoryFilterDto
{
    public string DivSeq { get; set; } = string.Empty;
    public string? StartDate { get; set; }
    public string? EndDate { get; set; }
    public string? UserId { get; set; }
    public string? OtpType { get; set; }
    public string? OtpResult { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 50;
}

/// <summary>
/// OTP history item DTO.
/// </summary>
public class OTPHistoryItemDto
{
    public long HistoryId { get; set; }
    public string OtpDate { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string OtpType { get; set; } = string.Empty;
    public string OtpTypeName { get; set; } = string.Empty;
    public string OtpTarget { get; set; } = string.Empty;
    public string OtpPurpose { get; set; } = string.Empty;
    public string OtpResult { get; set; } = string.Empty;
    public string OtpResultName { get; set; } = string.Empty;
    public int AttemptCount { get; set; }
    public string IpAddress { get; set; } = string.Empty;
    public string ExpireDate { get; set; } = string.Empty;
    public string VerifyDate { get; set; } = string.Empty;
}

/// <summary>
/// OTP history response DTO.
/// </summary>
public class OTPHistoryResponseDto
{
    public List<OTPHistoryItemDto> Items { get; set; } = new();
    public int TotalCount { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}

/// <summary>
/// Create OTP settings request DTO.
/// </summary>
public class CreateOTPSettingsRequestDto
{
    public string DivSeq { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string OtpType { get; set; } = string.Empty;
    public string OtpTarget { get; set; } = string.Empty;
    public string CreateUserId { get; set; } = string.Empty;
}

/// <summary>
/// Update OTP settings request DTO.
/// </summary>
public class UpdateOTPSettingsRequestDto
{
    public string DivSeq { get; set; } = string.Empty;
    public string OtpId { get; set; } = string.Empty;
    public string? OtpTarget { get; set; }
    public string? IsActive { get; set; }
    public string UpdateUserId { get; set; } = string.Empty;
}

/// <summary>
/// Delete OTP settings request DTO.
/// </summary>
public class DeleteOTPSettingsRequestDto
{
    public string DivSeq { get; set; } = string.Empty;
    public string OtpId { get; set; } = string.Empty;
    public string DeleteUserId { get; set; } = string.Empty;
}

#endregion
