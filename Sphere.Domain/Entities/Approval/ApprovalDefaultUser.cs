using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.Approval;

/// <summary>
/// Approval default user entity for managing default approvers.
/// Maps to SPC_APROV_DEFAULT_USER table.
/// </summary>
public class ApprovalDefaultUser : SphereEntity
{
    /// <summary>
    /// Sequence number (PK)
    /// </summary>
    public int Seq { get; set; }

    /// <summary>
    /// Change type identifier (PK)
    /// </summary>
    public string ChgTypeId { get; set; } = string.Empty;

    /// <summary>
    /// Approval action identifier (PK)
    /// </summary>
    public string AprovActionId { get; set; } = string.Empty;

    /// <summary>
    /// Writer/requester user ID
    /// </summary>
    public string Writer { get; set; } = string.Empty;

    /// <summary>
    /// Comma-separated list of default approver user IDs
    /// </summary>
    public string UserList { get; set; } = string.Empty;

    /// <summary>
    /// Action name (display)
    /// </summary>
    public string ActiName { get; set; } = string.Empty;

    /// <summary>
    /// Original action name
    /// </summary>
    public string OriginActiName { get; set; } = string.Empty;

    /// <summary>
    /// Reason code for the default assignment
    /// </summary>
    public string ReasonCode { get; set; } = string.Empty;

    /// <summary>
    /// Parallel approval group identifier
    /// </summary>
    public string ParallGroup { get; set; } = string.Empty;
}
