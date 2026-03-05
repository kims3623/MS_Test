using System.ComponentModel.DataAnnotations;

namespace Sphere.Application.DTOs.Auth;

/// <summary>
/// 사용자 프로필 조회/수정용 DTO (20개 속성)
/// </summary>
/// <remarks>
/// 소스: OutSystems UserInfoRec (Sphere.model.js)
/// 용도: 사용자 프로필 조회/수정, 사용자 목록 표시
/// </remarks>
public class UserInfoDto
{
    #region 신원 그룹 (5개)

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
    /// 사용자명 (한국어)
    /// </summary>
    [StringLength(100)]
    public string UserNameK { get; set; } = string.Empty;

    /// <summary>
    /// 사용자명 (영어)
    /// </summary>
    [StringLength(100)]
    public string UserNameE { get; set; } = string.Empty;

    /// <summary>
    /// 이메일 주소
    /// </summary>
    [Required]
    [StringLength(200)]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    #endregion

    #region 조직 그룹 (5개)

    /// <summary>
    /// 사업부 시퀀스
    /// </summary>
    [Required]
    [StringLength(10)]
    public string DivSeq { get; set; } = string.Empty;

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

    #region 권한 그룹 (4개)

    /// <summary>
    /// 협력사 ID (FK)
    /// </summary>
    [StringLength(50)]
    public string VendorId { get; set; } = string.Empty;

    /// <summary>
    /// 역할 ID
    /// </summary>
    [StringLength(50)]
    public string RoleId { get; set; } = string.Empty;

    /// <summary>
    /// 사용자 그룹 ID
    /// </summary>
    [StringLength(50)]
    public string UserGroupId { get; set; } = string.Empty;

    /// <summary>
    /// 권한 레벨 (0=일반, 9=최고관리자)
    /// </summary>
    [Range(0, 9)]
    public int AuthorityLevel { get; set; }

    #endregion

    #region 연락처 그룹 (3개)

    /// <summary>
    /// 전화번호
    /// </summary>
    [StringLength(30)]
    public string PhoneNumber { get; set; } = string.Empty;

    /// <summary>
    /// 휴대폰 번호
    /// </summary>
    [StringLength(30)]
    public string MobileNumber { get; set; } = string.Empty;

    /// <summary>
    /// 내선번호
    /// </summary>
    [StringLength(20)]
    public string Extension { get; set; } = string.Empty;

    #endregion

    #region 감사 그룹 (3개)

    /// <summary>
    /// 사용 여부 ("Y"/"N")
    /// </summary>
    [StringLength(1)]
    public string UseYn { get; set; } = "Y";

    /// <summary>
    /// 생성일시
    /// </summary>
    public DateTime? CreateDate { get; set; }

    /// <summary>
    /// 수정일시
    /// </summary>
    public DateTime? UpdateDate { get; set; }

    #endregion
}
