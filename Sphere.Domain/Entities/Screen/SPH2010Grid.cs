using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.Screen;

/// <summary>
/// SPH2010 Grid entity for data input grid.
/// </summary>
public class SPH2010Grid : SphereEntity
{
    public string SpecSysId { get; set; } = string.Empty;
    public string VendorId { get; set; } = string.Empty;
    public string VendorName { get; set; } = string.Empty;
    public string MtrlClassId { get; set; } = string.Empty;
    public string MtrlClassName { get; set; } = string.Empty;
    public string MtrlId { get; set; } = string.Empty;
    public string MtrlName { get; set; } = string.Empty;
    public string ActProdId { get; set; } = string.Empty;
    public string ActProdName { get; set; } = string.Empty;
    public string StepId { get; set; } = string.Empty;
    public string StepName { get; set; } = string.Empty;
    public string ItemId { get; set; } = string.Empty;
    public string ItemName { get; set; } = string.Empty;
    public string MeasmId { get; set; } = string.Empty;
    public string MeasmName { get; set; } = string.Empty;
    public string WorkDate { get; set; } = string.Empty;
    public string Shift { get; set; } = string.Empty;
    public string ShiftName { get; set; } = string.Empty;
    public string RawDataValue { get; set; } = string.Empty;
    public int InputQty { get; set; }
    public int DefectQty { get; set; }
    public string ApprovalYn { get; set; } = string.Empty;
}
