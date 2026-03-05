using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.SPC;

/// <summary>
/// X-Bar R Chart entity for statistical process control.
/// </summary>
public class XBarRChart : SphereEntity
{
    public string ChartId { get; set; } = string.Empty;
    public string SpecSysId { get; set; } = string.Empty;
    public string WorkDate { get; set; } = string.Empty;
    public decimal XBar { get; set; }
    public decimal Range { get; set; }
    public decimal Ucl { get; set; }
    public decimal Lcl { get; set; }
    public decimal Cl { get; set; }
    public decimal UclR { get; set; }
    public decimal LclR { get; set; }
    public decimal ClR { get; set; }
    public int SubgroupSize { get; set; }
    public string AlarmYn { get; set; } = "N";
    public string AlarmType { get; set; } = string.Empty;
}
