using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.Common;

/// <summary>
/// User favorite entity for managing user menu favorites/bookmarks.
/// Maps to SPC_USER_FAVORITE table.
/// </summary>
/// <remarks>
/// Composite PK: DivSeq + UserId + MenuId
/// Stores user's favorite menus with display order.
/// Supports logical delete via UseYn flag.
/// </remarks>
public class UserFavorite : SphereEntity
{
    /// <summary>
    /// Row identifier (auto-increment, table_sys_id)
    /// </summary>
    public long TableSysId { get; set; }

    /// <summary>
    /// User identifier (PK)
    /// </summary>
    public string UserId { get; set; } = string.Empty;

    /// <summary>
    /// Menu identifier (PK)
    /// </summary>
    public string MenuId { get; set; } = string.Empty;

    /// <summary>
    /// Display sequence for ordering favorites
    /// </summary>
    public int DspSeq { get; set; }

    /// <summary>
    /// Timestamp when favorite was added
    /// </summary>
    public DateTime? AddedDate { get; set; }

    /// <summary>
    /// Reference to Menu entity (optional navigation property)
    /// </summary>
    public virtual Menu? Menu { get; set; }
}
