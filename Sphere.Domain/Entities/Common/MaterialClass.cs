using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.Common;

/// <summary>
/// Material class entity for categorizing materials.
/// Maps to SPC_MTRL_CLASS table.
/// </summary>
public class MaterialClass : SphereEntity
{
    /// <summary>
    /// Material class identifier (PK)
    /// </summary>
    public string MtrlClassId { get; set; } = string.Empty;

    /// <summary>
    /// Material class name (default/display)
    /// </summary>
    public string MtrlClassName { get; set; } = string.Empty;

    /// <summary>
    /// Material class name in Korean
    /// </summary>
    public string MtrlClassNameK { get; set; } = string.Empty;

    /// <summary>
    /// Material class name in English
    /// </summary>
    public string MtrlClassNameE { get; set; } = string.Empty;

    /// <summary>
    /// Material class name in Chinese
    /// </summary>
    public string MtrlClassNameC { get; set; } = string.Empty;

    /// <summary>
    /// Material class name in Vietnamese
    /// </summary>
    public string MtrlClassNameV { get; set; } = string.Empty;

    /// <summary>
    /// Parent group identifier for hierarchical classification
    /// </summary>
    public string MtrlClassGroupId { get; set; } = string.Empty;

    /// <summary>
    /// Parent group name
    /// </summary>
    public string MtrlClassGroupName { get; set; } = string.Empty;
}
