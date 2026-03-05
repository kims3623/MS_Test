using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.SPC;

/// <summary>
/// Yield specification master entity for SPC module.
/// </summary>
public class YieldSpecMaster : SphereEntity
{
    public string SpecSysId { get; set; } = string.Empty;
    public string VendorId { get; set; } = string.Empty;
    public string VendorName { get; set; } = string.Empty;
    public string MtrlClassId { get; set; } = string.Empty;
    public string MtrlClassName { get; set; } = string.Empty;
    public string ActProdId { get; set; } = string.Empty;
    public string ActProdName { get; set; } = string.Empty;
    public string YieldStepId { get; set; } = string.Empty;
    public string YieldStepName { get; set; } = string.Empty;
    public string WorkDate { get; set; } = string.Empty;
    public string Shift { get; set; } = string.Empty;
    public string ShiftName { get; set; } = string.Empty;
    public string InputQty { get; set; } = string.Empty;
    public string DefectQty { get; set; } = string.Empty;
    public string Yield { get; set; } = string.Empty;
    public string Wlcl { get; set; } = string.Empty;
    public string Mlcl { get; set; } = string.Empty;
    public string Alarm { get; set; } = string.Empty;
    public string AlmSysId { get; set; } = string.Empty;
    public string YieldCalcType { get; set; } = string.Empty;
    public string YieldCalcTypeName { get; set; } = string.Empty;
}
