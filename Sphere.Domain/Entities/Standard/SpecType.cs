using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.Standard;

/// <summary>
/// Specification type entity for categorizing specifications.
/// </summary>
public class SpecType : SphereEntity
{
    public string SpecTypeId { get; set; } = string.Empty;
    public string SpecTypeName { get; set; } = string.Empty;
    public string SpecTypeNameK { get; set; } = string.Empty;
    public string SpecTypeNameE { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public int DspSeq { get; set; }
    public string Description { get; set; } = string.Empty;
}
