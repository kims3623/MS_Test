namespace Sphere.Domain.Common;

/// <summary>
/// Base entity class for SPHERE entities with OutSystems compatibility.
/// Supports composite primary keys and common audit fields.
/// </summary>
public abstract class SphereEntity
{
    /// <summary>
    /// Division sequence - common partition key across all entities
    /// </summary>
    public string DivSeq { get; set; } = string.Empty;

    /// <summary>
    /// Row status flag for tracking changes
    /// </summary>
    public string RowStatus { get; set; } = string.Empty;

    /// <summary>
    /// Soft delete flag: Y = Active, N = Deleted
    /// </summary>
    public string UseYn { get; set; } = "Y";

    /// <summary>
    /// User ID who created this record
    /// </summary>
    public string CreateUserId { get; set; } = string.Empty;

    /// <summary>
    /// Record creation timestamp
    /// </summary>
    public DateTime? CreateDate { get; set; }

    /// <summary>
    /// User ID who last updated this record
    /// </summary>
    public string UpdateUserId { get; set; } = string.Empty;

    /// <summary>
    /// Last update timestamp
    /// </summary>
    public DateTime? UpdateDate { get; set; }
}
