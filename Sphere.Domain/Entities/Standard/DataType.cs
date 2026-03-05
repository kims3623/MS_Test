using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.Standard;

/// <summary>
/// Data type entity for defining data types in specifications.
/// </summary>
public class DataType : SphereEntity
{
    public string DtType { get; set; } = string.Empty;
    public string DtTypeName { get; set; } = string.Empty;
    public string DtTypeNameK { get; set; } = string.Empty;
    public string DtTypeNameE { get; set; } = string.Empty;
    public string DtTypeNameC { get; set; } = string.Empty;
    public string DtTypeNameV { get; set; } = string.Empty;
    public int DspSeq { get; set; }
    public string Description { get; set; } = string.Empty;
}
