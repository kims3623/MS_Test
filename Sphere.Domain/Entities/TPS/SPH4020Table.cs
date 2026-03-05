using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.TPS;

/// <summary>
/// SPH4020 table entity for TPS screen data.
/// </summary>
public class SPH4020Table : SphereEntity
{
    public string RecordId { get; set; } = string.Empty;
    public string SpecSysId { get; set; } = string.Empty;
    public string VendorId { get; set; } = string.Empty;
    public string VendorName { get; set; } = string.Empty;
    public string MtrlClassId { get; set; } = string.Empty;
    public string MtrlClassName { get; set; } = string.Empty;
    public string PeriodFrom { get; set; } = string.Empty;
    public string PeriodTo { get; set; } = string.Empty;
    public int AlarmCount { get; set; }
    public int OosCount { get; set; }
    public decimal? AvgCpk { get; set; }
    public string Status { get; set; } = string.Empty;
}
