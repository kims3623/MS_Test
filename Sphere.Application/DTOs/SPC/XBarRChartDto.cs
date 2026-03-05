namespace Sphere.Application.DTOs.SPC;

#region X-Bar R Chart DTOs

/// <summary>
/// X-Bar R chart data response DTO.
/// </summary>
public class XBarRChartDto
{
    public string SpecSysId { get; set; } = string.Empty;
    public string SpecName { get; set; } = string.Empty;
    public ControlLimitsDto XBarLimits { get; set; } = new();
    public ControlLimitsDto RLimits { get; set; } = new();
    public List<XBarRDataPointDto> DataPoints { get; set; } = new();
    public XBarRStatisticsDto Statistics { get; set; } = new();
    public ChartMetadataDto Metadata { get; set; } = new();
}

/// <summary>
/// X-Bar R data point DTO.
/// </summary>
public class XBarRDataPointDto
{
    public int SubgroupNo { get; set; }
    public string WorkDate { get; set; } = string.Empty;
    public string Shift { get; set; } = string.Empty;
    public decimal XBar { get; set; }
    public decimal R { get; set; }
    public decimal S { get; set; }
    public int SampleSize { get; set; }
    public bool IsXBarOOC { get; set; }
    public bool IsROOC { get; set; }
    public string? ViolationRule { get; set; }
    public List<decimal> SampleValues { get; set; } = new();
}

/// <summary>
/// X-Bar R statistics DTO.
/// </summary>
public class XBarRStatisticsDto
{
    public decimal XBarBar { get; set; }
    public decimal RBar { get; set; }
    public decimal SBar { get; set; }
    public decimal Sigma { get; set; }
    public decimal EstSigma { get; set; }
    public int TotalSubgroups { get; set; }
    public int OOCSubgroups { get; set; }
    public decimal ProcessCapability { get; set; }
}

#endregion

#region Control Limits DTOs

/// <summary>
/// Control limits DTO.
/// </summary>
public class ControlLimitsDto
{
    public decimal Ucl { get; set; }
    public decimal Cl { get; set; }
    public decimal Lcl { get; set; }
    public decimal? Usl { get; set; }
    public decimal? Lsl { get; set; }
    public decimal? Target { get; set; }
    public decimal? UclWarning { get; set; }
    public decimal? LclWarning { get; set; }
}

/// <summary>
/// Control limits update request DTO.
/// </summary>
public class ControlLimitsUpdateDto
{
    public string SpecSysId { get; set; } = string.Empty;
    public string ChartType { get; set; } = string.Empty;
    public decimal Ucl { get; set; }
    public decimal Cl { get; set; }
    public decimal Lcl { get; set; }
    public decimal? Usl { get; set; }
    public decimal? Lsl { get; set; }
    public decimal? Target { get; set; }
    public string? Reason { get; set; }
}

/// <summary>
/// Control limits response with history DTO.
/// </summary>
public class ControlLimitsResponseDto
{
    public ControlLimitsDto Current { get; set; } = new();
    public List<ControlLimitsHistoryDto> History { get; set; } = new();
}

/// <summary>
/// Control limits history DTO.
/// </summary>
public class ControlLimitsHistoryDto
{
    public string EffectiveDate { get; set; } = string.Empty;
    public decimal Ucl { get; set; }
    public decimal Cl { get; set; }
    public decimal Lcl { get; set; }
    public string UpdatedBy { get; set; } = string.Empty;
    public string Reason { get; set; } = string.Empty;
}

#endregion

#region Chart Metadata DTOs

/// <summary>
/// Chart metadata DTO.
/// </summary>
public class ChartMetadataDto
{
    public string ChartType { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string XAxisLabel { get; set; } = string.Empty;
    public string YAxisLabel { get; set; } = string.Empty;
    public string DataPeriod { get; set; } = string.Empty;
    public int TotalPoints { get; set; }
    public DateTime GeneratedAt { get; set; }
}

#endregion
