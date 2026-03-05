namespace Sphere.Application.DTOs.SPC;

/// <summary>
/// Chart data query DTO.
/// </summary>
public class ChartDataQueryDto
{
    public string DivSeq { get; set; } = string.Empty;
    public string? VendorId { get; set; }
    public string? MtrlClassId { get; set; }
    public string? ProjectId { get; set; }
    public string? ActProdId { get; set; }
    public string? StepId { get; set; }
    public string? ItemId { get; set; }
    public string? MeasmId { get; set; }
    public string? SpecSysId { get; set; }
    public string? CntlnTypeId { get; set; }
    public string? SpecTypeId { get; set; }
    public string? StatTypeId { get; set; }
    public string? StartDate { get; set; }
    public string? EndDate { get; set; }
    public string? Shift { get; set; }
    public string ChartType { get; set; } = string.Empty;
}

/// <summary>
/// Chart data result DTO.
/// </summary>
public class ChartDataResultDto
{
    public string SpecSysId { get; set; } = string.Empty;
    public string WorkDate { get; set; } = string.Empty;
    public string Shift { get; set; } = string.Empty;
    public decimal Value { get; set; }
    public decimal Ucl { get; set; }
    public decimal Lcl { get; set; }
    public decimal Cl { get; set; }
    public decimal Usl { get; set; }
    public decimal Lsl { get; set; }
    public decimal Target { get; set; }
    public string AlarmYn { get; set; } = "N";
    public string AlarmType { get; set; } = string.Empty;
    public int SubgroupNo { get; set; }
    public int SampleNo { get; set; }
}

/// <summary>
/// Statistics calculation query DTO.
/// </summary>
public class StatisticsCalcQueryDto
{
    public string DivSeq { get; set; } = string.Empty;
    public string SpecSysId { get; set; } = string.Empty;
    public string? StartDate { get; set; }
    public string? EndDate { get; set; }
    public string? Shift { get; set; }
    public string StatType { get; set; } = string.Empty;
}

/// <summary>
/// Statistics calculation result DTO.
/// </summary>
public class StatisticsCalcResultDto
{
    public string SpecSysId { get; set; } = string.Empty;
    public decimal Mean { get; set; }
    public decimal StdDev { get; set; }
    public decimal Cp { get; set; }
    public decimal Cpk { get; set; }
    public decimal CpU { get; set; }
    public decimal CpL { get; set; }
    public decimal Pp { get; set; }
    public decimal Ppk { get; set; }
    public decimal Min { get; set; }
    public decimal Max { get; set; }
    public decimal Range { get; set; }
    public int SampleCount { get; set; }
    public int DefectCount { get; set; }
    public decimal DefectRate { get; set; }
}
