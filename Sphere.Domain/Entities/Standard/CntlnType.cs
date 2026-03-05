using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.Standard;

/// <summary>
/// Control line type entity for SPC chart control line types.
/// </summary>
public class CntlnType : SphereEntity
{
    public string CntlnTypeId { get; set; } = string.Empty;
    public string CntlnTypeName { get; set; } = string.Empty;
    public string CntlnTypeNameK { get; set; } = string.Empty;
    public string CntlnTypeNameE { get; set; } = string.Empty;
    public string ChartType { get; set; } = string.Empty;
    public int DspSeq { get; set; }
    public string Description { get; set; } = string.Empty;
}
