namespace Sphere.Application.DTOs.SPC;

#region Chart Config DTOs

/// <summary>
/// Chart configuration response DTO.
/// </summary>
public class ChartConfigDto
{
    public string ConfigId { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string ChartType { get; set; } = string.Empty;
    public ChartDisplaySettingsDto DisplaySettings { get; set; } = new();
    public ChartDataSettingsDto DataSettings { get; set; } = new();
    public ChartExportSettingsDto ExportSettings { get; set; } = new();
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

/// <summary>
/// Chart display settings DTO.
/// </summary>
public class ChartDisplaySettingsDto
{
    public bool ShowGrid { get; set; } = true;
    public bool ShowLegend { get; set; } = true;
    public string LegendPosition { get; set; } = "bottom";
    public bool ShowTooltip { get; set; } = true;
    public bool ShowDataLabels { get; set; } = false;
    public bool ShowControlLimits { get; set; } = true;
    public bool ShowSpecLimits { get; set; } = true;
    public bool ShowTargetLine { get; set; } = true;
    public bool HighlightOOC { get; set; } = true;
    public string OOCColor { get; set; } = "#ff4d4f";
    public string Theme { get; set; } = "light";
    public ChartColorsDto Colors { get; set; } = new();
}

/// <summary>
/// Chart colors DTO.
/// </summary>
public class ChartColorsDto
{
    public string Primary { get; set; } = "#1890ff";
    public string Secondary { get; set; } = "#52c41a";
    public string Warning { get; set; } = "#faad14";
    public string Error { get; set; } = "#ff4d4f";
    public string UCL { get; set; } = "#ff4d4f";
    public string LCL { get; set; } = "#ff4d4f";
    public string CL { get; set; } = "#52c41a";
    public string USL { get; set; } = "#722ed1";
    public string LSL { get; set; } = "#722ed1";
    public string Target { get; set; } = "#faad14";
}

/// <summary>
/// Chart data settings DTO.
/// </summary>
public class ChartDataSettingsDto
{
    public int MaxDataPoints { get; set; } = 500;
    public string AggregationLevel { get; set; } = "auto";
    public int MovingAvgWindow { get; set; } = 7;
    public bool ShowMovingAvg { get; set; } = false;
    public bool AutoRefresh { get; set; } = false;
    public int RefreshIntervalSec { get; set; } = 60;
    public string DefaultDateRange { get; set; } = "30d";
    public string DefaultShift { get; set; } = "all";
}

/// <summary>
/// Chart export settings DTO.
/// </summary>
public class ChartExportSettingsDto
{
    public string DefaultFormat { get; set; } = "png";
    public int ImageWidth { get; set; } = 1200;
    public int ImageHeight { get; set; } = 800;
    public int ImageDpi { get; set; } = 300;
    public bool IncludeTitle { get; set; } = true;
    public bool IncludeLegend { get; set; } = true;
    public bool IncludeTimestamp { get; set; } = true;
    public string ExcelTemplate { get; set; } = "default";
}

/// <summary>
/// Chart config save request DTO.
/// </summary>
public class ChartConfigSaveRequestDto
{
    public string? ConfigId { get; set; }
    public string ChartType { get; set; } = string.Empty;
    public ChartDisplaySettingsDto? DisplaySettings { get; set; }
    public ChartDataSettingsDto? DataSettings { get; set; }
    public ChartExportSettingsDto? ExportSettings { get; set; }
    public bool IsDefault { get; set; }
}

#endregion

#region Chart Export DTOs

/// <summary>
/// Chart export request DTO.
/// </summary>
public class ChartExportRequestDto
{
    public string SpecSysId { get; set; } = string.Empty;
    public string ChartType { get; set; } = string.Empty;
    public string ExportFormat { get; set; } = "xlsx";
    public string? StartDate { get; set; }
    public string? EndDate { get; set; }
    public string? Shift { get; set; }
    public bool IncludeRawData { get; set; } = true;
    public bool IncludeStatistics { get; set; } = true;
    public bool IncludeChartImage { get; set; } = false;
}

/// <summary>
/// Chart export response DTO.
/// </summary>
public class ChartExportResponseDto
{
    public string FileName { get; set; } = string.Empty;
    public string ContentType { get; set; } = string.Empty;
    public byte[] FileContent { get; set; } = Array.Empty<byte>();
    public long FileSize { get; set; }
    public DateTime GeneratedAt { get; set; }
}

/// <summary>
/// Chart export data DTO (for Excel/CSV).
/// </summary>
public class ChartExportDataDto
{
    public ChartExportHeaderDto Header { get; set; } = new();
    public List<ChartDataResultDto> RawData { get; set; } = new();
    public StatisticsCalcResultDto? Statistics { get; set; }
    public ControlLimitsDto? ControlLimits { get; set; }
}

/// <summary>
/// Chart export header DTO.
/// </summary>
public class ChartExportHeaderDto
{
    public string SpecSysId { get; set; } = string.Empty;
    public string SpecName { get; set; } = string.Empty;
    public string ChartType { get; set; } = string.Empty;
    public string DataPeriod { get; set; } = string.Empty;
    public string GeneratedBy { get; set; } = string.Empty;
    public DateTime GeneratedAt { get; set; }
    public int TotalDataPoints { get; set; }
}

#endregion

#region Filter Presets DTOs

/// <summary>
/// SPC filter preset DTO.
/// </summary>
public class SPCFilterPresetDto
{
    public string PresetId { get; set; } = string.Empty;
    public string PresetName { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public bool IsDefault { get; set; }
    public SPCFilterValuesDto Filters { get; set; } = new();
    public DateTime CreatedAt { get; set; }
}

/// <summary>
/// SPC filter values DTO.
/// </summary>
public class SPCFilterValuesDto
{
    public string? VendorId { get; set; }
    public string? MtrlClassId { get; set; }
    public string? ProjectId { get; set; }
    public string? SpecSysId { get; set; }
    public string? StartDate { get; set; }
    public string? EndDate { get; set; }
    public string? Shift { get; set; }
    public string? ChartType { get; set; }
    public List<string>? RunRuleIds { get; set; }
}

#endregion
