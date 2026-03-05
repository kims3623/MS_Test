using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.Standard;

/// <summary>
/// Statistics type entity for statistical calculation types.
/// </summary>
public class StatType : SphereEntity
{
    public string StatTypeId { get; set; } = string.Empty;
    public string StatTypeName { get; set; } = string.Empty;
    public string StatTypeNameK { get; set; } = string.Empty;
    public string StatTypeNameE { get; set; } = string.Empty;
    public string CalcFormula { get; set; } = string.Empty;
    public string ChartType { get; set; } = string.Empty;
    public int DspSeq { get; set; }
    public string Description { get; set; } = string.Empty;
}
