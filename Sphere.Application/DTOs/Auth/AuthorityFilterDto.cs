using System.ComponentModel.DataAnnotations;

namespace Sphere.Application.DTOs.Auth;

/// <summary>
/// 권한 기반 필터용 코드 조회 결과 DTO (8개 속성)
/// </summary>
/// <remarks>
/// 소스: USP_SPC_AUTHORITY_FILTER_SELECT 출력 결과
/// 용도: 드롭다운 필터용 코드 조회 (권한 기반 필터링)
/// </remarks>
public class AuthorityFilterResultDto
{
    /// <summary>
    /// 코드 ID (PK)
    /// </summary>
    [StringLength(50)]
    public string CodeId { get; set; } = string.Empty;

    /// <summary>
    /// 코드명 (다국어)
    /// </summary>
    [StringLength(100)]
    public string CodeName { get; set; } = string.Empty;

    /// <summary>
    /// 코드 옵션
    /// </summary>
    [StringLength(100)]
    public string CodeOpt { get; set; } = string.Empty;

    /// <summary>
    /// 협력사 ID
    /// </summary>
    [StringLength(50)]
    public string VendorId { get; set; } = string.Empty;

    /// <summary>
    /// 자재분류 ID
    /// </summary>
    [StringLength(50)]
    public string MtrlClassId { get; set; } = string.Empty;

    /// <summary>
    /// 자재분류명
    /// </summary>
    [StringLength(100)]
    public string MtrlClassName { get; set; } = string.Empty;

    /// <summary>
    /// 표시 순서
    /// </summary>
    public int DspSeq { get; set; }

    /// <summary>
    /// 사용 여부 ("Y"/"N")
    /// </summary>
    [StringLength(1)]
    public string UseYn { get; set; } = "Y";
}
