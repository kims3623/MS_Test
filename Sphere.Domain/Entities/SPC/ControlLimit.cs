using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.SPC;

/// <summary>
/// Control limit entity for SPC chart control limits.
/// Maps to SPC_CONTROL_LIMIT table.
/// </summary>
public class ControlLimit : SphereEntity
{
    /// <summary>
    /// Specification system identifier (PK)
    /// </summary>
    public string SpecSysId { get; set; } = string.Empty;

    /// <summary>
    /// Limit type (UCL, LCL, CL, USL, LSL, Target)
    /// </summary>
    public string LimitType { get; set; } = string.Empty;

    /// <summary>
    /// Chart type (X-bar, R, S, etc.)
    /// </summary>
    public string ChartType { get; set; } = string.Empty;

    /// <summary>
    /// Limit value
    /// </summary>
    public decimal? LimitValue { get; set; }

    /// <summary>
    /// Effective from date
    /// </summary>
    public DateTime? EffectiveFrom { get; set; }

    /// <summary>
    /// Effective to date
    /// </summary>
    public DateTime? EffectiveTo { get; set; }

    /// <summary>
    /// Calculation method
    /// </summary>
    public string CalcMethod { get; set; } = string.Empty;

    /// <summary>
    /// Version number
    /// </summary>
    public int Version { get; set; }
}
