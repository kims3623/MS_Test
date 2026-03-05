using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.Screen;

/// <summary>
/// SPH2020 Grid entity for data query grid.
/// </summary>
public class SPH2020Grid : SphereEntity
{
    public string SpecSysId { get; set; } = string.Empty;
    public string VendorId { get; set; } = string.Empty;
    public string VendorName { get; set; } = string.Empty;
    public string MtrlClassId { get; set; } = string.Empty;
    public string MtrlClassName { get; set; } = string.Empty;
    public string ProjectId { get; set; } = string.Empty;
    public string ProjectName { get; set; } = string.Empty;
    public string ActProdId { get; set; } = string.Empty;
    public string ActProdName { get; set; } = string.Empty;
    public string StepId { get; set; } = string.Empty;
    public string StepName { get; set; } = string.Empty;
    public string ItemId { get; set; } = string.Empty;
    public string ItemName { get; set; } = string.Empty;
    public string WorkDate { get; set; } = string.Empty;
    public string Shift { get; set; } = string.Empty;
    public string RawDataValue { get; set; } = string.Empty;
    public string LotName { get; set; } = string.Empty;
    public string Frequency { get; set; } = string.Empty;
    public string AprovId { get; set; } = string.Empty;
    public string ApprovalYn { get; set; } = string.Empty;
}
