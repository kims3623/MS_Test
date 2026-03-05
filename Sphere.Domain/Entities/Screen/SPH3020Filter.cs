using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.Screen;

/// <summary>
/// SPH3020 Filter entity for SPC chart filter.
/// </summary>
public class SPH3020Filter : SphereEntity
{
    public string VendorId { get; set; } = string.Empty;
    public string MtrlClassId { get; set; } = string.Empty;
    public string MtrlId { get; set; } = string.Empty;
    public string ProjectId { get; set; } = string.Empty;
    public string ActProdId { get; set; } = string.Empty;
    public string StepId { get; set; } = string.Empty;
    public string ItemId { get; set; } = string.Empty;
    public string MeasmId { get; set; } = string.Empty;
    public string CntlnTypeId { get; set; } = string.Empty;
    public string SpecTypeId { get; set; } = string.Empty;
    public string StatTypeId { get; set; } = string.Empty;
    public string FromDate { get; set; } = string.Empty;
    public string ToDate { get; set; } = string.Empty;
    public string ViewType { get; set; } = string.Empty;
    public string Shift { get; set; } = string.Empty;
    public string SpecSysId { get; set; } = string.Empty;
}
