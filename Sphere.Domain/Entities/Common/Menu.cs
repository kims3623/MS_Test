using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.Common;

/// <summary>
/// Menu entity for managing application navigation menus.
/// Maps to SPC_MENU table.
/// </summary>
/// <remarks>
/// Composite PK: DivSeq + MenuId
/// Supports hierarchical menu structure with parent-child relationships.
/// Includes multi-language support (Korean, English, Chinese, Vietnamese).
/// </remarks>
public class Menu : SphereEntity
{
    /// <summary>
    /// Menu identifier (PK)
    /// </summary>
    public string MenuId { get; set; } = string.Empty;

    /// <summary>
    /// Menu name (default locale)
    /// </summary>
    public string MenuName { get; set; } = string.Empty;

    /// <summary>
    /// Menu name in Korean
    /// </summary>
    public string MenuNameK { get; set; } = string.Empty;

    /// <summary>
    /// Menu name in English
    /// </summary>
    public string MenuNameE { get; set; } = string.Empty;

    /// <summary>
    /// Menu name in Chinese
    /// </summary>
    public string MenuNameC { get; set; } = string.Empty;

    /// <summary>
    /// Menu name in Vietnamese
    /// </summary>
    public string MenuNameV { get; set; } = string.Empty;

    /// <summary>
    /// Parent menu identifier for hierarchical structure
    /// </summary>
    public string ParentMenuId { get; set; } = string.Empty;

    /// <summary>
    /// Menu level in hierarchy (1=Top, 2=Sub, 3=Leaf)
    /// </summary>
    public int MenuLevel { get; set; }

    /// <summary>
    /// Display sequence within parent
    /// </summary>
    public int DspSeq { get; set; }

    /// <summary>
    /// Menu URL/route path
    /// </summary>
    public string MenuUrl { get; set; } = string.Empty;

    /// <summary>
    /// Icon class name or identifier
    /// </summary>
    public string Icon { get; set; } = string.Empty;

    /// <summary>
    /// Menu description
    /// </summary>
    public string Description { get; set; } = string.Empty;
}
