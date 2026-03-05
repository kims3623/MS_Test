using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.SPC;

/// <summary>
/// SPH3030 Chart entity for SPC pareto analysis chart.
/// </summary>
public class SPH3030Chart : SphereEntity
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
    public string CategoryName { get; set; } = string.Empty;
    public int DefectCount { get; set; }
    public decimal DefectRate { get; set; }
    public decimal CumulativeRate { get; set; }
    public int Rank { get; set; }
    public string FromDate { get; set; } = string.Empty;
    public string ToDate { get; set; } = string.Empty;
    public string ChartType { get; set; } = string.Empty;
    public string ChartTitle { get; set; } = string.Empty;
}
