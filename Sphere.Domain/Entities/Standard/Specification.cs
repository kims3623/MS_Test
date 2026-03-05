using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.Standard;

/// <summary>
/// Specification master entity for managing product specifications.
/// Maps to SPC_SPEC table.
/// Has composite primary key with 7 fields.
/// </summary>
public class Specification : SphereEntity
{
    /// <summary>
    /// Specification system identifier (PK)
    /// </summary>
    public string SpecSysId { get; set; } = string.Empty;

    /// <summary>
    /// Control line type identifier (PK)
    /// </summary>
    public string CntlnTypeId { get; set; } = string.Empty;

    /// <summary>
    /// Specification type identifier (PK)
    /// </summary>
    public string SpecTypeId { get; set; } = string.Empty;

    /// <summary>
    /// Specification version (PK)
    /// </summary>
    public int SpecVer { get; set; }

    /// <summary>
    /// Material class identifier (FK)
    /// </summary>
    public string MtrlClassId { get; set; } = string.Empty;

    /// <summary>
    /// Material class name
    /// </summary>
    public string MtrlClassName { get; set; } = string.Empty;

    /// <summary>
    /// Vendor identifier (FK)
    /// </summary>
    public string VendorId { get; set; } = string.Empty;

    /// <summary>
    /// Vendor name
    /// </summary>
    public string VendorName { get; set; } = string.Empty;

    /// <summary>
    /// Material identifier (FK)
    /// </summary>
    public string MtrlId { get; set; } = string.Empty;

    /// <summary>
    /// Material name
    /// </summary>
    public string MtrlName { get; set; } = string.Empty;

    /// <summary>
    /// Project identifier (FK)
    /// </summary>
    public string ProjectId { get; set; } = string.Empty;

    /// <summary>
    /// Project name
    /// </summary>
    public string ProjectName { get; set; } = string.Empty;

    /// <summary>
    /// Actual product identifier (FK)
    /// </summary>
    public string ActProdId { get; set; } = string.Empty;

    /// <summary>
    /// Actual product name
    /// </summary>
    public string ActProdName { get; set; } = string.Empty;

    /// <summary>
    /// Step identifier (FK)
    /// </summary>
    public string StepId { get; set; } = string.Empty;

    /// <summary>
    /// Step name
    /// </summary>
    public string StepName { get; set; } = string.Empty;

    /// <summary>
    /// Item identifier (FK)
    /// </summary>
    public string ItemId { get; set; } = string.Empty;

    /// <summary>
    /// Item name
    /// </summary>
    public string ItemName { get; set; } = string.Empty;

    /// <summary>
    /// Measurement method identifier (FK)
    /// </summary>
    public string MeasmId { get; set; } = string.Empty;

    /// <summary>
    /// Measurement method name
    /// </summary>
    public string MeasmName { get; set; } = string.Empty;

    /// <summary>
    /// Sample count
    /// </summary>
    public string SplCnt { get; set; } = string.Empty;

    /// <summary>
    /// Equipment identifier (FK)
    /// </summary>
    public string EqpId { get; set; } = string.Empty;

    /// <summary>
    /// Production line
    /// </summary>
    public string Line { get; set; } = string.Empty;

    /// <summary>
    /// Mold count
    /// </summary>
    public string MoldCnt { get; set; } = string.Empty;

    /// <summary>
    /// Cavity count
    /// </summary>
    public string Cavity { get; set; } = string.Empty;

    /// <summary>
    /// Needle value
    /// </summary>
    public string Needle { get; set; } = string.Empty;

    /// <summary>
    /// Angle value
    /// </summary>
    public string Angle { get; set; } = string.Empty;

    /// <summary>
    /// Statistics type
    /// </summary>
    public string Statistics { get; set; } = string.Empty;

    /// <summary>
    /// Lower limit value
    /// </summary>
    public string LlValue { get; set; } = string.Empty;

    /// <summary>
    /// Center line value
    /// </summary>
    public string ClValue { get; set; } = string.Empty;

    /// <summary>
    /// Upper limit value
    /// </summary>
    public string UlValue { get; set; } = string.Empty;

    /// <summary>
    /// Lower spec limit
    /// </summary>
    public string LslValue { get; set; } = string.Empty;

    /// <summary>
    /// Target value
    /// </summary>
    public string TargetValue { get; set; } = string.Empty;

    /// <summary>
    /// Upper spec limit
    /// </summary>
    public string UslValue { get; set; } = string.Empty;

    /// <summary>
    /// Approval identifier
    /// </summary>
    public string AprovId { get; set; } = string.Empty;

    /// <summary>
    /// Description
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Remarks
    /// </summary>
    public string Remarks { get; set; } = string.Empty;
}
