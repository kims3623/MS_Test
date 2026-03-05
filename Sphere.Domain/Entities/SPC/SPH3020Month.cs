using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.SPC;

/// <summary>
/// SPH3020 monthly chart entity for monthly aggregated data.
/// Maps to SPH3020 screen monthly view data.
/// </summary>
public class SPH3020Month : SphereEntity
{
    /// <summary>
    /// Specification system identifier
    /// </summary>
    public string SpecSysId { get; set; } = string.Empty;

    /// <summary>
    /// Year (PK)
    /// </summary>
    public string Year { get; set; } = string.Empty;

    /// <summary>
    /// Month (PK)
    /// </summary>
    public string Month { get; set; } = string.Empty;

    /// <summary>
    /// Sample count for the month
    /// </summary>
    public int SampleCount { get; set; }

    /// <summary>
    /// Monthly average
    /// </summary>
    public decimal? MonthlyAvg { get; set; }

    /// <summary>
    /// Monthly minimum
    /// </summary>
    public decimal? MonthlyMin { get; set; }

    /// <summary>
    /// Monthly maximum
    /// </summary>
    public decimal? MonthlyMax { get; set; }

    /// <summary>
    /// Monthly range
    /// </summary>
    public decimal? MonthlyRange { get; set; }

    /// <summary>
    /// Monthly standard deviation
    /// </summary>
    public decimal? MonthlyStdDev { get; set; }

    /// <summary>
    /// Monthly Cp
    /// </summary>
    public decimal? MonthlyCp { get; set; }

    /// <summary>
    /// Monthly Cpk
    /// </summary>
    public decimal? MonthlyCpk { get; set; }

    /// <summary>
    /// Out of spec count
    /// </summary>
    public int OosCount { get; set; }

    /// <summary>
    /// Alarm count
    /// </summary>
    public int AlarmCount { get; set; }
}
