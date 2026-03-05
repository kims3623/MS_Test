namespace Sphere.Application.DTOs.Auth;

#region Login DTOs

/// <summary>
/// Login request DTO.
/// </summary>
public class LoginRequestDto
{
    public string UserId { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string DivSeq { get; set; } = string.Empty;
}

/// <summary>
/// Login response DTO.
/// </summary>
public class LoginResponseDto
{
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public DateTime ExpiresAt { get; set; }
    public bool RequiresOtp { get; set; }
    public string? OtpSessionId { get; set; }
    public UserProfileDto User { get; set; } = new();
}

/// <summary>
/// User profile DTO.
/// </summary>
public class UserProfileDto
{
    public string UserId { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string DivSeq { get; set; } = string.Empty;
    public string DeptCode { get; set; } = string.Empty;
    public string DeptName { get; set; } = string.Empty;
    public string RoleCode { get; set; } = string.Empty;
    public string RoleName { get; set; } = string.Empty;
    public string Language { get; set; } = "ko-KR";
    public string Timezone { get; set; } = "Asia/Seoul";
    public List<string> Permissions { get; set; } = new();
}

/// <summary>
/// Token refresh request DTO.
/// </summary>
public class RefreshTokenRequestDto
{
    public string RefreshToken { get; set; } = string.Empty;
}

/// <summary>
/// Token response DTO.
/// </summary>
public class TokenResponseDto
{
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public DateTime ExpiresAt { get; set; }
}

/// <summary>
/// Change password request DTO.
/// </summary>
public class ChangePasswordRequestDto
{
    public string CurrentPassword { get; set; } = string.Empty;
    public string NewPassword { get; set; } = string.Empty;
    public string ConfirmPassword { get; set; } = string.Empty;
}

/// <summary>
/// OTP verification request DTO.
/// </summary>
public class VerifyOtpRequestDto
{
    public string OtpSessionId { get; set; } = string.Empty;
    public string OtpCode { get; set; } = string.Empty;
}

/// <summary>
/// OTP resend request DTO.
/// </summary>
public class ResendOtpRequestDto
{
    public string OtpSessionId { get; set; } = string.Empty;
}

#endregion

#region Oath DTOs

/// <summary>
/// Oath master DTO.
/// </summary>
public class OathMasterDto
{
    public string OathId { get; set; } = string.Empty;
    public string OathDocId { get; set; } = string.Empty;
    public string OathDocName { get; set; } = string.Empty;
    public string OathActionId { get; set; } = string.Empty;
    public string OathActionName { get; set; } = string.Empty;
    public string RequestVendor { get; set; } = string.Empty;
    public string RequestVendorName { get; set; } = string.Empty;
    public string AcceptVendor { get; set; } = string.Empty;
    public string AcceptVendorName { get; set; } = string.Empty;
    public string ProvidVendorList { get; set; } = string.Empty;
    public string CompleteOath { get; set; } = string.Empty;
    public string ExpireTime { get; set; } = string.Empty;
    public string UpdateUserId { get; set; } = string.Empty;
    public string UpdateDate { get; set; } = string.Empty;
}

/// <summary>
/// Oath master filter DTO.
/// </summary>
public class OathMasterFilterDto
{
    public string DivSeq { get; set; } = string.Empty;
    public string? RequestVendor { get; set; }
    public string? AcceptVendor { get; set; }
    public string? OathDocId { get; set; }
    public string? CompleteOath { get; set; }
}

/// <summary>
/// Oath login DTO.
/// </summary>
public class OathLoginDto
{
    public string OathId { get; set; } = string.Empty;
    public string OathDocName { get; set; } = string.Empty;
    public string OathActionName { get; set; } = string.Empty;
    public string RequestVendorName { get; set; } = string.Empty;
    public string AcceptVendorName { get; set; } = string.Empty;
}

/// <summary>
/// Oath login link DTO.
/// </summary>
public class OathLoginLinkDto
{
    public string StartDate { get; set; } = string.Empty;
    public string EndDate { get; set; } = string.Empty;
    public string ReqVendor { get; set; } = string.Empty;
    public string AcceptVendor { get; set; } = string.Empty;
    public string OathDocType { get; set; } = string.Empty;
    public string OathActionId { get; set; } = string.Empty;
}

/// <summary>
/// Oath history DTO.
/// </summary>
public class OathHistoryDto
{
    public string OathId { get; set; } = string.Empty;
    public string OathActionName { get; set; } = string.Empty;
    public string CompleteOath { get; set; } = string.Empty;
    public string CompleteDate { get; set; } = string.Empty;
    public string ActionCrud { get; set; } = string.Empty;
    public string UpdateDate { get; set; } = string.Empty;
    public string UpdateUserId { get; set; } = string.Empty;
}

/// <summary>
/// Oath document DTO.
/// </summary>
public class OathDocumentDto
{
    public string OathDocId { get; set; } = string.Empty;
    public string OathDocName { get; set; } = string.Empty;
    public string OathDocContent { get; set; } = string.Empty;
}

#endregion
