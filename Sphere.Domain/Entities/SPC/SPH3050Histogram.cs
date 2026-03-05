using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.SPC;

/// <summary>
/// SPH3050 Histogram chart entity for distribution analysis.
/// Maps to SPH3050 screen histogram data.
/// </summary>
public class SPH3050Histogram : SphereEntity
{
    /// <summary>
    /// Specification system identifier
    /// </summary>
    public string SpecSysId { get; set; } = string.Empty;

    /// <summary>
    /// Period start date
    /// </summary>
    public string PeriodFrom { get; set; } = string.Empty;

    /// <summary>
    /// Period end date
    /// </summary>
    public string PeriodTo { get; set; } = string.Empty;

    /// <summary>
    /// Bin number (sequence)
    /// </summary>
    public int BinNo { get; set; }

    /// <summary>
    /// Bin lower bound
    /// </summary>
    public decimal? BinLower { get; set; }

    /// <summary>
    /// Bin upper bound
    /// </summary>
    public decimal? BinUpper { get; set; }

    /// <summary>
    /// Frequency count
    /// </summary>
    public int Frequency { get; set; }

    /// <summary>
    /// Relative frequency (percentage)
    /// </summary>
    public decimal? RelativeFrequency { get; set; }

    /// <summary>
    /// Cumulative frequency
    /// </summary>
    public int CumulativeFrequency { get; set; }

    /// <summary>
    /// Total sample count
    /// </summary>
    public int TotalCount { get; set; }

    /// <summary>
    /// Mean value
    /// </summary>
    public decimal? Mean { get; set; }

    /// <summary>
    /// Standard deviation
    /// </summary>
    public decimal? StdDev { get; set; }
}
