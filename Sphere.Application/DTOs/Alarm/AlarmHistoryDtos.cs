namespace Sphere.Application.DTOs.Alarm;

/// <summary>
/// Alarm history item DTO for timeline display.
/// </summary>
public class AlarmHistoryItemDto
{
    public string HistSeq { get; set; } = string.Empty;
    public string AlmSysId { get; set; } = string.Empty;
    public string AlmActionId { get; set; } = string.Empty;
    public string AlmActionName { get; set; } = string.Empty;
    public string PrevActionId { get; set; } = string.Empty;
    public string PrevActionName { get; set; } = string.Empty;
    public string ActionType { get; set; } = string.Empty;
    public string ActionTypeName { get; set; } = string.Empty;
    public string Comment { get; set; } = string.Empty;
    public string CreateDate { get; set; } = string.Empty;
    public string CreateUserId { get; set; } = string.Empty;
    public string CreateUserName { get; set; } = string.Empty;
}

/// <summary>
/// Alarm history query parameters.
/// </summary>
public class AlarmHistoryQueryDto
{
    public string DivSeq { get; set; } = string.Empty;
    public string AlmSysId { get; set; } = string.Empty;
    public int? PageNumber { get; set; }
    public int? PageSize { get; set; }
}

/// <summary>
/// Alarm history list response.
/// </summary>
public class AlarmHistoryResponseDto
{
    public List<AlarmHistoryItemDto> Items { get; set; } = new();
    public int TotalCount { get; set; }
}

/// <summary>
/// Alarm statistics DTO.
/// </summary>
public class AlarmStatisticsDto
{
    public string DivSeq { get; set; } = string.Empty;
    public string Period { get; set; } = string.Empty;
    public int TotalCount { get; set; }
    public int OpenCount { get; set; }
    public int ClosedCount { get; set; }
    public int InProgressCount { get; set; }
    public int OverdueCount { get; set; }
    public double AvgResolutionTime { get; set; }
    public List<AlarmStatsByTypeDto> ByType { get; set; } = new();
    public List<AlarmStatsBySeverityDto> BySeverity { get; set; } = new();
}

/// <summary>
/// Alarm statistics by type.
/// </summary>
public class AlarmStatsByTypeDto
{
    public string AlmProcId { get; set; } = string.Empty;
    public string AlmProcName { get; set; } = string.Empty;
    public int Count { get; set; }
    public double Percentage { get; set; }
}

/// <summary>
/// Alarm statistics by severity.
/// </summary>
public class AlarmStatsBySeverityDto
{
    public string Severity { get; set; } = string.Empty;
    public string SeverityName { get; set; } = string.Empty;
    public int Count { get; set; }
    public double Percentage { get; set; }
}

/// <summary>
/// Alarm trend data DTO.
/// </summary>
public class AlarmTrendDto
{
    public string Date { get; set; } = string.Empty;
    public int CreatedCount { get; set; }
    public int ClosedCount { get; set; }
    public int OpenCount { get; set; }
}

/// <summary>
/// Alarm trend query parameters.
/// </summary>
public class AlarmTrendQueryDto
{
    public string DivSeq { get; set; } = string.Empty;
    public string StartDate { get; set; } = string.Empty;
    public string EndDate { get; set; } = string.Empty;
    public string? VendorId { get; set; }
    public string? MtrlClassId { get; set; }
    public string GroupBy { get; set; } = "DAY"; // DAY, WEEK, MONTH
}

/// <summary>
/// Alarm trend response.
/// </summary>
public class AlarmTrendResponseDto
{
    public List<AlarmTrendDto> Items { get; set; } = new();
    public AlarmStatisticsDto Summary { get; set; } = new();
}
