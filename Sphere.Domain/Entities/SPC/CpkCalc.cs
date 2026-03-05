using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.SPC;

/// <summary>
/// Cpk calculation entity for process capability analysis.
/// </summary>
public class CpkCalc : SphereEntity
{
    public string CalcId { get; set; } = string.Empty;
    public string SpecSysId { get; set; } = string.Empty;
    public decimal Cp { get; set; }
    public decimal Cpk { get; set; }
    public decimal CpU { get; set; }
    public decimal CpL { get; set; }
    public decimal Pp { get; set; }
    public decimal Ppk { get; set; }
    public decimal Mean { get; set; }
    public decimal StdDev { get; set; }
}
