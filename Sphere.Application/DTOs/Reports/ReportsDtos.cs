namespace Sphere.Application.DTOs.Reports;

#region Dashboard DTOs

/// <summary>
/// Aggregated dashboard data DTO.
/// </summary>
public class DashboardDataDto
{
    public List<HomeIssueDataDto> IssueData { get; set; } = new();
    public HomeAlarmDataDto AlarmData { get; set; } = new();
    public DashboardSummaryDto Summary { get; set; } = new();
}

/// <summary>
/// Dashboard summary statistics.
/// </summary>
public class DashboardSummaryDto
{
    public int TotalIssues { get; set; }
    public int TotalAlarms { get; set; }
    public int CriticalAlerts { get; set; }
    public decimal YieldRate { get; set; }
    public DateTime LastUpdated { get; set; }
}

#endregion

#region Issue Data DTOs

/// <summary>
/// Home issue data DTO (matches HomeIssueDataRec entity).
/// </summary>
public class HomeIssueDataDto
{
    public string DivSeq { get; set; } = string.Empty;
    public string VendorType { get; set; } = string.Empty;
    public string VendorTypeName { get; set; } = string.Empty;
    public string StatTypeId { get; set; } = string.Empty;
    public string StatTypeName { get; set; } = string.Empty;
    public string VendorId { get; set; } = string.Empty;
    public string VendorName { get; set; } = string.Empty;
    public string MtrlClassGroupName { get; set; } = string.Empty;
    public int DiffDay { get; set; }
    public int Count { get; set; }
}

/// <summary>
/// Issue data filter DTO.
/// </summary>
public class IssueDataFilterDto
{
    public string? VendorType { get; set; }
    public string? StatTypeId { get; set; }
    public string? VendorId { get; set; }
    /// <summary>
    /// Work date for HOME_ISSUE_DATA USP (format: yyyy-MM-dd). Defaults to today if not specified.
    /// </summary>
    public string? WorkDate { get; set; }
}

#endregion

#region Alarm Data DTOs

/// <summary>
/// Home alarm data DTO (combined grid, yearly and monthly).
/// Maps to 3 separate USPs: USP_SPC_HOME_ALARM_GRID, USP_SPC_HOME_ALARM_YEAR, USP_SPC_HOME_ALARM_MONTH.
/// </summary>
public class HomeAlarmDataDto
{
    public List<HomeAlarmGridDto> GridData { get; set; } = new();
    public List<HomeAlarmYearDto> YearlyData { get; set; } = new();
    public List<HomeAlarmMonthDto> MonthlyData { get; set; } = new();
}

/// <summary>
/// Home alarm grid data DTO (from USP_SPC_HOME_ALARM_GRID).
/// </summary>
public class HomeAlarmGridDto
{
    public string DivSeq { get; set; } = string.Empty;
    public string VendorId { get; set; } = string.Empty;
    public string VendorName { get; set; } = string.Empty;
    public string AlmSysId { get; set; } = string.Empty;
    public string AlmSysName { get; set; } = string.Empty;
    public string WorkDate { get; set; } = string.Empty;
    public int AlmCount { get; set; }
    public string AlmStatus { get; set; } = string.Empty;
}

/// <summary>
/// Home alarm yearly data DTO (matches HomeAlarmYearRec entity).
/// </summary>
public class HomeAlarmYearDto
{
    public string DivSeq { get; set; } = string.Empty;
    public string Year { get; set; } = string.Empty;
    public string VendorType { get; set; } = string.Empty;
    public string VendorTypeName { get; set; } = string.Empty;
    public int AlmCount { get; set; }
    public decimal AlmMonthlyAvg { get; set; }
}

/// <summary>
/// Home alarm monthly data DTO.
/// </summary>
public class HomeAlarmMonthDto
{
    public string DivSeq { get; set; } = string.Empty;
    public string Year { get; set; } = string.Empty;
    public string Month { get; set; } = string.Empty;
    public string VendorType { get; set; } = string.Empty;
    public string VendorTypeName { get; set; } = string.Empty;
    public int AlmCount { get; set; }
}

/// <summary>
/// Alarm data filter DTO.
/// </summary>
public class AlarmDataFilterDto
{
    public string Year { get; set; } = string.Empty;
    public string? VendorType { get; set; }

