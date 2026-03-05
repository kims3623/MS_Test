using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.SPC;

/// <summary>
/// SPH3020 daily chart entity for daily aggregated data.
/// Maps to SPH3020 screen daily view data.
/// </summary>
public class SPH3020Day : SphereEntity
{
    /// <summary>
    /// Specification system identifier
    /// </summary>
    public string SpecSysId { get; set; } = string.Empty;

    /// <summary>
    /// Work date (PK)
    /// </summary>
    public string WorkDate { get; set; } = string.Empty;

    /// <summary>
    /// Sample count for the day
    /// </summary>
    public int SampleCount { get; set; }

    /// <summary>
    /// Daily average
    /// </summary>
    public decimal? DailyAvg { get; set; }

    /// <summary>
    /// Daily minimum
    /// </summary>
    public decimal? DailyMin { get; set; }

    /// <summary>
    /// Daily maximum
    /// </summary>
    public decimal? DailyMax { get; set; }

    /// <summary>
    /// Daily range
    /// </summary>
    public decimal? DailyRange { get; set; }

    /// <summary>
    /// Daily standard deviation
    /// </summary>
    public decimal? DailyStdDev { get; set; }

    /// <summary>
    /// Daily Cp
    /// </summary>
    public decimal? DailyCp { get; set; }

    /// <summary>
    /// Daily Cpk
    /// </summary>
    public decimal? DailyCpk { get; set; }

    /// <summary>
    /// Out of spec count
    /// </summary>
    public int OosCount { get; set; }

    /// <summary>
    /// Alarm count
    /// </summary>
    public int AlarmCount { get; set; }
}
