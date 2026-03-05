using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.Screen;

/// <summary>
/// SPH4020 Grid entity for TPS alarm grid.
/// </summary>
public class SPH4020Grid : SphereEntity
{
    public string AlmSysId { get; set; } = string.Empty;
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
    public string AlmProcId { get; set; } = string.Empty;
    public string AlmProcName { get; set; } = string.Empty;
    public string AlmActionId { get; set; } = string.Empty;
    public string AlmActionName { get; set; } = string.Empty;
    public string WorkDate { get; set; } = string.Empty;
    public string Shift { get; set; } = string.Empty;
    public string AlmState { get; set; } = string.Empty;
    public string AlmStateName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime? AlmDate { get; set; }
    public string AlmDateStr { get; set; } = string.Empty;
}
