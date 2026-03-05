using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.Common;

/// <summary>
/// Audit log entity for tracking system activities and changes.
/// Maps to SPC_AUDIT_LOG table.
/// </summary>
/// <remarks>
/// Composite PK: DivSeq + LogId
/// Records user actions, entity changes, and system events for compliance and debugging.
/// Stores both old and new values for change tracking.
/// </remarks>
public class AuditLog : SphereEntity
{
    /// <summary>
    /// Audit log identifier (PK)
    /// </summary>
    public string LogId { get; set; } = string.Empty;

    /// <summary>
    /// User identifier who performed the action
    /// </summary>
    public string UserId { get; set; } = string.Empty;

    /// <summary>
    /// User display name
    /// </summary>
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// Action type (CREATE, UPDATE, DELETE, LOGIN, etc.)
    /// </summary>
    public string Action { get; set; } = string.Empty;

    /// <summary>
    /// Target entity type
    /// </summary>
    public string EntityType { get; set; } = string.Empty;

    /// <summary>
    /// Target entity identifier
    /// </summary>
    public string EntityId { get; set; } = string.Empty;

    /// <summary>
    /// Previous value before change (JSON)
    /// </summary>
    public string OldValue { get; set; } = string.Empty;

    /// <summary>
    /// New value after change (JSON)
    /// </summary>
    public string NewValue { get; set; } = string.Empty;

    /// <summary>
    /// Client IP address
    /// </summary>
    public string IpAddress { get; set; } = string.Empty;

    /// <summary>
    /// Client user agent string
    /// </summary>
    public string UserAgent { get; set; } = string.Empty;

    /// <summary>
    /// Timestamp when action occurred
    /// </summary>
    public DateTime? ActionDate { get; set; }
}
