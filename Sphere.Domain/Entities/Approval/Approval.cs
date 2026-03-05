using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.Approval;

/// <summary>
/// Approval entity for managing approval workflows.
/// Maps to SPC_APROV table.
/// </summary>
public class Approval : SphereEntity
{
    /// <summary>
    /// Approval identifier (PK)
    /// </summary>
    public string AprovId { get; set; } = string.Empty;

    /// <summary>
    /// Change type identifier (references CodeMaster)
    /// </summary>
    public string ChgTypeId { get; set; } = string.Empty;

    /// <summary>
    /// Change type name
    /// </summary>
    public string ChgTypeName { get; set; } = string.Empty;

    /// <summary>
    /// Approval action identifier
    /// </summary>
    public string AprovActionId { get; set; } = string.Empty;

    /// <summary>
    /// Approval action name
    /// </summary>
    public string AprovActionName { get; set; } = string.Empty;

    /// <summary>
    /// Current approval state
    /// </summary>
    public string AprovState { get; set; } = string.Empty;

    /// <summary>
    /// Approval state name
    /// </summary>
    public string AprovStateName { get; set; } = string.Empty;

    /// <summary>
    /// Approval title/subject
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Approval contents/description
    /// </summary>
    public string Contents { get; set; } = string.Empty;

    /// <summary>
    /// Request writer user ID
    /// </summary>
    public string Writer { get; set; } = string.Empty;

    /// <summary>
    /// Comma-separated list of approver user IDs
    /// </summary>
    public string UserList { get; set; } = string.Empty;

    /// <summary>
    /// Batch identifier for grouping related approvals
    /// </summary>
    public string BatchId { get; set; } = string.Empty;

    /// <summary>
    /// Alarm system identifier
    /// </summary>
    public string AlmSysId { get; set; } = string.Empty;

    /// <summary>
    /// Alarm action identifier
    /// </summary>
    public string AlmActionId { get; set; } = string.Empty;

    /// <summary>
    /// Notification flag
    /// </summary>
    public string NotiFlag { get; set; } = string.Empty;

    /// <summary>
    /// Remarks/notes
    /// </summary>
    public string Remarks { get; set; } = string.Empty;
}
