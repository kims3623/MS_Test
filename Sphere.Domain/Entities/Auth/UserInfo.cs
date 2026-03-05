using Sphere.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sphere.Domain.Entities.Auth;

/// <summary>
/// User information entity for managing user profiles.
/// Maps to SPC_USER_INFO table.
/// </summary>
public class UserInfo : SphereEntity
{
    /// <summary>
    /// User identifier (PK)
    /// </summary>
    public string UserId { get; set; } = string.Empty;

    /// <summary>
    /// Password hash (PBKDF2)
    /// </summary>
    public string PasswordHash { get; set; } = string.Empty;

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
    /// User email address
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// User phone number (maps to PHONE_NUMBER column)
    /// </summary>
    [NotMapped]
    public string PhoneNumber { get; set; } = string.Empty;

    /// <summary>
    /// User mobile number (maps to MOBILE_NUMBER column)
    /// </summary>
    [NotMapped]
    public string MobileNumber { get; set; } = string.Empty;

    /// <summary>
    /// User extension number (maps to EXTENSION column)
    /// </summary>
    [NotMapped]
    public string Extension { get; set; } = string.Empty;

    /// <summary>
    /// Department identifier (maps to DEPT_ID column)
    /// </summary>
    public string DeptId { get; set; } = string.Empty;

    /// <summary>
    /// Department name
    /// </summary>
    public string DeptName { get; set; } = string.Empty;

    /// <summary>
    /// Position identifier (maps to POSITION_ID column)
    /// </summary>
    [NotMapped]
    public string PositionId { get; set; } = string.Empty;

    /// <summary>
    /// Position/Title name
    /// </summary>
    public string PositionName { get; set; } = string.Empty;

    /// <summary>
    /// Role identifier (maps to ROLE_ID column)
    /// </summary>
    [NotMapped]
    public string RoleId { get; set; } = string.Empty;
    public string role_code { get; set; } = string.Empty;
    /// <summary>
    /// Role name
    /// </summary>
    public string RoleName { get; set; } = string.Empty;

    /// <summary>
    /// User group identifier (maps to USER_GROUP_ID column)
    /// </summary>
    [NotMapped]
    public string UserGroupId { get; set; } = string.Empty;

    /// <summary>
    /// Vendor identifier (maps to VENDOR_ID column)
    /// </summary>
    [NotMapped]
    public string VendorId { get; set; } = string.Empty;

    ///// <summary>
    ///// Authority level (maps to AUTHORITY_LEVEL column)
    ///// </summary>
    //public int AuthorityLevel { get; set; }

    /// <summary>
    /// User locale/language preference (maps to LOCALE column)
    /// </summary>
    [NotMapped]
    public string Locale { get; set; } = "ko-KR";

    /// <summary>
    /// User date format preference (maps to DATE_FORMAT column)
    /// </summary>
    [NotMapped]
    public string DateFormat { get; set; } = "yyyy-MM-dd";

    /// <summary>
    /// User number format preference (maps to NUMBER_FORMAT column)
    /// </summary>
    [NotMapped]
    public string NumberFormat { get; set; } = "#,##0.##";

    /// <summary>
    /// User timezone
    /// </summary>
    public string Timezone { get; set; } = "Asia/Seoul";

    /// <summary>
    /// Last login timestamp
    /// </summary>
    public DateTime? LastLoginDate { get; set; }

    /// <summary>
    /// Account locked flag
    /// </summary>
    public string IsLocked { get; set; } = "N";

    /// <summary>
    /// Login failure count
    /// </summary>
    public int FailCount { get; set; }
}
