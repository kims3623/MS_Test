using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.TPS;

/// <summary>
/// Alarm action history entity for tracking alarm responses.
/// Maps to SPC_ALARM_ACTION_HIST table.
/// </summary>
public class AlarmActionHist : SphereEntity
{
    /// <summary>
    /// History sequence (PK)
    /// </summary>
    public int HistSeq { get; set; }

    /// <summary>
    /// Alarm system identifier (FK)
    /// </summary>
    public string AlmSysId { get; set; } = string.Empty;

    /// <summary>
    /// Action identifier
    /// </summary>
    public string ActionId { get; set; } = string.Empty;

    /// <summary>
    /// Action name
    /// </summary>
    public string ActionName { get; set; } = string.Empty;

    /// <summary>
    /// Action type (ACKNOWLEDGE, RESOLVE, ESCALATE, etc.)
    /// </summary>
    public string ActionType { get; set; } = string.Empty;

    /// <summary>
    /// Previous status
    /// </summary>
    public string PrevStatus { get; set; } = string.Empty;

    /// <summary>
    /// New status after action
    /// </summary>
    public string NewStatus { get; set; } = string.Empty;

    /// <summary>
    /// Action user identifier
    /// </summary>
    public string ActionUserId { get; set; } = string.Empty;

    /// <summary>
    /// Action user name
    /// </summary>
    public string ActionUserName { get; set; } = string.Empty;

    /// <summary>
    /// Action timestamp
    /// </summary>
    public DateTime? ActionDate { get; set; }

    /// <summary>
    /// Comment/notes
    /// </summary>
    public string Comment { get; set; } = string.Empty;

    /// <summary>
    /// Root cause analysis
    /// </summary>
    public string RootCause { get; set; } = string.Empty;

    /// <summary>
    /// Corrective action taken
    /// </summary>
    public string CorrectiveAction { get; set; } = string.Empty;
}
