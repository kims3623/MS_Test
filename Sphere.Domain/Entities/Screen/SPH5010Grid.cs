using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.Screen;

/// <summary>
/// SPH5010 Grid entity for system management grid.
/// </summary>
public class SPH5010Grid : SphereEntity
{
    public string ConfigId { get; set; } = string.Empty;
    public string ConfigName { get; set; } = string.Empty;
    public string ConfigValue { get; set; } = string.Empty;
    public string ConfigType { get; set; } = string.Empty;
    public string ConfigTypeName { get; set; } = string.Empty;
    public string ConfigGroup { get; set; } = string.Empty;
    public string ConfigGroupName { get; set; } = string.Empty;
    public int DspSeq { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Remarks { get; set; } = string.Empty;
    public string ModifyYn { get; set; } = "Y";
    public string VisibleYn { get; set; } = "Y";
    public string DefaultValue { get; set; } = string.Empty;
    public string ValidationType { get; set; } = string.Empty;
    public string ValidationRule { get; set; } = string.Empty;
    public DateTime? LastModifiedDate { get; set; }
    public string LastModifiedUserId { get; set; } = string.Empty;
    public string LastModifiedUserName { get; set; } = string.Empty;
}
