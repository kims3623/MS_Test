using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.SPC;

/// <summary>
/// SPH3070 Cpk trend chart entity for capability analysis.
/// Maps to SPH3070 screen Cpk trend data.
/// </summary>
public class SPH3070Cpk : SphereEntity
{
    /// <summary>
    /// Specification system identifier
    /// </summary>
    public string SpecSysId { get; set; } = string.Empty;

    /// <summary>
    /// Period (date or month)
    /// </summary>
    public string Period { get; set; } = string.Empty;

    /// <summary>
    /// Period type (DAY, WEEK, MONTH)
    /// </summary>
    public string PeriodType { get; set; } = "DAY";

    /// <summary>
    /// Sample count
    /// </summary>
    public int SampleCount { get; set; }

    /// <summary>
    /// Cp value
    /// </summary>
    public decimal? Cp { get; set; }

    /// <summary>
    /// Cpk value
    /// </summary>
    public decimal? Cpk { get; set; }

    /// <summary>
    /// Cpu value (upper)
    /// </summary>
    public decimal? Cpu { get; set; }

    /// <summary>
    /// Cpl value (lower)
    /// </summary>
    public decimal? Cpl { get; set; }

    /// <summary>
    /// Pp value
    /// </summary>
    public decimal? Pp { get; set; }

    /// <summary>
    /// Ppk value
    /// </summary>
    public decimal? Ppk { get; set; }

    /// <summary>
    /// Mean value
    /// </summary>
    public decimal? Mean { get; set; }

    /// <summary>
    /// Standard deviation
    /// </summary>
    public decimal? StdDev { get; set; }

    /// <summary>
    /// Target Cpk
    /// </summary>
    public decimal? TargetCpk { get; set; }

    /// <summary>
    /// Pass/Fail flag based on target
    /// </summary>
    public string PassYn { get; set; } = "Y";
}
