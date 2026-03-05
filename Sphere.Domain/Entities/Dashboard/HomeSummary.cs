using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.Dashboard;

/// <summary>
/// Home summary entity for dashboard KPI summary.
/// </summary>
public class HomeSummary : SphereEntity
{
    public string SummaryType { get; set; } = string.Empty;
    public string SummaryName { get; set; } = string.Empty;
    public int TotalCount { get; set; }
    public int ActiveCount { get; set; }
    public int WarningCount { get; set; }
    public int CriticalCount { get; set; }
    public decimal? AvgCpk { get; set; }
    public decimal? TargetCpk { get; set; }
    public string Period { get; set; } = string.Empty;
}
