namespace Sphere.Application.DTOs.SPC;

#region P Chart DTOs

/// <summary>
/// P chart data response DTO.
/// </summary>
public class PChartDto
{
    public string SpecSysId { get; set; } = string.Empty;
    public string SpecName { get; set; } = string.Empty;
    public ControlLimitsDto PLimits { get; set; } = new();
    public List<PChartDataPointDto> DataPoints { get; set; } = new();
    public PChartStatisticsDto Statistics { get; set; } = new();
    public ChartMetadataDto Metadata { get; set; } = new();
}

/// <summary>
/// P chart data point DTO.
/// </summary>
public class PChartDataPointDto
{
    public int SubgroupNo { get; set; }
    public string WorkDate { get; set; } = string.Empty;
    public string Shift { get; set; } = string.Empty;
    public int InspectedQty { get; set; }
    public int DefectQty { get; set; }
    public decimal P { get; set; }
    public decimal Ucl { get; set; }
    public decimal Cl { get; set; }
    public decimal Lcl { get; set; }
    public bool IsOOC { get; set; }
    public string? ViolationRule { get; set; }
}

/// <summary>
/// P chart statistics DTO.
/// </summary>
public class PChartStatisticsDto
{
    public decimal PBar { get; set; }
    public decimal AvgSampleSize { get; set; }
    public int TotalInspected { get; set; }
    public int TotalDefects { get; set; }
    public decimal OverallDefectRate { get; set; }
    public int TotalSubgroups { get; set; }
    public int OOCSubgroups { get; set; }
}

#endregion

#region NP Chart DTOs

/// <summary>
/// NP chart data response DTO.
/// </summary>
public class NPChartDto
{
    public string SpecSysId { get; set; } = string.Empty;
    public string SpecName { get; set; } = string.Empty;
    public ControlLimitsDto NPLimits { get; set; } = new();
    public List<NPChartDataPointDto> DataPoints { get; set; } = new();
    public NPChartStatisticsDto Statistics { get; set; } = new();
    public ChartMetadataDto Metadata { get; set; } = new();
}

/// <summary>
/// NP chart data point DTO.
/// </summary>
public class NPChartDataPointDto
{
    public int SubgroupNo { get; set; }
    public string WorkDate { get; set; } = string.Empty;
    public string Shift { get; set; } = string.Empty;
    public int SampleSize { get; set; }
    public int DefectQty { get; set; }
    public decimal NP { get; set; }
    public bool IsOOC { get; set; }
    public string? ViolationRule { get; set; }
}

/// <summary>
/// NP chart statistics DTO.
/// </summary>
public class NPChartStatisticsDto
{
    public decimal NPBar { get; set; }
    public decimal PBar { get; set; }
    public int ConstantSampleSize { get; set; }
    public int TotalInspected { get; set; }
    public int TotalDefects { get; set; }
    public int TotalSubgroups { get; set; }
    public int OOCSubgroups { get; set; }
}

#endregion

#region C Chart DTOs

/// <summary>
/// C chart data response DTO.
/// </summary>
public class CChartDto
{
    public string SpecSysId { get; set; } = string.Empty;
    public string SpecName { get; set; } = string.Empty;
    public ControlLimitsDto CLimits { get; set; } = new();
    public List<CChartDataPointDto> DataPoints { get; set; } = new();
    public CChartStatisticsDto Statistics { get; set; } = new();
    public ChartMetadataDto Metadata { get; set; } = new();
}

/// <summary>
/// C chart data point DTO.
/// </summary>
public class CChartDataPointDto
{
    public int SubgroupNo { get; set; }
    public string WorkDate { get; set; } = string.Empty;
    public string Shift { get; set; } = string.Empty;
    public int DefectCount { get; set; }
    public bool IsOOC { get; set; }
    public string? ViolationRule { get; set; }
}

/// <summary>
/// C chart statistics DTO.
/// </summary>
public class CChartStatisticsDto
{
    public decimal CBar { get; set; }
    public int TotalDefects { get; set; }
    public int TotalSubgroups { get; set; }
    public int OOCSubgroups { get; set; }
}

#endregion

#region U Chart DTOs

/// <summary>
/// U chart data response DTO.
/// </summary>
public class UChartDto
{
    public string SpecSysId { get; set; } = string.Empty;
    public string SpecName { get; set; } = string.Empty;
    public ControlLimitsDto ULimits { get; set; } = new();
    public List<UChartDataPointDto> DataPoints { get; set; } = new();
    public UChartStatisticsDto Statistics { get; set; } = new();
    public ChartMetadataDto Metadata { get; set; } = new();
}

/// <summary>
/// U chart data point DTO.
/// </summary>
public class UChartDataPointDto
{
    public int SubgroupNo { get; set; }
    public string WorkDate { get; set; } = string.Empty;
    public string Shift { get; set; } = string.Empty;
    public int InspectedUnits { get; set; }
    public int DefectCount { get; set; }
    public decimal U { get; set; }
    public decimal Ucl { get; set; }
    public decimal Cl { get; set; }
    public decimal Lcl { get; set; }
    public bool IsOOC { get; set; }
    public string? ViolationRule { get; set; }
}

/// <summary>
/// U chart statistics DTO.
/// </summary>
public class UChartStatisticsDto
{
    public decimal UBar { get; set; }
    public decimal AvgInspectedUnits { get; set; }
    public int TotalUnits { get; set; }
    public int TotalDefects { get; set; }
    public int TotalSubgroups { get; set; }
    public int OOCSubgroups { get; set; }
}

#endregion
