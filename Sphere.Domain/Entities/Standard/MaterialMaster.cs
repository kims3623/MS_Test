using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.Standard;

/// <summary>
/// Material master entity for managing materials.
/// Maps to SPC_MTRL_MST table.
/// </summary>
public class MaterialMaster : SphereEntity
{
    /// <summary>
    /// Material identifier (PK)
    /// </summary>
    public string MtrlId { get; set; } = string.Empty;

    /// <summary>
    /// Material name (default/display)
    /// </summary>
    public string MtrlName { get; set; } = string.Empty;

    /// <summary>
    /// Material name in Korean
    /// </summary>
    public string MtrlNameK { get; set; } = string.Empty;

    /// <summary>
    /// Material name in English
    /// </summary>
    public string MtrlNameE { get; set; } = string.Empty;

    /// <summary>
    /// Material name in Chinese
    /// </summary>
    public string MtrlNameC { get; set; } = string.Empty;

    /// <summary>
    /// Material name in Vietnamese
    /// </summary>
    public string MtrlNameV { get; set; } = string.Empty;

    /// <summary>
    /// Material class identifier (FK)
    /// </summary>
    public string MtrlClassId { get; set; } = string.Empty;

    /// <summary>
    /// Material class name
    /// </summary>
    public string MtrlClassName { get; set; } = string.Empty;

    /// <summary>
    /// Material type code
    /// </summary>
    public string MtrlType { get; set; } = string.Empty;

    /// <summary>
    /// Material specification
    /// </summary>
    public string MtrlSpec { get; set; } = string.Empty;

    /// <summary>
    /// Unit of measurement
    /// </summary>
    public string Unit { get; set; } = string.Empty;

    /// <summary>
    /// Vendor identifier (FK)
    /// </summary>
    public string VendorId { get; set; } = string.Empty;

    /// <summary>
    /// Vendor name
    /// </summary>
    public string VendorName { get; set; } = string.Empty;

    /// <summary>
    /// Display sequence for ordering
    /// </summary>
    public int DspSeq { get; set; }

    /// <summary>
    /// Description of the material
    /// </summary>
    public string Description { get; set; } = string.Empty;
}
