using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.Common;

/// <summary>
/// Dropdown item entity for dropdown/select list options.
/// Maps to SPC_DROPDOWN_ITEM table.
/// </summary>
/// <remarks>
/// Composite PK: DivSeq + ItemId
/// Note: ItemId is used instead of DropdownId to match table PK structure.
/// Supports cascading dropdowns through parent code relationship.
/// </remarks>
public class DropdownItem : SphereEntity
{
    /// <summary>
    /// Item identifier (PK)
    /// </summary>
    public string ItemId { get; set; } = string.Empty;

    /// <summary>
    /// Dropdown type/category
    /// </summary>
    public string DropdownType { get; set; } = string.Empty;

    /// <summary>
    /// Item code value
    /// </summary>
    public string ItemCode { get; set; } = string.Empty;

    /// <summary>
    /// Item display name (default locale)
    /// </summary>
    public string ItemName { get; set; } = string.Empty;

    /// <summary>
    /// Item name in Korean
    /// </summary>
    public string ItemNameK { get; set; } = string.Empty;

    /// <summary>
    /// Item name in English
    /// </summary>
    public string ItemNameE { get; set; } = string.Empty;

    /// <summary>
    /// Display sequence
    /// </summary>
    public int DspSeq { get; set; }

    /// <summary>
    /// Parent item code for cascading dropdowns
    /// </summary>
    public string ParentCode { get; set; } = string.Empty;
}
