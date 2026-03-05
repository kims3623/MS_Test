using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.Standard;

/// <summary>
/// PIC (Person In Charge) type entity.
/// </summary>
public class PicType : SphereEntity
{
    public string PicTypeId { get; set; } = string.Empty;
    public string PicTypeName { get; set; } = string.Empty;
    public string PicTypeNameK { get; set; } = string.Empty;
    public string PicTypeNameE { get; set; } = string.Empty;
    public string ResponsibilityArea { get; set; } = string.Empty;
    public int DspSeq { get; set; }
    public string Description { get; set; } = string.Empty;
}
