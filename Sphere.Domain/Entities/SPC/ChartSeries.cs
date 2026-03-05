using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.SPC;

/// <summary>
/// Chart series entity for SPC chart data series.
/// </summary>
public class ChartSeries : SphereEntity
{
    public string SeriesId { get; set; } = string.Empty;
    public string ChartId { get; set; } = string.Empty;
    public string SeriesName { get; set; } = string.Empty;
    public string SeriesType { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
    public string DataKey { get; set; } = string.Empty;
    public int DisplayOrder { get; set; }
    public string Visible { get; set; } = "Y";
}
