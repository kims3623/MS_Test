namespace Sphere.Application.DTOs.Alarm;

/// <summary>
/// 알람 액션 목록 조회용 간소화 DTO
/// </summary>
/// <remarks>
/// SPC_ALARM_ACTION 테이블의 간소화된 조회용 DTO입니다.
/// 목록 조회 및 드롭다운 표시 등에 사용됩니다.
/// </remarks>
public class AlarmActionListDto
{
    /// <summary>
    /// 알람 액션 ID
    /// </summary>
    public string AlmActionId { get; set; } = string.Empty;

    /// <summary>
    /// 알람 액션명
    /// </summary>
    public string AlmActionName { get; set; } = string.Empty;

    /// <summary>
    /// 액션 순서
    /// </summary>
    public int ActSeq { get; set; }

    /// <summary>
    /// 메일 발송 필요 여부
    /// </summary>
    public bool RequiresMail { get; set; }

    /// <summary>
    /// 승인 필요 여부
    /// </summary>
    public bool RequiresApproval { get; set; }
}
