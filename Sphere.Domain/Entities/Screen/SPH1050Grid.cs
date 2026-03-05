using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.Screen;

/// <summary>
/// SPH1050 Grid entity for spec detail grid.
/// </summary>
public class SPH1050Grid : SphereEntity
{
    public string SpecSysId { get; set; } = string.Empty;
    public string DtType { get; set; } = string.Empty;
    public string DtTypeName { get; set; } = string.Empty;
    public string LlValue { get; set; } = string.Empty;
    public string ClValue { get; set; } = string.Empty;
    public string UlValue { get; set; } = string.Empty;
    public string LclValue { get; set; } = string.Empty;
    public string UclValue { get; set; } = string.Empty;
    public string TargetValue { get; set; } = string.Empty;
    public string RunruleId { get; set; } = string.Empty;
    public string RunruleName { get; set; } = string.Empty;
    public int SpecVer { get; set; }
    public string AprovId { get; set; } = string.Empty;
    public string ApprovalYn { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Remarks { get; set; } = string.Empty;
}
