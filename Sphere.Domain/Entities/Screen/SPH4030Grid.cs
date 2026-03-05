using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.Screen;

/// <summary>
/// SPH4030 Grid entity for TPS action history grid.
/// </summary>
public class SPH4030Grid : SphereEntity
{
    public string AlmSysId { get; set; } = string.Empty;
    public string AlmActionId { get; set; } = string.Empty;
    public string AlmActionName { get; set; } = string.Empty;
    public string VendorId { get; set; } = string.Empty;
    public string VendorName { get; set; } = string.Empty;
    public string MtrlClassId { get; set; } = string.Empty;
    public string MtrlClassName { get; set; } = string.Empty;
    public string ProjectId { get; set; } = string.Empty;
    public string ProjectName { get; set; } = string.Empty;
    public string StepId { get; set; } = string.Empty;
    public string StepName { get; set; } = string.Empty;
    public string ItemId { get; set; } = string.Empty;
    public string ItemName { get; set; } = string.Empty;
    public string ActionResult { get; set; } = string.Empty;
    public string ActionResultName { get; set; } = string.Empty;
    public string ActionContent { get; set; } = string.Empty;
    public string ActionUserId { get; set; } = string.Empty;
    public string ActionUserName { get; set; } = string.Empty;
    public DateTime? ActionDate { get; set; }
    public string ActionDateStr { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Remarks { get; set; } = string.Empty;
}
