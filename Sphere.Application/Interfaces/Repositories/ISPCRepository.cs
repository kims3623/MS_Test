using Sphere.Application.DTOs.SPC;

namespace Sphere.Application.Interfaces.Repositories;

/// <summary>
/// SPC repository interface for stored procedure operations.
/// </summary>
public interface ISPCRepository
{
    #region Basic Chart Data

    /// <summary>
    /// Gets chart data. (USP_SPC_SPH3010_CHART)
    /// </summary>
    Task<IEnumerable<ChartDataResultDto>> GetChartDataAsync(
        ChartDataQueryDto query,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Calculates statistics. (USP_SPC_INIT_CL_CALC (TVP - pending migration))
    /// </summary>
    Task<StatisticsCalcResultDto?> CalculateStatisticsAsync(
        StatisticsCalcQueryDto query,
        CancellationToken cancellationToken = default);

    #endregion

    #region X-Bar R Chart

    /// <summary>
    /// Gets X-Bar R chart data. (USP_SPC_SPH3010_CHART (ret_type=SUMDATA))
    /// </summary>
    Task<XBarRChartDto> GetXBarRChartAsync(
        ChartDataQueryDto query,
        CancellationToken cancellationToken = default);

    #endregion

    #region P Chart

    /// <summary>
    /// Gets P chart data. (USP_SPC_SPH3010_CHART (ret_type=SUMDATA))
    /// </summary>
    Task<PChartDto> GetPChartAsync(
        ChartDataQueryDto query,
        CancellationToken cancellationToken = default);

    #endregion

    #region Cpk Analysis

    /// <summary>
    /// Gets Cpk analysis data. (USP_SPC_SPH3020_SELECT)
    /// </summary>
    Task<CpkAnalysisDto> GetCpkDataAsync(
        CpkQueryDto query,
        CancellationToken cancellationToken = default);

    #endregion

    #region Control Limits

    /// <summary>
    /// Gets control limits. (TODO [P2]: No DB USP - EF Core migration needed)
    /// </summary>
    Task<ControlLimitsResponseDto> GetControlLimitsAsync(
        string specSysId,
        string chartType,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates control limits. (TODO [P1]: Migrate to USP_SPC_INIT_CL_CALC or appropriate USP)
    /// </summary>
    Task<bool> UpdateControlLimitsAsync(
        ControlLimitsUpdateDto request,
        string userId,
        CancellationToken cancellationToken = default);

    #endregion

    #region Run Rules

    /// <summary>
    /// Gets run rules configuration. (USP_SPC_DEFAULT_RUNRULE_REL_SELECT)
    /// </summary>
    Task<RunRulesConfigDto> GetRunRulesAsync(
        string specSysId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Checks run rule violations. (TODO [P3]: No DB USP - redesign needed)
    /// </summary>
    Task<RunRuleCheckResponseDto> CheckRunRulesAsync(
        RunRuleCheckRequestDto request,
        CancellationToken cancellationToken = default);

    #endregion

    #region Day/Month/Trend Analysis

    /// <summary>
    /// Gets day analysis data. (USP_SPC_SPH3010_SELECT)
    /// </summary>
    Task<DayAnalysisDto> GetDayAnalysisAsync(
        DayAnalysisQueryDto query,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets month analysis data. (USP_SPC_SPH3030_SELECT)
    /// </summary>
    Task<MonthAnalysisDto> GetMonthAnalysisAsync(
        MonthAnalysisQueryDto query,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets trend analysis data. (TODO [P3]: No DB USP - consider SPH3010_SELECT/SPH3030_SELECT)
    /// </summary>
    Task<TrendAnalysisDto> GetTrendAnalysisAsync(
        TrendAnalysisQueryDto query,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets yield spec data. (USP_SPC_YIELD_SPEC_MST_SELECT)
    /// </summary>
    Task<YieldSpecDataDto> GetYieldSpecDataAsync(
        string divSeq,
        string? vendorId = null,
        string? mtrlClassId = null,
        CancellationToken cancellationToken = default);

    #endregion

    #region Histogram & Pareto

    /// <summary>
    /// Gets histogram data. (TODO [P3]: No DB USP - client-side analysis)
    /// </summary>
    Task<HistogramDataDto> GetHistogramDataAsync(
        HistogramQueryDto query,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets pareto diagram data. (TODO [P3]: No DB USP - client-side analysis)
    /// </summary>
    Task<ParetoDataDto> GetParetoDataAsync(
        ParetoQueryDto query,
        CancellationToken cancellationToken = default);

    #endregion

    #region Chart Configuration

    /// <summary>
    /// Gets chart configuration. (TODO [P3]: No DB USP - chart config table design needed)
    /// </summary>
    Task<ChartConfigDto?> GetChartConfigAsync(
        string userId,
        string chartType,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Saves chart configuration. (TODO [P3]: No DB USP - chart config table design needed)
    /// </summary>
    Task<bool> SaveChartConfigAsync(
        string userId,
        ChartConfigSaveRequestDto request,
        CancellationToken cancellationToken = default);

    #endregion

    #region Export

    /// <summary>
    /// Gets chart data for export.
    /// </summary>
    Task<ChartExportDataDto> GetChartExportDataAsync(
        ChartExportRequestDto request,
        CancellationToken cancellationToken = default);

    #endregion
}
