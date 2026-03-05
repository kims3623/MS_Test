using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.TPS;

/// <summary>
/// Alarm master entity for managing alarm definitions.
/// Maps to SPC_ALARM_MST table.
/// </summary>
public class AlarmMaster : SphereEntity
{
    /// <summary>
    /// Alarm system identifier (PK)
    /// </summary>
    public string AlmSysId { get; set; } = string.Empty;

    /// <summary>
    /// Alarm type code
    /// </summary>
    public string AlmType { get; set; } = string.Empty;

    /// <summary>
    /// Alarm type name
    /// </summary>
    public string AlmTypeName { get; set; } = string.Empty;

    /// <summary>
    /// Alarm severity (1=Low, 2=Medium, 3=High, 4=Critical)
    /// </summary>
    public int Severity { get; set; }

    /// <summary>
    /// Specification system identifier (FK)
    /// </summary>
    public string SpecSysId { get; set; } = string.Empty;

    /// <summary>
    /// Trigger condition
    /// </summary>
    public string TriggerCondition { get; set; } = string.Empty;

    /// <summary>
    /// Threshold value
    /// </summary>
    public decimal? ThresholdValue { get; set; }

    /// <summary>
    /// Current status (OPEN, ACKNOWLEDGED, RESOLVED, CLOSED)
    /// </summary>
    public string Status { get; set; } = "OPEN";

    /// <summary>
    /// Triggered timestamp
    /// </summary>
    public DateTime? TriggeredDate { get; set; }

    /// <summary>
    /// Acknowledged timestamp
    /// </summary>
    public DateTime? AcknowledgedDate { get; set; }

    /// <summary>
    /// Resolved timestamp
    /// </summary>
    public DateTime? ResolvedDate { get; set; }

    /// <summary>
    /// Assigned user identifier
    /// </summary>
    public string AssignedUserId { get; set; } = string.Empty;

    /// <summary>
    /// Assigned user name
    /// </summary>
    public string AssignedUserName { get; set; } = string.Empty;

    /// <summary>
    /// Description
    /// </summary>
    public string Description { get; set; } = string.Empty;
}
