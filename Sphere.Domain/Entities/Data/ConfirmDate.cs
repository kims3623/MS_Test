using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.Data;

/// <summary>
/// Confirm date entity for tracking data confirmation status.
/// Maps to SPC_CONFIRM_DATE table.
/// </summary>
public class ConfirmDate : SphereEntity
{
    /// <summary>
    /// Specification system identifier (PK)
    /// </summary>
    public string SpecSysId { get; set; } = string.Empty;

    /// <summary>
    /// Work date (PK)
    /// </summary>
    public string WorkDate { get; set; } = string.Empty;

    /// <summary>
    /// Shift identifier (PK)
    /// </summary>
    public string Shift { get; set; } = string.Empty;

    /// <summary>
    /// Confirm status (Y/N)
    /// </summary>
    public string ConfirmYn { get; set; } = "N";

    /// <summary>
    /// Confirm user identifier
    /// </summary>
    public string ConfirmUserId { get; set; } = string.Empty;

    /// <summary>
    /// Confirm user name
    /// </summary>
    public string ConfirmUserName { get; set; } = string.Empty;

    /// <summary>
    /// Confirm timestamp
    /// </summary>
    public DateTime? ConfirmDateTime { get; set; }

    /// <summary>
    /// Remarks
    /// </summary>
    public string Remarks { get; set; } = string.Empty;
}
