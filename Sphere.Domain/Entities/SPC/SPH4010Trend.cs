using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.SPC;

/// <summary>
/// SPH4010 trend entity for SPC trend analysis.
/// </summary>
public class SPH4010Trend : SphereEntity
{
    public string SpecSysId { get; set; } = string.Empty;
    public string Period { get; set; } = string.Empty;
    public string PeriodType { get; set; } = "DAY";
    public decimal? AvgValue { get; set; }
    public decimal? MinValue { get; set; }
    public decimal? MaxValue { get; set; }
    public decimal? StdDev { get; set; }
    public decimal? Cpk { get; set; }
    public int SampleCount { get; set; }
    public int OosCount { get; set; }
    public decimal? TrendSlope { get; set; }
}
