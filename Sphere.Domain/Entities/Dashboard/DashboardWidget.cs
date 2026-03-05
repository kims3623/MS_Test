using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.Dashboard;

/// <summary>
/// Dashboard widget entity for configurable dashboard widgets.
/// </summary>
public class DashboardWidget : SphereEntity
{
    public string WidgetId { get; set; } = string.Empty;
    public string WidgetName { get; set; } = string.Empty;
    public string WidgetType { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public int PositionX { get; set; }
    public int PositionY { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public string ConfigJson { get; set; } = string.Empty;
    public string RefreshInterval { get; set; } = "300";
}
