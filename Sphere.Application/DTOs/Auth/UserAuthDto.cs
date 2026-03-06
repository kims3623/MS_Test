namespace Sphere.Application.DTOs.Auth;

/// <summary>
/// 로그인 인증용 사용자 데이터 DTO (PasswordHash, FailCount, IsLocked 포함)
/// SPC_USER_INFO 테이블에서 직접 조회
/// </summary>
public class UserAuthDto
{
    public string UserId { get; set; } = string.Empty;
    public string DivSeq { get; set; } = string.Empty;
    public string? PasswordHash { get; set; }
    public int FailCount { get; set; }
    public string IsLocked { get; set; } = "N";
    public string UseYn { get; set; } = "Y";
    public DateTime? LastLoginDate { get; set; }
    public string? Email { get; set; }
    public string? UserName { get; set; }
    public string? DeptId { get; set; }
    public string? DeptName { get; set; }
    public string? RoleId { get; set; }
    public string? RoleName { get; set; }
    public string? Locale { get; set; }
    public string? Timezone { get; set; }
}

/// <summary>
/// 사용자 세션 DTO
/// SPC_USER_SESSION 테이블에서 조회
/// </summary>
public class UserSessionDto
{
    public string UserId { get; set; } = string.Empty;
    public string DivSeq { get; set; } = string.Empty;
    public string IsActive { get; set; } = "N";
}
