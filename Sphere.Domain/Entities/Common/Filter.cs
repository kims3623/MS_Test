using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.Common;

/// <summary>
/// Filter entity for managing saved filter configurations.
/// Maps to SPC_FILTER table.
/// </summary>
/// <remarks>
/// Composite PK: DivSeq + FilterId
/// Stores reusable filter criteria as JSON for screen-specific data filtering.
/// Supports both private (user-specific) and public (shared) filters.
/// </remarks>
public class Filter : SphereEntity
{
    /// <summary>
    /// Filter identifier (PK)
    /// </summary>
    public string FilterId { get; set; } = string.Empty;

    /// <summary>
    /// Filter display name
    /// </summary>
    public string FilterName { get; set; } = string.Empty;

    /// <summary>
    /// Owner user identifier
    /// </summary>
    public string UserId { get; set; } = string.Empty;

    /// <summary>
    /// Target screen/page identifier
    /// </summary>
    public string ScreenId { get; set; } = string.Empty;

    /// <summary>
    /// Filter criteria stored as JSON
    /// </summary>
    public string FilterJson { get; set; } = string.Empty;

    /// <summary>
    /// Public filter flag (Y=Shared, N=Private)
    /// </summary>
    public string PublicYn { get; set; } = "N";

    /// <summary>
    /// Default filter flag (Y=Default for screen)
    /// </summary>
    public string DefaultYn { get; set; } = "N";

    /// <summary>
    /// Display sequence for ordering filters
    /// </summary>
    public int DspSeq { get; set; }
}
