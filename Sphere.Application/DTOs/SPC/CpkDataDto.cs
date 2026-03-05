namespace Sphere.Application.DTOs.SPC;

#region Cpk Analysis DTOs

/// <summary>
/// Cpk analysis response DTO.
/// </summary>
public class CpkAnalysisDto
{
    public string SpecSysId { get; set; } = string.Empty;
    public string SpecName { get; set; } = string.Empty;
    public CpkStatisticsDto Statistics { get; set; } = new();
    public SpecLimitsDto SpecLimits { get; set; } = new();
    public DistributionDto Distribution { get; set; } = new();
    public List<CpkTrendPointDto> TrendData { get; set; } = new();
    public CpkInterpretationDto Interpretation { get; set; } = new();
    public ChartMetadataDto Metadata { get; set; } = new();
}

/// <summary>
/// Cpk statistics DTO.
/// </summary>
public class CpkStatisticsDto
{
    public decimal Mean { get; set; }
    public decimal StdDev { get; set; }
    public decimal Variance { get; set; }
    public decimal Min { get; set; }
    public decimal Max { get; set; }
    public decimal Range { get; set; }
    public decimal Median { get; set; }
    public decimal Skewness { get; set; }
    public decimal Kurtosis { get; set; }
    public int SampleSize { get; set; }

    // Process Capability Indices
    public decimal Cp { get; set; }
    public decimal Cpk { get; set; }
    public decimal CpU { get; set; }
    public decimal CpL { get; set; }
    public decimal Pp { get; set; }
    public decimal Ppk { get; set; }
    public decimal PpU { get; set; }
    public decimal PpL { get; set; }
    public decimal Cpm { get; set; }

    // Sigma Level
    public decimal SigmaLevel { get; set; }
    public decimal ZBenchUpper { get; set; }
    public decimal ZBenchLower { get; set; }

    // PPM Calculations
    public decimal ExpectedPPMTotal { get; set; }
    public decimal ExpectedPPMAboveUSL { get; set; }
    public decimal ExpectedPPMBelowLSL { get; set; }
    public decimal ObservedPPM { get; set; }
}

/// <summary>
/// Specification limits DTO.
/// </summary>
public class SpecLimitsDto
{
    public decimal Usl { get; set; }
    public decimal Lsl { get; set; }
    public decimal Target { get; set; }
    public decimal Tolerance { get; set; }
    public bool HasUpperSpec { get; set; }
    public bool HasLowerSpec { get; set; }
}

/// <summary>
/// Distribution data DTO for histogram.
/// </summary>
public class DistributionDto
{
    public List<HistogramBinDto> Bins { get; set; } = new();
    public List<NormalCurvePointDto> NormalCurve { get; set; } = new();
    public decimal NormalityTestPValue { get; set; }
    public bool IsNormallyDistributed { get; set; }
}

/// <summary>
/// Histogram bin DTO.
/// </summary>
public class HistogramBinDto
{
    public decimal BinStart { get; set; }
    public decimal BinEnd { get; set; }
    public decimal BinCenter { get; set; }
    public int Frequency { get; set; }
    public decimal RelativeFrequency { get; set; }
    public decimal CumulativeFrequency { get; set; }
}

/// <summary>
/// Normal curve point DTO.
/// </summary>
public class NormalCurvePointDto
{
    public decimal X { get; set; }
    public decimal Y { get; set; }
}

/// <summary>
/// Cpk trend point DTO.
/// </summary>
public class CpkTrendPointDto
{
    public string Period { get; set; } = string.Empty;
    public string WorkDate { get; set; } = string.Empty;
    public decimal Cpk { get; set; }
    public decimal Ppk { get; set; }
    public decimal Mean { get; set; }
    public decimal StdDev { get; set; }
    public int SampleSize { get; set; }
}

/// <summary>
/// Cpk interpretation DTO.
/// </summary>
public class CpkInterpretationDto
{
    public string CapabilityLevel { get; set; } = string.Empty;
    public string Recommendation { get; set; } = string.Empty;
    public bool MeetsRequirement { get; set; }
    public decimal TargetCpk { get; set; }
    public decimal GapToTarget { get; set; }
    public List<string> ImprovementSuggestions { get; set; } = new();
}

#endregion

#region Cpk Query DTOs

/// <summary>
/// Cpk query parameters DTO.
/// </summary>
public class CpkQueryDto
{
    public string DivSeq { get; set; } = string.Empty;
    public string SpecSysId { get; set; } = string.Empty;
    public string? StartDate { get; set; }
    public string? EndDate { get; set; }
    public string? Shift { get; set; }
    public int? HistogramBins { get; set; }
    public bool IncludeTrend { get; set; } = true;
    public string? TrendGroupBy { get; set; }
}

#endregion
