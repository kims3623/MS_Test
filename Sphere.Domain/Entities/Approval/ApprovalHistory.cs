using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.Approval;

/// <summary>
/// Approval history entity for tracking approval workflow history.
/// Maps to SPC_APROV_HIST table.
/// </summary>
public class ApprovalHistory : SphereEntity
{
    /// <summary>
    /// History sequence (PK)
    /// </summary>
    public int HistSeq { get; set; }

    /// <summary>
    /// Approval identifier (FK)
    /// </summary>
    public string AprovId { get; set; } = string.Empty;

    /// <summary>
    /// Approver user identifier
    /// </summary>
    public string ApproverId { get; set; } = string.Empty;

    /// <summary>
    /// Approver user name
    /// </summary>
    public string ApproverName { get; set; } = string.Empty;

    /// <summary>
    /// Approval action taken (APPROVE, REJECT, RETURN, etc.)
    /// </summary>
    public string Action { get; set; } = string.Empty;

    /// <summary>
    /// Action name (display)
    /// </summary>
    public string ActionName { get; set; } = string.Empty;

    /// <summary>
    /// Previous approval state
    /// </summary>
    public string PrevState { get; set; } = string.Empty;

    /// <summary>
    /// New approval state after action
    /// </summary>
    public string NewState { get; set; } = string.Empty;

    /// <summary>
    /// Comment/reason for the action
    /// </summary>
    public string Comment { get; set; } = string.Empty;

    /// <summary>
    /// Action timestamp
    /// </summary>
    public DateTime? ActionDate { get; set; }
}
