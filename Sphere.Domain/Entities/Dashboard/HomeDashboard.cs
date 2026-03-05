using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.Dashboard;

/// <summary>
/// Home dashboard entity for main dashboard data.
/// </summary>
public class HomeDashboard : SphereEntity
{
    public string WidgetId { get; set; } = string.Empty;
    public string WidgetType { get; set; } = string.Empty;
    public string WidgetTitle { get; set; } = string.Empty;
    public string WidgetData { get; set; } = string.Empty;
    public int TotalAlarmCount { get; set; }
    public int PendingIssueCount { get; set; }
    public int ResolvedCount { get; set; }
    public decimal AlarmRate { get; set; }
    public string LastUpdateDate { get; set; } = string.Empty;
    public string RefreshInterval { get; set; } = "300";
    public string ChartType { get; set; } = string.Empty;
    public string ChartConfig { get; set; } = string.Empty;
}