    /// <summary>
    /// Vendor ID for HOME_ALARM USPs. Falls back to VendorType if not set.
    /// </summary>
    public string? VendorId
    {
        get => _vendorId ?? VendorType;
        set => _vendorId = value;
    }
    private string? _vendorId;
}

#endregion

#region Yield Report DTOs

/// <summary>
/// Yield report data DTO.
/// </summary>
public class YieldReportDto
{
    public List<YieldReportItemDto> Items { get; set; } = new();
    public YieldSummaryDto Summary { get; set; } = new();
}

/// <summary>
/// Yield report item DTO.
/// </summary>
public class YieldReportItemDto
{
    public string DivSeq { get; set; } = string.Empty;
    public string VendorId { get; set; } = string.Empty;
    public string VendorName { get; set; } = string.Empty;
    public string MtrlClassId { get; set; } = string.Empty;
    public string MtrlClassName { get; set; } = string.Empty;
    public string SpecId { get; set; } = string.Empty;
    public string SpecName { get; set; } = string.Empty;
    public string ReportDate { get; set; } = string.Empty;
    public int TotalCount { get; set; }
    public int PassCount { get; set; }
    public int FailCount { get; set; }
    public decimal YieldRate { get; set; }
}

/// <summary>
/// Yield summary DTO.
/// </summary>
public class YieldSummaryDto
{
    public int TotalItems { get; set; }
    public int TotalPass { get; set; }
    public int TotalFail { get; set; }
    public decimal OverallYieldRate { get; set; }
    public decimal DailyAvgYield { get; set; }
    public decimal WeeklyAvgYield { get; set; }
    public decimal MonthlyAvgYield { get; set; }
}

/// <summary>
/// Yield report filter DTO.
/// </summary>
public class YieldReportFilterDto
{
    public string? StartDate { get; set; }
    public string? EndDate { get; set; }
    public string? VendorId { get; set; }
    public string? MtrlClassId { get; set; }
    public string? SpecId { get; set; }
    public string? GroupBy { get; set; }
}

#endregion

#region Statistics Report DTOs

/// <summary>
/// Statistics report data DTO.
/// </summary>
public class StatisticsReportDto
{
    public List<StatisticsItemDto> Items { get; set; } = new();
    public List<StatisticsChartDto> Charts { get; set; } = new();
    public StatisticsSummaryDto Summary { get; set; } = new();
}

/// <summary>
/// Statistics item DTO.
/// </summary>
public class StatisticsItemDto
{
    public string DivSeq { get; set; } = string.Empty;
    public string CategoryId { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public string SubCategoryId { get; set; } = string.Empty;
    public string SubCategoryName { get; set; } = string.Empty;
    public string Period { get; set; } = string.Empty;
    public decimal Value { get; set; }
    public string Unit { get; set; } = string.Empty;
    public decimal ChangeRate { get; set; }
    public string Trend { get; set; } = string.Empty;
}

/// <summary>
/// Statistics chart data DTO.
/// </summary>
public class StatisticsChartDto
{
    public string ChartId { get; set; } = string.Empty;
    public string ChartType { get; set; } = string.Empty;
    public string ChartTitle { get; set; } = string.Empty;
    public List<ChartSeriesDto> Series { get; set; } = new();
}

/// <summary>
/// Chart series data DTO.
/// </summary>
public class ChartSeriesDto
{
    public string Name { get; set; } = string.Empty;
    public List<ChartPointDto> Data { get; set; } = new();
}

/// <summary>
/// Chart point data DTO.
/// </summary>
public class ChartPointDto
{
    public string X { get; set; } = string.Empty;
    public decimal Y { get; set; }
    public string? Label { get; set; }
}

/// <summary>
/// Statistics summary DTO.
/// </summary>
public class StatisticsSummaryDto
{
    public int TotalRecords { get; set; }
    public decimal AvgValue { get; set; }
    public decimal MaxValue { get; set; }
    public decimal MinValue { get; set; }
    public string AnalysisPeriod { get; set; } = string.Empty;
}

/// <summary>
/// Statistics report filter DTO.
/// </summary>
public class StatisticsFilterDto
{
    public string? StartDate { get; set; }
    public string? EndDate { get; set; }
    public string? ReportType { get; set; }
    public string? CategoryId { get; set; }
    public string? VendorId { get; set; }
    public string? GroupBy { get; set; }
}

#endregion
