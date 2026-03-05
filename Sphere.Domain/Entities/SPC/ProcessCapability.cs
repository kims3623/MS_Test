using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.SPC;

/// <summary>
/// Process capability entity for capability study results.
/// </summary>
public class ProcessCapability : SphereEntity
{
    public string StudyId { get; set; } = string.Empty;
    public string SpecSysId { get; set; } = string.Empty;
    public string PeriodFrom { get; set; } = string.Empty;
    public string PeriodTo { get; set; } = string.Empty;
    public int SampleSize { get; set; }
    public decimal? Mean { get; set; }
    public decimal? StdDev { get; set; }
    public decimal? Usl { get; set; }
    public decimal? Lsl { get; set; }
    public decimal? Target { get; set; }
    public decimal? Cp { get; set; }
    public decimal? Cpk { get; set; }
    public decimal? Cpu { get; set; }
    public decimal? Cpl { get; set; }
    public decimal? Pp { get; set; }
    public decimal? Ppk { get; set; }
    public decimal? Cpm { get; set; }
    public string StudyStatus { get; set; } = string.Empty;
}
