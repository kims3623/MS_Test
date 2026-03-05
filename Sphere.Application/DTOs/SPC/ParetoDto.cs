namespace Sphere.Application.DTOs.SPC;

#region Pareto DTOs

/// <summary>
/// Pareto diagram response DTO.
/// </summary>
public class ParetoDataDto
{
    public string SpecSysId { get; set; } = string.Empty;
    public string SpecName { get; set; } = string.Empty;
    public string AnalysisType { get; set; } = string.Empty;
    public List<ParetoCategoryDto> Categories { get; set; } = new();
    public ParetoSummaryDto Summary { get; set; } = new();
    public ChartMetadataDto Metadata { get; set; } = new();
}

/// <summary>
/// Pareto category item DTO.
/// </summary>
public class ParetoCategoryDto
{
    public int Rank { get; set; }
    public string CategoryId { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public string CategoryNameK { get; set; } = string.Empty;
    public string CategoryNameE { get; set; } = string.Empty;
    public int Count { get; set; }
    public decimal Percentage { get; set; }
    public decimal CumulativeCount { get; set; }
    public decimal CumulativePercentage { get; set; }
    public bool IsVitalFew { get; set; }
}

/// <summary>
/// Pareto summary DTO.
/// </summary>
public class ParetoSummaryDto
{
    public int TotalCategories { get; set; }
    public int TotalCount { get; set; }
    public int VitalFewCount { get; set; }
    public decimal VitalFewPercentage { get; set; }
    public List<string> VitalFewCategories { get; set; } = new();
    public decimal EightyPercentCutoff { get; set; }
    public string TopContributor { get; set; } = string.Empty;
    public decimal TopContributorPercentage { get; set; }
}

/// <summary>
/// Pareto query parameters DTO.
/// </summary>
public class ParetoQueryDto
{
    public string DivSeq { get; set; } = string.Empty;
    public string SpecSysId { get; set; } = string.Empty;
    public string AnalysisType { get; set; } = "defect";
    public string? StartDate { get; set; }
    public string? EndDate { get; set; }
    public string? Shift { get; set; }
    public int? TopN { get; set; }
    public bool GroupOthers { get; set; } = true;
}

#endregion

#region Cause Analysis DTOs

/// <summary>
/// Defect cause analysis response DTO.
/// </summary>
public class DefectCauseAnalysisDto
{
    public string SpecSysId { get; set; } = string.Empty;
    public List<DefectCauseDto> Causes { get; set; } = new();
    public List<DefectTrendDto> Trends { get; set; } = new();
    public DefectCauseSummaryDto Summary { get; set; } = new();
}

/// <summary>
/// Defect cause DTO.
/// </summary>
public class DefectCauseDto
{
    public string CauseId { get; set; } = string.Empty;
    public string CauseName { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public int OccurrenceCount { get; set; }
    public decimal Percentage { get; set; }
    public decimal CumulativePercentage { get; set; }
    public decimal AvgResolutionTime { get; set; }
    public string Severity { get; set; } = string.Empty;
}

/// <summary>
/// Defect trend DTO.
/// </summary>
public class DefectTrendDto
{
    public string Period { get; set; } = string.Empty;
    public int TotalDefects { get; set; }
    public Dictionary<string, int> DefectsByType { get; set; } = new();
    public decimal DefectRate { get; set; }
    public string TrendDirection { get; set; } = string.Empty;
}

/// <summary>
/// Defect cause summary DTO.
/// </summary>
public class DefectCauseSummaryDto
{
    public int TotalDefects { get; set; }
    public int UniqueCauses { get; set; }
    public string TopCause { get; set; } = string.Empty;
    public decimal TopCausePercentage { get; set; }
    public decimal ResolutionRate { get; set; }
    public string RecommendedAction { get; set; } = string.Empty;
}

#endregion
