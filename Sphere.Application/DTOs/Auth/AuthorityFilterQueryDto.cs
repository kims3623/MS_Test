using System.ComponentModel.DataAnnotations;

namespace Sphere.Application.DTOs.Auth;

/// <summary>
/// 권한 필터 조회 요청 DTO (4개 속성)
/// </summary>
/// <remarks>
/// 용도: USP_SPC_AUTHORITY_FILTER_SELECT SP 입력 파라미터
/// </remarks>
public class AuthorityFilterRequestDto
{
    /// <summary>
    /// 사업부 시퀀스
    /// </summary>
    [Required(ErrorMessage = "사업부 시퀀스는 필수 입력 항목입니다.")]
    [StringLength(10)]
    public string DivSeq { get; set; } = string.Empty;

    /// <summary>
    /// 코드 분류 ID
    /// </summary>
    [Required(ErrorMessage = "코드 분류 ID는 필수 입력 항목입니다.")]
    [StringLength(50)]
    public string CodeClassId { get; set; } = string.Empty;

    /// <summary>
    /// 사용자 ID (권한 필터링용, 선택)
    /// </summary>
    [StringLength(50)]
    public string? UserId { get; set; }

    /// <summary>
    /// 언어 설정 (기본값: ko-KR)
    /// </summary>
    [StringLength(10)]
    public string Language { get; set; } = "ko-KR";
}

/// <summary>
/// Alias for AuthorityFilterRequestDto (used in tests as AuthorityFilterQueryDto)
/// </summary>
public class AuthorityFilterQueryDto : AuthorityFilterRequestDto { }
