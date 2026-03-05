using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.Auth;

/// <summary>
/// User session entity for managing active sessions.
/// Mapped to SPC_USER_SESSION table.
/// </summary>
public class UserSession : SphereEntity
{
    public string SessionId { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string OtpEnabled { get; set; } = "N";
    public string RefreshToken { get; set; } = string.Empty;
    public DateTime? ExpiresAt { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? LastLoginAt { get; set; }
    public string IpAddress { get; set; } = string.Empty;
    public string LastIpAddress { get; set; } = string.Empty;
    public string UserAgent { get; set; } = string.Empty;
    public string LastUserAgent { get; set; } = string.Empty;
    public string IsActive { get; set; } = "Y";
    public string PasswordResetRequired { get; set; } = "N";
}
