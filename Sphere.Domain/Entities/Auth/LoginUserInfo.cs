using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.Auth;

/// <summary>
/// Login user information entity for session management.
/// Contains current session user details.
/// </summary>
public class LoginUserInfo : SphereEntity
{
    /// <summary>
    /// User identifier (PK)
    /// </summary>
    public string UserId { get; set; } = string.Empty;

    /// <summary>
    /// User name
    /// </summary>
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// User name in Korean
    /// </summary>
    public string UserNameK { get; set; } = string.Empty;

    /// <summary>
    /// User name in English
    /// </summary>
    public string UserNameE { get; set; } = string.Empty;

    /// <summary>
    /// User email
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Department code
    /// </summary>
    public string DeptCode { get; set; } = string.Empty;

    /// <summary>
    /// Department name
    /// </summary>
    public string DeptName { get; set; } = string.Empty;

    /// <summary>
    /// Role code
    /// </summary>
    public string RoleCode { get; set; } = string.Empty;

    /// <summary>
    /// Role name
    /// </summary>
    public string RoleName { get; set; } = string.Empty;

    /// <summary>
    /// User language preference
    /// </summary>
    public string Language { get; set; } = "ko-KR";

    /// <summary>
    /// Admin flag
    /// </summary>
    public string IsAdmin { get; set; } = "N";

    /// <summary>
    /// Session token
    /// </summary>
    public string SessionToken { get; set; } = string.Empty;

    /// <summary>
    /// Session expiry timestamp
    /// </summary>
    public DateTime? SessionExpiry { get; set; }

    /// <summary>
    /// Current menu ID
    /// </summary>
    public string CurrentMenuId { get; set; } = string.Empty;

    /// <summary>
    /// IP address of the login session
    /// </summary>
    public string IpAddress { get; set; } = string.Empty;

    /// <summary>
    /// User agent string
    /// </summary>
    public string UserAgent { get; set; } = string.Empty;

    /// <summary>
    /// Login timestamp
    /// </summary>
    public DateTime? LoginDate { get; set; }
}
