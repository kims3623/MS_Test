using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.SPC;

/// <summary>
/// Chart point entity for individual data points in SPC charts.
/// </summary>
public class ChartPoint : SphereEntity
{
    public string PointId { get; set; } = string.Empty;
    public string SeriesId { get; set; } = string.Empty;
    public decimal XValue { get; set; }
    public decimal YValue { get; set; }
    public string Label { get; set; } = string.Empty;
    public string Marker { get; set; } = string.Empty;
}
