using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.SPC;

/// <summary>
/// Statistics calculation entity for storing SPC statistics.
/// Maps to SPC_STATISTICS_CALC table.
/// </summary>
public class StatisticsCalc : SphereEntity
{
    /// <summary>
    /// Calculation identifier (PK)
    /// </summary>
    public string CalcId { get; set; } = string.Empty;

    /// <summary>
    /// Specification system identifier (FK)
    /// </summary>
    public string SpecSysId { get; set; } = string.Empty;

    /// <summary>
    /// Calculation period start date
    /// </summary>
    public string PeriodFrom { get; set; } = string.Empty;

    /// <summary>
    /// Calculation period end date
    /// </summary>
    public string PeriodTo { get; set; } = string.Empty;

    /// <summary>
    /// Sample count
    /// </summary>
    public int SampleCount { get; set; }

    /// <summary>
    /// Mean (X-bar)
    /// </summary>
    public decimal? Mean { get; set; }

    /// <summary>
    /// Range (R)
    /// </summary>
    public decimal? Range { get; set; }

    /// <summary>
    /// Standard deviation (S)
    /// </summary>
    public decimal? StdDev { get; set; }

    /// <summary>
    /// Variance
    /// </summary>
    public decimal? Variance { get; set; }

    /// <summary>
    /// Cp value
    /// </summary>
    public decimal? Cp { get; set; }

    /// <summary>
    /// Cpk value
    /// </summary>
    public decimal? Cpk { get; set; }

    /// <summary>
    /// Pp value
    /// </summary>
    public decimal? Pp { get; set; }

    /// <summary>
    /// Ppk value
    /// </summary>
    public decimal? Ppk { get; set; }

    /// <summary>
    /// Cpm value
    /// </summary>
    public decimal? Cpm { get; set; }

    /// <summary>
    /// Upper control limit
    /// </summary>
    public decimal? Ucl { get; set; }

    /// <summary>
    /// Center line
    /// </summary>
    public decimal? Cl { get; set; }

    /// <summary>
    /// Lower control limit
    /// </summary>
    public decimal? Lcl { get; set; }

    /// <summary>
    /// Calculation timestamp
    /// </summary>
    public DateTime? CalcDate { get; set; }
}
