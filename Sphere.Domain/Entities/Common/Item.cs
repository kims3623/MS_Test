using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.Common;

/// <summary>
/// Item entity for managing inspection items.
/// Maps to SPC_ITEM table.
/// </summary>
public class Item : SphereEntity
{
    /// <summary>
    /// Item identifier (PK)
    /// </summary>
    public string ItemId { get; set; } = string.Empty;

    /// <summary>
    /// Item name (default/display)
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
    /// Item name in Chinese
    /// </summary>
    public string ItemNameC { get; set; } = string.Empty;

    /// <summary>
    /// Item name in Vietnamese
    /// </summary>
    public string ItemNameV { get; set; } = string.Empty;

    /// <summary>
    /// Item type code
    /// </summary>
    public string ItemType { get; set; } = string.Empty;

    /// <summary>
    /// Display sequence for ordering
    /// </summary>
    public int DspSeq { get; set; }

    /// <summary>
    /// Unit of measurement
    /// </summary>
    public string Unit { get; set; } = string.Empty;

    /// <summary>
    /// Description of the item
    /// </summary>
    public string Description { get; set; } = string.Empty;
}
