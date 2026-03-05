using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.Common;

/// <summary>
/// Notification list entity for managing system notifications.
/// Maps to SPC_NOTIFY_LIST table.
/// </summary>
/// <remarks>
/// PK: TableSysId (auto-increment)
/// Stores notification information including alarm and approval references.
/// Tracks send status and error information.
/// </remarks>
public class NotifyList : SphereEntity
{
    // ─────────────────────────────────────────────────────────────
    // Identity
    // ─────────────────────────────────────────────────────────────

    /// <summary>
    /// Row identifier (auto-increment, table_sys_id)
    /// </summary>
    public long TableSysId { get; set; }

    // ─────────────────────────────────────────────────────────────
    // Module
    // ─────────────────────────────────────────────────────────────

    /// <summary>
    /// Module identifier (module_id)
    /// </summary>
    public string ModuleId { get; set; } = string.Empty;

    /// <summary>
    /// Notification type identifier (noti_type_id)
    /// </summary>
    public string NotiTypeId { get; set; } = string.Empty;

    // ─────────────────────────────────────────────────────────────
    // Alarm
    // ─────────────────────────────────────────────────────────────

    /// <summary>
    /// Alarm system identifier (alm_sys_id)
    /// </summary>
    public string AlmSysId { get; set; } = string.Empty;

    /// <summary>
    /// Alarm action identifier (alm_action_id)
    /// </summary>
    public string AlmActionId { get; set; } = string.Empty;

    // ─────────────────────────────────────────────────────────────
    // Approval
    // ─────────────────────────────────────────────────────────────

    /// <summary>
    /// Approval identifier (aprov_id)
    /// </summary>
    public string AprovId { get; set; } = string.Empty;

    /// <summary>
    /// Approval action identifier (aprov_action_id)
    /// </summary>
    public string AprovActionId { get; set; } = string.Empty;

    // ─────────────────────────────────────────────────────────────
    // Content
    // ─────────────────────────────────────────────────────────────

    /// <summary>
    /// Receiver list (comma-separated)
    /// </summary>
    public string Receiver { get; set; } = string.Empty;

    /// <summary>
    /// Notification title
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Notification content
    /// </summary>
    public string Contents { get; set; } = string.Empty;

    // ─────────────────────────────────────────────────────────────
    // Status
    // ─────────────────────────────────────────────────────────────

    /// <summary>
    /// Send status flag (Y/N)
    /// </summary>
    public string SendYn { get; set; } = "N";

    /// <summary>
    /// Error flag (Y/N)
    /// </summary>
    public string ErrorYn { get; set; } = "N";

    /// <summary>
    /// Error code
    /// </summary>
    public string ErrorCode { get; set; } = string.Empty;

    /// <summary>
    /// Error message
    /// </summary>
    public string ErrorMsg { get; set; } = string.Empty;

    // ─────────────────────────────────────────────────────────────
    // Audit Trail
    // ─────────────────────────────────────────────────────────────

    /// <summary>
    /// Action name
    /// </summary>
    public string ActiName { get; set; } = string.Empty;

    /// <summary>
    /// Original action name
    /// </summary>
    public string OriginActiName { get; set; } = string.Empty;

    /// <summary>
    /// Reason code
    /// </summary>
    public string ReasonCode { get; set; } = string.Empty;

    /// <summary>
    /// Description
    /// </summary>
    public string Description { get; set; } = string.Empty;
}
