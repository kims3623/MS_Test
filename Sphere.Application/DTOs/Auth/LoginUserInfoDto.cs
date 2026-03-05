using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Sphere.Application.DTOs.Auth;

/// <summary>
/// JWT 토큰 Payload 및 세션 컨텍스트 저장용 DTO (25개 속성)
/// </summary>
/// <remarks>
/// 소스: OutSystems LoginUserInfoRec (Sphere.model.js)
/// 용도: JWT Claims + 세션 컨텍스트
/// </remarks>
public class LoginUserInfoDto
{
    #region 인증 그룹 (5개)

    /// <summary>
    /// 사용자 고유 ID
    /// </summary>
    [Required]
    [StringLength(50)]
    public string UserId { get; set; } = string.Empty;

    /// <summary>
    /// 사용자명 (표시용)
    /// </summary>
    [Required]
    [StringLength(100)]
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// 이메일 주소
    /// </summary>
    [Required]
    [StringLength(200)]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// 암호화된 비밀번호 (JSON 직렬화 제외)
    /// </summary>
    [JsonIgnore]
    public string PasswordHash { get; set; } = string.Empty;

    /// <summary>
    /// 최종 로그인 일시
    /// </summary>
    public DateTime? LastLoginDate { get; set; }

    #endregion

    #region 조직 그룹 (6개)

    /// <summary>
    /// 사업부 시퀀스
    /// </summary>
    [Required]
    [StringLength(10)]
    public string DivSeq { get; set; } = string.Empty;

    /// <summary>
    /// 사업부명 (다국어 지원)
    /// </summary>
    [StringLength(100)]
    public string DivName { get; set; } = string.Empty;

    /// <summary>
    /// 부서 ID
    /// </summary>
    [StringLength(50)]
    public string DeptId { get; set; } = string.Empty;

    /// <summary>
    /// 부서명 (다국어 지원)
    /// </summary>
    [StringLength(100)]
    public string DeptName { get; set; } = string.Empty;

    /// <summary>
    /// 직급 ID
    /// </summary>
    [StringLength(50)]
    public string PositionId { get; set; } = string.Empty;

    /// <summary>
    /// 직급명 (다국어 지원)
    /// </summary>
    [StringLength(100)]
    public string PositionName { get; set; } = string.Empty;

    #endregion

    #region 권한 그룹 (6개)

    /// <summary>
    /// 협력사 ID (FK → SPC_VENDOR_INFO)
    /// </summary>
    [StringLength(50)]
    public string VendorId { get; set; } = string.Empty;

    /// <summary>
    /// 협력사명 (다국어 지원)
    /// </summary>
    [StringLength(100)]
    public string VendorName { get; set; } = string.Empty;

    /// <summary>
    /// 협력사 유형 (VENDOR_TYPE 코드)
    /// </summary>
    [StringLength(20)]
    public string VendorType { get; set; } = string.Empty;

    /// <summary>
    /// 관리자 여부
    /// </summary>
    public bool IsAdmin { get; set; }

    /// <summary>
    /// 역할 ID
    /// </summary>
    [StringLength(50)]
    public string RoleId { get; set; } = string.Empty;

    /// <summary>
    /// 역할명 (다국어 지원)
    /// </summary>
    [StringLength(100)]
    public string RoleName { get; set; } = string.Empty;

    #endregion

    #region 로케일 그룹 (4개)

    /// <summary>
    /// 언어 설정
    /// </summary>
    [StringLength(10)]
    public string Locale { get; set; } = "ko-KR";

    /// <summary>
    /// 시간대
    /// </summary>
    [StringLength(50)]
    public string TimeZone { get; set; } = "Asia/Seoul";

    /// <summary>
    /// 날짜 포맷
    /// </summary>
    [StringLength(20)]
    public string DateFormat { get; set; } = "yyyy-MM-dd";

    /// <summary>
    /// 숫자 포맷
    /// </summary>
    [StringLength(20)]
    public string NumberFormat { get; set; } = "#,##0.##";

    #endregion

    #region 세션 그룹 (4개)

    /// <summary>
    /// 로그인 시각 (서버 생성)
    /// </summary>
    public DateTime LoginTime { get; set; }

    /// <summary>
    /// 세션 고유 ID (GUID)
    /// </summary>
    [StringLength(50)]
    public string SessionId { get; set; } = string.Empty;

    /// <summary>
    /// 클라이언트 IP (감사용)
    /// </summary>
    [StringLength(50)]
    public string IpAddress { get; set; } = string.Empty;

    /// <summary>
    /// 브라우저 정보 (감사용)
    /// </summary>
    [StringLength(500)]
    public string UserAgent { get; set; } = string.Empty;

    #endregion
}
