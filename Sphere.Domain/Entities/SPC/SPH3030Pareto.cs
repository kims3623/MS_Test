using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.SPC;

/// <summary>
/// SPH3030 Pareto chart entity for defect analysis.
/// Maps to SPH3030 screen Pareto chart data.
/// </summary>
public class SPH3030Pareto : SphereEntity
{
    /// <summary>
    /// Specification system identifier
    /// </summary>
    public string SpecSysId { get; set; } = string.Empty;

    /// <summary>
    /// Period start date
    /// </summary>
    public string PeriodFrom { get; set; } = string.Empty;

    /// <summary>
    /// Period end date
    /// </summary>
    public string PeriodTo { get; set; } = string.Empty;

    /// <summary>
    /// Defect category
    /// </summary>
    public string DefectCategory { get; set; } = string.Empty;

    /// <summary>
    /// Defect category name
    /// </summary>
    public string DefectCategoryName { get; set; } = string.Empty;

    /// <summary>
    /// Defect count
    /// </summary>
    public int DefectCount { get; set; }

    /// <summary>
    /// Percentage of total
    /// </summary>
    public decimal? Percentage { get; set; }

    /// <summary>
    /// Cumulative percentage
    /// </summary>
    public decimal? CumulativePercentage { get; set; }

    /// <summary>
    /// Rank
    /// </summary>
    public int Rank { get; set; }
}
