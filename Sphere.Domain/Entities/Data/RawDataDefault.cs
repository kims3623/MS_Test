using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.Data;

/// <summary>
/// Raw data default entity for default raw data templates.
/// </summary>
public class RawDataDefault : SphereEntity
{
    public string DefaultId { get; set; } = string.Empty;
    public string SpecSysId { get; set; } = string.Empty;
    public string FieldName { get; set; } = string.Empty;
    public string DefaultValue { get; set; } = string.Empty;
    public string ValueType { get; set; } = "STRING";
    public string ApplyCondition { get; set; } = string.Empty;
    public int Priority { get; set; }
}
