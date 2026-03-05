using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.Standard;

/// <summary>
/// Yield specification entity for managing yield targets.
/// Maps to SPC_YIELD_SPEC table.
/// </summary>
public class YieldSpec : SphereEntity
{
    /// <summary>
    /// Specification system identifier (PK)
    /// </summary>
    public string SpecSysId { get; set; } = string.Empty;

    /// <summary>
    /// Vendor identifier (PK)
    /// </summary>
    public string VendorId { get; set; } = string.Empty;

    /// <summary>
    /// Material class identifier (PK)
    /// </summary>
    public string MtrlClassId { get; set; } = string.Empty;

    /// <summary>
    /// Yield step identifier (PK)
    /// </summary>
    public string YieldStepId { get; set; } = string.Empty;

    /// <summary>
    /// Vendor name
    /// </summary>
    public string VendorName { get; set; } = string.Empty;

    /// <summary>
    /// Material class name
    /// </summary>
    public string MtrlClassName { get; set; } = string.Empty;

    /// <summary>
    /// Yield step name
    /// </summary>
    public string YieldStepName { get; set; } = string.Empty;

    /// <summary>
    /// Target yield percentage
    /// </summary>
    public decimal? TargetYield { get; set; }

    /// <summary>
    /// Lower limit yield
    /// </summary>
    public decimal? LowerLimit { get; set; }

    /// <summary>
    /// Upper limit yield
    /// </summary>
    public decimal? UpperLimit { get; set; }

    /// <summary>
    /// Unit of measurement
    /// </summary>
    public string Unit { get; set; } = string.Empty;

    /// <summary>
    /// Effective start date
    /// </summary>
    public DateTime? EffectiveFrom { get; set; }

    /// <summary>
    /// Effective end date
    /// </summary>
    public DateTime? EffectiveTo { get; set; }

    /// <summary>
    /// Description
    /// </summary>
    public string Description { get; set; } = string.Empty;
}
