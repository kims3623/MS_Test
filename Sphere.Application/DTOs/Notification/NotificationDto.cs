namespace Sphere.Application.DTOs.Notification;

/// <summary>
/// 알림 목록 DTO (SPC_NOTIFY_LIST 매핑)
/// </summary>
/// <remarks>
/// SPC_NOTIFY_LIST 테이블의 24개 컬럼을 매핑합니다.
/// 알림 발송 및 조회에 사용됩니다.
/// </remarks>
public class NotificationDto
{
    // ─────────────────────────────────────────────────────────────
    // Identity (2개)
    // ─────────────────────────────────────────────────────────────

    /// <summary>
    /// 행 식별자 (table_sys_id)
    /// </summary>
    public long TableSysId { get; set; }

    /// <summary>
    /// 사업부 코드 (div_seq)
    /// </summary>
    public string DivSeq { get; set; } = string.Empty;

    // ─────────────────────────────────────────────────────────────
    // Module (2개)
    // ─────────────────────────────────────────────────────────────

    /// <summary>
    /// 모듈 ID (module_id)
    /// </summary>
    public string ModuleId { get; set; } = string.Empty;

    /// <summary>
    /// 알림 유형 ID (noti_type_id)
    /// </summary>
    public string NotiTypeId { get; set; } = string.Empty;

    // ─────────────────────────────────────────────────────────────
    // Alarm (2개)
    // ─────────────────────────────────────────────────────────────

    /// <summary>
    /// 알람 시스템 ID (alm_sys_id)
    /// </summary>
    public string AlmSysId { get; set; } = string.Empty;

    /// <summary>
    /// 알람 액션 ID (alm_action_id)
    /// </summary>
    public string AlmActionId { get; set; } = string.Empty;

    // ─────────────────────────────────────────────────────────────
    // Approval (2개)
    // ─────────────────────────────────────────────────────────────

    /// <summary>
    /// 승인 ID (aprov_id)
    /// </summary>
    public string AprovId { get; set; } = string.Empty;

    /// <summary>
    /// 승인 액션 ID (aprov_action_id)
    /// </summary>
    public string AprovActionId { get; set; } = string.Empty;

    // ─────────────────────────────────────────────────────────────
    // Content (3개)
    // ─────────────────────────────────────────────────────────────

    /// <summary>
    /// 수신자 목록 (receiver, 쉼표 구분)
    /// </summary>
    public string Receiver { get; set; } = string.Empty;

    /// <summary>
    /// 알림 제목 (title)
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// 알림 내용 (contents)
    /// </summary>
    public string Contents { get; set; } = string.Empty;

    // ─────────────────────────────────────────────────────────────
    // Status (4개)
    // ─────────────────────────────────────────────────────────────

    /// <summary>
    /// 발송 여부 (send_yn, Y/N)
    /// </summary>
    public string SendYn { get; set; } = "N";

    /// <summary>
    /// 오류 여부 (error_yn, Y/N)
    /// </summary>
    public string ErrorYn { get; set; } = "N";

    /// <summary>
    /// 오류 코드 (error_code)
    /// </summary>
    public string ErrorCode { get; set; } = string.Empty;

    /// <summary>
    /// 오류 메시지 (error_msg)
    /// </summary>
    public string ErrorMsg { get; set; } = string.Empty;

    // ─────────────────────────────────────────────────────────────
    // Common (1개)
    // ─────────────────────────────────────────────────────────────

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
