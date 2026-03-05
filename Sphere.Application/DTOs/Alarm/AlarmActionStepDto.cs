namespace Sphere.Application.DTOs.Alarm;

/// <summary>
/// 알람 액션 단계 DTO (SPC_ALARM_ACTION 매핑)
/// </summary>
/// <remarks>
/// SPC_ALARM_ACTION 테이블의 15개 컬럼을 매핑합니다.
/// 알람 프로세스의 각 액션 단계 관리에 사용됩니다.
/// </remarks>
public class AlarmActionStepDto
{
    // ─────────────────────────────────────────────────────────────
    // Primary Keys (3개)
    // ─────────────────────────────────────────────────────────────

    /// <summary>
    /// 사업부 코드 (PK, div_seq)
    /// </summary>
    public string DivSeq { get; set; } = string.Empty;

    /// <summary>
    /// 알람 프로세스 ID (PK, alm_proc_id)
    /// </summary>
    public string AlmProcId { get; set; } = string.Empty;

    /// <summary>
    /// 알람 액션 ID (PK, alm_action_id)
    /// </summary>
    public string AlmActionId { get; set; } = string.Empty;

    // ─────────────────────────────────────────────────────────────
    // Action Info (4개)
    // ─────────────────────────────────────────────────────────────

    /// <summary>
    /// 액션 순서 (act_seq)
    /// </summary>
    public int ActSeq { get; set; }

    /// <summary>
    /// 메일 발송 여부 (mail_yn, Y/N)
    /// </summary>
    public string MailYn { get; set; } = "N";

    /// <summary>
    /// 승인 필요 여부 (approv_yn, Y/N)
    /// </summary>
    public string ApprovYn { get; set; } = "N";

    /// <summary>
    /// 사용 여부 (use_yn, Y/N)
    /// </summary>
    public string UseYn { get; set; } = "Y";

    // ─────────────────────────────────────────────────────────────
    // Audit Trail (8개)
    // ─────────────────────────────────────────────────────────────

    /// <summary>
    /// 작업명 (acti_name)
    /// </summary>
    public string ActiName { get; set; } = string.Empty;

    /// <summary>
    /// 원본 작업명 (origin_acti_name)
    /// </summary>
    public string OriginActiName { get; set; } = string.Empty;

    /// <summary>
    /// 사유 코드 (reason_code)
    /// </summary>
    public string ReasonCode { get; set; } = string.Empty;

    /// <summary>
    /// 설명 (description)
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// 생성자 ID (create_user_id)
    /// </summary>
    public string CreateUserId { get; set; } = string.Empty;

    /// <summary>
    /// 생성일시 (create_date)
    /// </summary>
    public DateTime? CreateDate { get; set; }

    /// <summary>
    /// 수정자 ID (update_user_id)
    /// </summary>
    public string UpdateUserId { get; set; } = string.Empty;

    /// <summary>
    /// 수정일시 (update_date)
    /// </summary>
    public DateTime UpdateDate { get; set; }
}
