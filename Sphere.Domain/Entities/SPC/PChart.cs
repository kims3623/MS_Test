using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.SPC;

/// <summary>
/// P-Chart entity for proportion defective control charts.
/// </summary>
public class PChart : SphereEntity
{
    public string ChartId { get; set; } = string.Empty;
    public string SpecSysId { get; set; } = string.Empty;
    public string WorkDate { get; set; } = string.Empty;
    public int SampleSize { get; set; }
    public int DefectCount { get; set; }
    public decimal Proportion { get; set; }
    public decimal Ucl { get; set; }
    public decimal Lcl { get; set; }
    public decimal Cl { get; set; }
    public string AlarmYn { get; set; } = "N";
    public string AlarmType { get; set; } = string.Empty;
}
