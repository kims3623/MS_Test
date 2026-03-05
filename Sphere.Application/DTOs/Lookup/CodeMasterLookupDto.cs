namespace Sphere.Application.DTOs.Lookup;

/// <summary>
/// SPC_CODE_MST 기반 Lookup 데이터의 공통 베이스 DTO
/// 모든 코드 마스터 View에서 공유하는 속성 정의
/// </summary>
public class CodeMasterLookupDto
{
    /// <summary>
    /// 사업부 구분 코드
    /// </summary>
    public string DivSeq { get; set; } = string.Empty;

    /// <summary>
    /// 코드 ID (code_id -> 각 View의 고유 ID)
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// 코드 별칭 (code_alias)
    /// </summary>
    public string Alias { get; set; } = string.Empty;

    /// <summary>
    /// 로케일 기반 표시 명칭 (런타임 결정)
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 한국어 명칭 (code_name_k)
    /// </summary>
    public string NameK { get; set; } = string.Empty;

    /// <summary>
    /// 영어 명칭 (code_name_e)
    /// </summary>
    public string NameE { get; set; } = string.Empty;

    /// <summary>
    /// 중국어 명칭 (code_name_c)
    /// </summary>
    public string NameC { get; set; } = string.Empty;

    /// <summary>
    /// 베트남어 명칭 (code_name_v)
    /// </summary>
    public string NameV { get; set; } = string.Empty;

    /// <summary>
    /// 표시 순서 (dsp_seq)
    /// </summary>
    public int DisplaySeq { get; set; }

    /// <summary>
    /// 코드 옵션 (code_opt) - 분류별 의미 상이
    /// </summary>
    public string CodeOpt { get; set; } = string.Empty;
}
