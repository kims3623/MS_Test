using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.Common;

/// <summary>
/// Favorite entity for managing user menu favorites/bookmarks.
/// Maps to SPC_FAVORITE table.
/// </summary>
/// <remarks>
/// Composite PK: DivSeq + UserId + MenuId
/// Allows users to bookmark frequently accessed menus for quick access.
/// </remarks>
public class Favorite : SphereEntity
{
    /// <summary>
    /// User identifier (PK)
    /// </summary>
    public string UserId { get; set; } = string.Empty;

    /// <summary>
    /// Menu identifier (PK)
    /// </summary>
    public string MenuId { get; set; } = string.Empty;

    /// <summary>
    /// Menu name for display
    /// </summary>
    public string MenuName { get; set; } = string.Empty;

    /// <summary>
    /// Display sequence for ordering favorites
    /// </summary>
    public int DspSeq { get; set; }

    /// <summary>
    /// Timestamp when favorite was added
    /// </summary>
    public DateTime? AddedDate { get; set; }
}
