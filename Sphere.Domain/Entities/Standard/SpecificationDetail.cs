using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.Standard;

/// <summary>
/// Specification detail entity for detailed spec parameters.
/// Maps to SPC_SPEC_DETAIL table.
/// </summary>
public class SpecificationDetail : SphereEntity
{
    /// <summary>
    /// Specification system identifier (PK)
    /// </summary>
    public string SpecSysId { get; set; } = string.Empty;

    /// <summary>
    /// Detail sequence (PK)
    /// </summary>
    public int DetailSeq { get; set; }

    /// <summary>
    /// Parameter name
    /// </summary>
    public string ParamName { get; set; } = string.Empty;

    /// <summary>
    /// Parameter value
    /// </summary>
    public string ParamValue { get; set; } = string.Empty;

    /// <summary>
    /// Parameter type
    /// </summary>
    public string ParamType { get; set; } = string.Empty;

    /// <summary>
    /// Minimum value
    /// </summary>
    public decimal? MinValue { get; set; }

    /// <summary>
    /// Maximum value
    /// </summary>
    public decimal? MaxValue { get; set; }

    /// <summary>
    /// Target value
    /// </summary>
    public decimal? TargetValue { get; set; }

    /// <summary>
    /// Unit of measurement
    /// </summary>
    public string Unit { get; set; } = string.Empty;

    /// <summary>
    /// Description
    /// </summary>
    public string Description { get; set; } = string.Empty;
}
