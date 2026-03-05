namespace Sphere.Application.DTOs.SPC;

#region Day Analysis DTOs

/// <summary>
/// Day analysis response DTO.
/// </summary>
public class DayAnalysisDto
{
    public string SpecSysId { get; set; } = string.Empty;
    public string SpecName { get; set; } = string.Empty;
    public string AnalysisDate { get; set; } = string.Empty;
    public List<DayAnalysisItemDto> Items { get; set; } = new();
    public DayAnalysisSummaryDto Summary { get; set; } = new();
    public ChartMetadataDto Metadata { get; set; } = new();
}

/// <summary>
/// Day analysis item DTO.
/// </summary>
public class DayAnalysisItemDto
{
    public string WorkDate { get; set; } = string.Empty;
    public string Shift { get; set; } = string.Empty;
    public int SampleCount { get; set; }
    public decimal Mean { get; set; }
    public decimal StdDev { get; set; }
    public decimal Min { get; set; }
    public decimal Max { get; set; }
    public decimal Range { get; set; }
    public decimal Cp { get; set; }
    public decimal Cpk { get; set; }
    public int DefectCount { get; set; }
    public decimal DefectRate { get; set; }
    public decimal YieldRate { get; set; }
    public int OOCCount { get; set; }
    public string Status { get; set; } = string.Empty;
}

/// <summary>
/// Day analysis summary DTO.
/// </summary>
public class DayAnalysisSummaryDto
{
    public int TotalDays { get; set; }
    public int TotalSamples { get; set; }
    public decimal OverallMean { get; set; }
    public decimal OverallStdDev { get; set; }
    public decimal AvgCpk { get; set; }
    public decimal MinCpk { get; set; }
    public decimal MaxCpk { get; set; }
    public int TotalDefects { get; set; }
    public decimal OverallDefectRate { get; set; }
    public decimal OverallYieldRate { get; set; }
    public int DaysWithOOC { get; set; }
}

#endregion

#region Month Analysis DTOs

/// <summary>
/// Month analysis response DTO.
/// </summary>
public class MonthAnalysisDto
{
    public string SpecSysId { get; set; } = string.Empty;
    public string SpecName { get; set; } = string.Empty;
    public string Year { get; set; } = string.Empty;
    public List<MonthAnalysisItemDto> Items { get; set; } = new();
    public MonthAnalysisSummaryDto Summary { get; set; } = new();
    public List<MonthComparisonDto> YearComparison { get; set; } = new();
    public ChartMetadataDto Metadata { get; set; } = new();
}

/// <summary>
/// Month analysis item DTO.
/// </summary>
public class MonthAnalysisItemDto
{
    public string Year { get; set; } = string.Empty;
    public string Month { get; set; } = string.Empty;
    public string MonthName { get; set; } = string.Empty;
    public int WorkDays { get; set; }
    public int SampleCount { get; set; }
    public decimal Mean { get; set; }
    public decimal StdDev { get; set; }
    public decimal Cp { get; set; }
    public decimal Cpk { get; set; }
    public decimal Pp { get; set; }
    public decimal Ppk { get; set; }
    public int DefectCount { get; set; }
    public decimal DefectRate { get; set; }
    public decimal YieldRate { get; set; }
    public int OOCCount { get; set; }
    public decimal ChangeFromPrevMonth { get; set; }
    public string Trend { get; set; } = string.Empty;
}

/// <summary>
/// Month analysis summary DTO.
/// </summary>
public class MonthAnalysisSummaryDto
{
    public int TotalMonths { get; set; }
    public int TotalSamples { get; set; }
    public decimal OverallMean { get; set; }
    public decimal OverallStdDev { get; set; }
    public decimal AvgCpk { get; set; }
    public string BestMonth { get; set; } = string.Empty;
    public decimal BestMonthCpk { get; set; }
    public string WorstMonth { get; set; } = string.Empty;
    public decimal WorstMonthCpk { get; set; }
    public decimal OverallYieldRate { get; set; }
    public string OverallTrend { get; set; } = string.Empty;
}

/// <summary>
/// Month comparison (year over year) DTO.
/// </summary>
public class MonthComparisonDto
{
    public string Month { get; set; } = string.Empty;
    public decimal CurrentYearCpk { get; set; }
    public decimal PreviousYearCpk { get; set; }
    public decimal Change { get; set; }
    public string ChangeDirection { get; set; } = string.Empty;
}

#endregion

#region Trend Analysis DTOs

/// <summary>
/// Trend analysis response DTO.
/// </summary>
public class TrendAnalysisDto
{
    public string SpecSysId { get; set; } = string.Empty;
    public string SpecName { get; set; } = string.Empty;
    public List<TrendDataPointDto> DataPoints { get; set; } = new();
    public TrendStatisticsDto Statistics { get; set; } = new();
    public TrendForecastDto Forecast { get; set; } = new();
    public ChartMetadataDto Metadata { get; set; } = new();
}

