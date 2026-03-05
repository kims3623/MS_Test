namespace Sphere.Application.DTOs.SPC;

#region Histogram DTOs

/// <summary>
/// Histogram data response DTO.
/// </summary>
public class HistogramDataDto
{
    public string SpecSysId { get; set; } = string.Empty;
    public string SpecName { get; set; } = string.Empty;
    public List<HistogramBinDto> Bins { get; set; } = new();
    public HistogramStatisticsDto Statistics { get; set; } = new();
    public HistogramSpecLinesDto SpecLines { get; set; } = new();
    public List<NormalCurvePointDto> NormalCurve { get; set; } = new();
    public ChartMetadataDto Metadata { get; set; } = new();
}

/// <summary>
/// Histogram statistics DTO.
/// </summary>
public class HistogramStatisticsDto
{
    public int TotalCount { get; set; }
    public decimal Mean { get; set; }
    public decimal Median { get; set; }
    public decimal Mode { get; set; }
    public decimal StdDev { get; set; }
    public decimal Variance { get; set; }
    public decimal Skewness { get; set; }
    public decimal Kurtosis { get; set; }
    public decimal Min { get; set; }
    public decimal Max { get; set; }
    public decimal Range { get; set; }
    public decimal Q1 { get; set; }
    public decimal Q3 { get; set; }
    public decimal IQR { get; set; }

    // Normality test
    public decimal ShapiroWilkW { get; set; }
    public decimal ShapiroWilkPValue { get; set; }
    public bool IsNormal { get; set; }
    public string NormalityInterpretation { get; set; } = string.Empty;
}

/// <summary>
/// Histogram specification lines DTO.
/// </summary>
public class HistogramSpecLinesDto
{
    public decimal? Usl { get; set; }
    public decimal? Lsl { get; set; }
    public decimal? Target { get; set; }
    public decimal Mean { get; set; }
    public decimal Plus1Sigma { get; set; }
    public decimal Minus1Sigma { get; set; }
    public decimal Plus2Sigma { get; set; }
    public decimal Minus2Sigma { get; set; }
    public decimal Plus3Sigma { get; set; }
    public decimal Minus3Sigma { get; set; }
}

/// <summary>
/// Histogram query parameters DTO.
/// </summary>
public class HistogramQueryDto
{
    public string DivSeq { get; set; } = string.Empty;
    public string SpecSysId { get; set; } = string.Empty;
    public string? StartDate { get; set; }
    public string? EndDate { get; set; }
    public string? Shift { get; set; }
    public int? BinCount { get; set; }
    public decimal? BinWidth { get; set; }
    public bool IncludeNormalCurve { get; set; } = true;
}

#endregion

#region Box Plot DTOs

/// <summary>
/// Box plot data DTO.
/// </summary>
public class BoxPlotDataDto
{
    public string SpecSysId { get; set; } = string.Empty;
    public string SpecName { get; set; } = string.Empty;
    public List<BoxPlotItemDto> Items { get; set; } = new();
    public ChartMetadataDto Metadata { get; set; } = new();
}

/// <summary>
/// Box plot item DTO.
/// </summary>
public class BoxPlotItemDto
{
    public string Category { get; set; } = string.Empty;
    public string Label { get; set; } = string.Empty;
    public decimal Min { get; set; }
    public decimal Q1 { get; set; }
    public decimal Median { get; set; }
    public decimal Q3 { get; set; }
    public decimal Max { get; set; }
    public decimal Mean { get; set; }
    public List<decimal> Outliers { get; set; } = new();
    public int SampleCount { get; set; }
}

#endregion
