namespace Sphere.Application.DTOs.Alarm;

/// <summary>
/// 알람 프로세스 DTO
/// </summary>
/// <remarks>
/// 알람 프로세스 정보를 담는 DTO입니다.
/// SPC_DEFAULT_MNG_RULE.alm_proc_type 참조 시 사용됩니다.
/// </remarks>
public class AlarmProcessDto
{
    /// <summary>
    /// 사업부 코드
    /// </summary>
    public string DivSeq { get; set; } = string.Empty;

    /// <summary>
    /// 알람 프로세스 ID
    /// </summary>
    public string AlmProcId { get; set; } = string.Empty;

    /// <summary>
    /// 알람 프로세스명 (로케일 기반)
    /// </summary>
    public string AlmProcName { get; set; } = string.Empty;

    /// <summary>
    /// 알람 프로세스명 (한국어)
    /// </summary>
    public string AlmProcNameK { get; set; } = string.Empty;

    /// <summary>
    /// 알람 프로세스명 (영어)
    /// </summary>
    public string AlmProcNameE { get; set; } = string.Empty;

    /// <summary>
    /// 알람 프로세스 유형
    /// </summary>
    public string AlmProcType { get; set; } = string.Empty;

    /// <summary>
    /// 기본 액션 ID
    /// </summary>
    public string DefaultActionId { get; set; } = string.Empty;

    /// <summary>
    /// 사용 여부 (Y/N)
    /// </summary>
    public string UseYn { get; set; } = "Y";

    /// <summary>
    /// 표시 순서
    /// </summary>
    public int DisplaySeq { get; set; }

    /// <summary>
    /// 설명
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// 생성일시
    /// </summary>
    public DateTime? CreateDate { get; set; }

    /// <summary>
    /// 수정일시
    /// </summary>
    public DateTime? UpdateDate { get; set; }
}