/// <summary>
/// Trend data point DTO.
/// </summary>
public class TrendDataPointDto
{
    public string Period { get; set; } = string.Empty;
    public string PeriodLabel { get; set; } = string.Empty;
    public decimal Value { get; set; }
    public decimal MovingAvg { get; set; }
    public decimal? UpperBound { get; set; }
    public decimal? LowerBound { get; set; }
    public int SampleCount { get; set; }
    public bool IsAnomaly { get; set; }
}

/// <summary>
/// Trend statistics DTO.
/// </summary>
public class TrendStatisticsDto
{
    public decimal Slope { get; set; }
    public decimal Intercept { get; set; }
    public decimal RSquared { get; set; }
    public decimal Correlation { get; set; }
    public string TrendDirection { get; set; } = string.Empty;
    public decimal TrendStrength { get; set; }
    public decimal SeasonalityIndex { get; set; }
    public int DataPointCount { get; set; }
}

/// <summary>
/// Trend forecast DTO.
/// </summary>
public class TrendForecastDto
{
    public List<TrendForecastPointDto> ForecastPoints { get; set; } = new();
    public decimal ConfidenceLevel { get; set; }
    public string ForecastMethod { get; set; } = string.Empty;
}

/// <summary>
/// Trend forecast point DTO.
/// </summary>
public class TrendForecastPointDto
{
    public string Period { get; set; } = string.Empty;
    public decimal PredictedValue { get; set; }
    public decimal UpperConfidence { get; set; }
    public decimal LowerConfidence { get; set; }
}

#endregion

#region Yield Spec Analysis DTOs

/// <summary>
/// Yield spec data response DTO.
/// </summary>
public class YieldSpecDataDto
{
    public string DivSeq { get; set; } = string.Empty;
    public List<YieldSpecItemDto> Items { get; set; } = new();
    public YieldSpecSummaryDto Summary { get; set; } = new();
    public ChartMetadataDto Metadata { get; set; } = new();
}

/// <summary>
/// Yield spec item DTO.
/// </summary>
public class YieldSpecItemDto
{
    public string SpecSysId { get; set; } = string.Empty;
    public string SpecId { get; set; } = string.Empty;
    public string SpecName { get; set; } = string.Empty;
    public string VendorId { get; set; } = string.Empty;
    public string VendorName { get; set; } = string.Empty;
    public string MtrlClassName { get; set; } = string.Empty;
    public int TotalQty { get; set; }
    public int PassQty { get; set; }
    public int FailQty { get; set; }
    public decimal YieldRate { get; set; }
    public decimal TargetYield { get; set; }
    public decimal GapToTarget { get; set; }
    public string Status { get; set; } = string.Empty;
}

/// <summary>
/// Yield spec summary DTO.
/// </summary>
public class YieldSpecSummaryDto
{
    public int TotalSpecs { get; set; }
    public int MeetingTarget { get; set; }
    public int BelowTarget { get; set; }
    public decimal OverallYield { get; set; }
    public decimal TargetYield { get; set; }
    public string BestSpec { get; set; } = string.Empty;
    public decimal BestYield { get; set; }
    public string WorstSpec { get; set; } = string.Empty;
    public decimal WorstYield { get; set; }
}

#endregion

#region Analysis Query DTOs

/// <summary>
/// Day analysis query parameters DTO.
/// </summary>
public class DayAnalysisQueryDto
{
    public string DivSeq { get; set; } = string.Empty;
    public string SpecSysId { get; set; } = string.Empty;
    public string? StartDate { get; set; }
    public string? EndDate { get; set; }
    public string? Shift { get; set; }
    public bool IncludeShiftBreakdown { get; set; }
}

/// <summary>
/// Month analysis query parameters DTO.
/// </summary>
public class MonthAnalysisQueryDto
{
    public string DivSeq { get; set; } = string.Empty;
    public string SpecSysId { get; set; } = string.Empty;
    public string? Year { get; set; }
    public bool IncludeYearComparison { get; set; }
}

/// <summary>
/// Trend analysis query parameters DTO.
/// </summary>
public class TrendAnalysisQueryDto
{
    public string DivSeq { get; set; } = string.Empty;
    public string SpecSysId { get; set; } = string.Empty;
    public string? StartDate { get; set; }
    public string? EndDate { get; set; }
    public string GroupBy { get; set; } = "day";
    public int MovingAvgWindow { get; set; } = 7;
    public bool IncludeForecast { get; set; }
    public int ForecastPeriods { get; set; } = 7;
}

#endregion
