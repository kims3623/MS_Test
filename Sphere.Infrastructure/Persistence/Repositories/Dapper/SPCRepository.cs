using System.Data;
using Sphere.Application.DTOs.Alarm;
using Sphere.Application.DTOs.SPC;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Infrastructure.Persistence.Repositories.Dapper;

/// <summary>
/// SPC repository implementation using Dapper for stored procedures.
/// </summary>
public class SPCRepository : DapperRepositoryBase, ISPCRepository
{
    public SPCRepository(IDbConnection connection) : base(connection) { }

    #region Basic Chart Data

    /// <inheritdoc />
    /// <remarks>
    /// DB USP: USP_SPC_SPH3010_CHART
    /// DB params: @P_Lang_Type, @P_div_seq, @P_ret_type, @P_work_date_fr, @P_work_date_to, @P_spec_sys_id
    /// Note: 16 params reduced to 6. ret_type = 'SUMDATA' or 'RAWDATA'.
    /// </remarks>
    public async Task<IEnumerable<ChartDataResultDto>> GetChartDataAsync(
        ChartDataQueryDto query,
        CancellationToken cancellationToken = default)
    {
        return await QueryAsync<ChartDataResultDto>(
            "USP_SPC_SPH3010_CHART",
            new
            {
                Lang_Type = "ko-KR",
                div_seq = query.DivSeq,
                ret_type = query.ChartType ?? "SUMDATA",     // 'SUMDATA' or 'RAWDATA'
                work_date_fr = query.StartDate,
                work_date_to = query.EndDate,
                spec_sys_id = query.SpecSysId
            });
    }

    /// <inheritdoc />
    /// <remarks>
    /// TODO: [TVP] Migrate to USP_SPC_INIT_CL_CALC - requires Table-Valued Parameter (TVP).
    /// DB USP: USP_SPC_INIT_CL_CALC uses TVP for statistics calculation.
    /// Current implementation uses placeholder USP name; will be replaced in Phase E (TVP migration).
    /// </remarks>
    public async Task<StatisticsCalcResultDto?> CalculateStatisticsAsync(
        StatisticsCalcQueryDto query,
        CancellationToken cancellationToken = default)
    {
        // Fetch raw chart data via existing SP
        var rawData = (await QueryAsync<ChartDataResultDto>(
            "USP_SPC_SPH3010_CHART",
            new
            {
                Lang_Type = "ko-KR",
                div_seq = query.DivSeq,
                ret_type = "RAWDATA",
                work_date_fr = query.StartDate,
                work_date_to = query.EndDate,
                spec_sys_id = query.SpecSysId
            })).ToList();

        var values = rawData.Where(r => r.Value != 0).Select(r => r.Value).ToList();
        if (values.Count == 0) return null;

        // SPC statistics calculation
        var mean = values.Average();
        var stdDev = values.Count > 1
            ? (decimal)Math.Sqrt((double)values.Sum(v => (v - mean) * (v - mean)) / (values.Count - 1))
            : 0m;
        var usl = rawData.First().Usl;
        var lsl = rawData.First().Lsl;

        return new StatisticsCalcResultDto
        {
            SpecSysId = query.SpecSysId,
            Mean = mean,
            StdDev = stdDev,
            Min = values.Min(),
            Max = values.Max(),
            Range = values.Max() - values.Min(),
            SampleCount = values.Count,
            Cp = stdDev > 0 ? (usl - lsl) / (6 * stdDev) : 0,
            CpU = stdDev > 0 ? (usl - mean) / (3 * stdDev) : 0,
            CpL = stdDev > 0 ? (mean - lsl) / (3 * stdDev) : 0,
            Cpk = stdDev > 0 ? Math.Min((usl - mean) / (3 * stdDev), (mean - lsl) / (3 * stdDev)) : 0,
            Pp = stdDev > 0 ? (usl - lsl) / (6 * stdDev) : 0,
            Ppk = stdDev > 0 ? Math.Min((usl - mean) / (3 * stdDev), (mean - lsl) / (3 * stdDev)) : 0,
            DefectCount = rawData.Count(r => r.Value > usl || r.Value < lsl),
            DefectRate = values.Count > 0 ? (decimal)rawData.Count(r => r.Value > usl || r.Value < lsl) / values.Count * 100 : 0
        };
    }

    #endregion

    #region X-Bar R Chart

    /// <inheritdoc />
    /// <remarks>
    /// DB USP: USP_SPC_SPH3010_CHART (same USP as GetChartDataAsync, ret_type = 'SUMDATA')
    /// </remarks>
    public async Task<XBarRChartDto> GetXBarRChartAsync(
        ChartDataQueryDto query,
        CancellationToken cancellationToken = default)
    {
        using var multi = await QueryMultipleAsync(
            "USP_SPC_SPH3010_CHART",
            new
            {
                Lang_Type = "ko-KR",
                div_seq = query.DivSeq,
                ret_type = "SUMDATA",
                work_date_fr = query.StartDate,
                work_date_to = query.EndDate,
                spec_sys_id = query.SpecSysId
            });

        var metadata = await multi.ReadFirstOrDefaultAsync<dynamic>();
        var dataPoints = await multi.ReadAsync<XBarRDataPointDto>();
        var statistics = await multi.ReadFirstOrDefaultAsync<XBarRStatisticsDto>();

        return new XBarRChartDto
        {
            SpecSysId = query.SpecSysId ?? string.Empty,
            SpecName = metadata?.spec_name ?? string.Empty,
            XBarLimits = new ControlLimitsDto
            {
                Ucl = metadata?.xbar_ucl ?? 0,
                Cl = metadata?.xbar_cl ?? 0,
                Lcl = metadata?.xbar_lcl ?? 0,
                Usl = metadata?.usl,
                Lsl = metadata?.lsl,
                Target = metadata?.target
            },
            RLimits = new ControlLimitsDto
            {
                Ucl = metadata?.r_ucl ?? 0,
                Cl = metadata?.r_cl ?? 0,
                Lcl = metadata?.r_lcl ?? 0
            },
            DataPoints = dataPoints.ToList(),
            Statistics = statistics ?? new XBarRStatisticsDto(),
            Metadata = new ChartMetadataDto
            {
                ChartType = "X-Bar R",
                Title = $"X-Bar R Chart - {metadata?.spec_name}",
                XAxisLabel = "Subgroup",
                YAxisLabel = "Value",
                DataPeriod = $"{query.StartDate} ~ {query.EndDate}",
                TotalPoints = dataPoints.Count(),
                GeneratedAt = DateTime.UtcNow
            }
        };
    }

    #endregion

    #region P Chart

    /// <inheritdoc />
    /// <remarks>
    /// DB USP: USP_SPC_SPH3010_CHART (same USP as GetChartDataAsync, ret_type = 'SUMDATA')
    /// </remarks>
    public async Task<PChartDto> GetPChartAsync(
        ChartDataQueryDto query,
        CancellationToken cancellationToken = default)
    {
        using var multi = await QueryMultipleAsync(
            "USP_SPC_SPH3010_CHART",
            new
            {
                Lang_Type = "ko-KR",
                div_seq = query.DivSeq,
                ret_type = "SUMDATA",
                work_date_fr = query.StartDate,
                work_date_to = query.EndDate,
                spec_sys_id = query.SpecSysId
            });

        var metadata = await multi.ReadFirstOrDefaultAsync<dynamic>();
        var dataPoints = await multi.ReadAsync<PChartDataPointDto>();
        var statistics = await multi.ReadFirstOrDefaultAsync<PChartStatisticsDto>();

        return new PChartDto
        {
            SpecSysId = query.SpecSysId ?? string.Empty,
            SpecName = metadata?.spec_name ?? string.Empty,
            PLimits = new ControlLimitsDto
            {
                Ucl = metadata?.p_ucl ?? 0,
                Cl = metadata?.p_cl ?? 0,
                Lcl = metadata?.p_lcl ?? 0
            },
            DataPoints = dataPoints.ToList(),
            Statistics = statistics ?? new PChartStatisticsDto(),
            Metadata = new ChartMetadataDto
            {
                ChartType = "P Chart",
                Title = $"P Chart - {metadata?.spec_name}",
                XAxisLabel = "Subgroup",
                YAxisLabel = "Proportion Defective",
                DataPeriod = $"{query.StartDate} ~ {query.EndDate}",
                TotalPoints = dataPoints.Count(),
                GeneratedAt = DateTime.UtcNow
            }
        };
    }

    #endregion

    #region Cpk Analysis

    /// <inheritdoc />
    public async Task<CpkAnalysisDto> GetCpkDataAsync(
        CpkQueryDto query,
        CancellationToken cancellationToken = default)
    {
        // DB USP: USP_SPC_SPH3020_SELECT (10 params)
        // DB params: @P_Lang_Type, @P_div_seq, @P_work_date_fr, @P_work_date_to, @P_ret_type,
        //            @P_vendor_id(MAX), @P_mtrl_class_id(MAX), @P_act_prod_id(MAX), @P_step_id(MAX), @P_RESULT_TYPE
        using var multi = await QueryMultipleAsync(
            "USP_SPC_SPH3020_SELECT",
            new
            {
                Lang_Type = "ko-KR",
                div_seq = query.DivSeq,
                work_date_fr = query.StartDate,
                work_date_to = query.EndDate,
                ret_type = "STEP",
                vendor_id = "",
                mtrl_class_id = "",
                act_prod_id = "",
                step_id = "",
                RESULT_TYPE = "DAY"
            });

        var metadata = await multi.ReadFirstOrDefaultAsync<dynamic>();
        var statistics = await multi.ReadFirstOrDefaultAsync<CpkStatisticsDto>();
        var bins = await multi.ReadAsync<HistogramBinDto>();
        var normalCurve = await multi.ReadAsync<NormalCurvePointDto>();
        var trendData = query.IncludeTrend
            ? await multi.ReadAsync<CpkTrendPointDto>()
            : Enumerable.Empty<CpkTrendPointDto>();

        var stats = statistics ?? new CpkStatisticsDto();

        return new CpkAnalysisDto
        {
            SpecSysId = query.SpecSysId,
            SpecName = metadata?.spec_name ?? string.Empty,
            Statistics = stats,
            SpecLimits = new SpecLimitsDto
            {
                Usl = metadata?.usl ?? 0,
                Lsl = metadata?.lsl ?? 0,
                Target = metadata?.target ?? 0,
                Tolerance = (metadata?.usl ?? 0) - (metadata?.lsl ?? 0),
                HasUpperSpec = metadata?.usl != null,
                HasLowerSpec = metadata?.lsl != null
            },
            Distribution = new DistributionDto
            {
                Bins = bins.ToList(),
                NormalCurve = normalCurve.ToList(),
                NormalityTestPValue = metadata?.normality_p_value ?? 0,
                IsNormallyDistributed = (metadata?.normality_p_value ?? 0) > 0.05m
            },
            TrendData = trendData.ToList(),
            Interpretation = GetCpkInterpretation(stats),
            Metadata = new ChartMetadataDto
            {
                ChartType = "Cpk Analysis",
                Title = $"Cpk Analysis - {metadata?.spec_name}",
                XAxisLabel = "Value",
                YAxisLabel = "Frequency",
                DataPeriod = $"{query.StartDate} ~ {query.EndDate}",
                TotalPoints = stats.SampleSize,
                GeneratedAt = DateTime.UtcNow
            }
        };
    }

    private static CpkInterpretationDto GetCpkInterpretation(CpkStatisticsDto stats)
    {
        var (level, recommendation) = stats.Cpk switch
        {
            >= 2.0m => ("Excellent", "Process is highly capable. Maintain current controls."),
            >= 1.67m => ("Very Good", "Process is capable. Continue monitoring."),
            >= 1.33m => ("Good", "Process is capable but could be improved."),
            >= 1.0m => ("Marginal", "Process needs improvement. Investigate variation sources."),
            >= 0.67m => ("Poor", "Process is not capable. Immediate action required."),
            _ => ("Very Poor", "Process is severely incapable. Stop production and investigate.")
        };

        return new CpkInterpretationDto
        {
            CapabilityLevel = level,
            Recommendation = recommendation,
            MeetsRequirement = stats.Cpk >= 1.33m,
            TargetCpk = 1.33m,
            GapToTarget = 1.33m - stats.Cpk,
            ImprovementSuggestions = GetImprovementSuggestions(stats)
        };
    }

    private static List<string> GetImprovementSuggestions(CpkStatisticsDto stats)
    {
        var suggestions = new List<string>();

        if (stats.Cpk < stats.Cp)
            suggestions.Add("Center the process closer to target - process mean is off-center.");

        if (stats.StdDev > 0 && stats.Cp < 1.33m)
            suggestions.Add("Reduce process variation - standard deviation is too high.");

        if (Math.Abs(stats.Skewness) > 0.5m)
            suggestions.Add("Investigate asymmetric distribution - data is skewed.");

        if (stats.Kurtosis > 3.5m)
            suggestions.Add("Check for outliers - distribution has heavy tails.");

        return suggestions;
    }

    #endregion

    #region Control Limits

    /// <inheritdoc />
    public async Task<ControlLimitsResponseDto> GetControlLimitsAsync(
        string specSysId,
        string chartType,
        CancellationToken cancellationToken = default)
    {
        var sql = @"
            SELECT spec_sys_id, ucl, cl, lcl, usl, lsl, target,
                   update_user_id, CONVERT(VARCHAR(20), update_date, 120) AS update_date
            FROM SPC_SPEC_MST
            WHERE spec_sys_id = @spec_sys_id";

        var current = await QueryFirstOrDefaultRawSqlAsync<ControlLimitsDto>(sql, new
        {
            spec_sys_id = specSysId
        });

        return new ControlLimitsResponseDto
        {
            Current = current ?? new ControlLimitsDto(),
            History = new List<ControlLimitsHistoryDto>()
        };
    }

    /// <inheritdoc />
    /// <remarks>
    /// No SP exists for control limit update. Uses direct SQL UPDATE on SPC_SPEC_MST.
    /// </remarks>
    public async Task<bool> UpdateControlLimitsAsync(
        ControlLimitsUpdateDto request,
        string userId,
        CancellationToken cancellationToken = default)
    {
        var sql = @"
            UPDATE SPC_SPEC_MST
            SET ucl = @ucl, cl = @cl, lcl = @lcl,
                usl = @usl, lsl = @lsl, target = @target,
                update_user_id = @user_id, update_date = GETDATE()
            WHERE spec_sys_id = @spec_sys_id";

        var affected = await ExecuteRawSqlAsync(sql, new
        {
            spec_sys_id = request.SpecSysId,
            ucl = request.Ucl,
            cl = request.Cl,
            lcl = request.Lcl,
            usl = request.Usl,
            lsl = request.Lsl,
            target = request.Target,
            user_id = userId
        });

        return affected > 0;
    }

    #endregion

    #region Run Rules

    /// <inheritdoc />
    /// <remarks>
    /// DB USP: USP_SPC_DEFAULT_RUNRULE_REL_SELECT
    /// DB params: @P_Lang_Type, @P_div_seq, @P_vendor_id, @P_mtrl_class_id,
    ///            @P_step_id, @P_item_id, @P_measm_id, @P_use_yn
    /// Note: Method signature only accepts specSysId but DB USP requires different params.
    ///       Currently passing empty strings for unavailable params; method signature may need
    ///       to be extended in a future iteration.
    /// </remarks>
    public async Task<RunRulesConfigDto> GetRunRulesAsync(
        string specSysId,
        CancellationToken cancellationToken = default)
    {
        using var multi = await QueryMultipleAsync(
            "USP_SPC_DEFAULT_RUNRULE_REL_SELECT",
            new
            {
                Lang_Type = "ko-KR",
                div_seq = "",
                vendor_id = "",
                mtrl_class_id = "",
                step_id = "",
                item_id = "",
                measm_id = "",
                use_yn = "Y"
            });

        var metadata = await multi.ReadFirstOrDefaultAsync<dynamic>();
        var rules = await multi.ReadAsync<RunRuleDto>();

        return new RunRulesConfigDto
        {
            SpecSysId = specSysId,
            Rules = rules.ToList(),
            LastUpdatedBy = metadata?.updated_by ?? string.Empty,
            LastUpdatedAt = metadata?.updated_at ?? DateTime.UtcNow
        };
    }

    /// <inheritdoc />
    public async Task<RunRuleCheckResponseDto> CheckRunRulesAsync(
        RunRuleCheckRequestDto request,
        CancellationToken cancellationToken = default)
    {
        // Fetch chart data
        var chartData = (await QueryAsync<ChartDataResultDto>(
            "USP_SPC_SPH3010_CHART",
            new
            {
                Lang_Type = "ko-KR",
                div_seq = request.DivSeq,
                ret_type = "SUMDATA",
                work_date_fr = request.StartDate ?? "",
                work_date_to = request.EndDate ?? "",
                spec_sys_id = request.SpecSysId
            })).OrderBy(d => d.WorkDate).ThenBy(d => d.SubgroupNo).ToList();

        if (chartData.Count == 0)
        {
            return new RunRuleCheckResponseDto
            {
                SpecSysId = request.SpecSysId, TotalPoints = 0, TotalViolations = 0,
                Violations = new(), RuleSummary = new(),
                Metadata = new ChartMetadataDto { ChartType = "Run Rule Check", TotalPoints = 0, GeneratedAt = DateTime.UtcNow }
            };
        }

        var values = chartData.Select(d => d.Value).ToList();
        var cl = chartData.First().Cl;
        var ucl = chartData.First().Ucl;
        var lcl = chartData.First().Lcl;
        var sigma = ucl != cl ? (ucl - cl) / 3 : 0m;

        var violations = new List<RunRuleViolationDto>();

        // WE Rule 1: One point beyond 3 sigma
        for (int i = 0; i < values.Count; i++)
        {
            if (values[i] > cl + 3 * sigma || values[i] < cl - 3 * sigma)
                violations.Add(NewViolation(WesternElectricRules.Rule1_Beyond3Sigma, "Beyond 3 sigma", chartData[i], i, "HIGH"));
        }

        // WE Rule 2: Two of three consecutive points beyond 2 sigma (same side)
        for (int i = 2; i < values.Count; i++)
        {
            var window = new[] { values[i - 2], values[i - 1], values[i] };
            var above = window.Count(v => v > cl + 2 * sigma);
            var below = window.Count(v => v < cl - 2 * sigma);
            if (above >= 2 || below >= 2)
                violations.Add(NewViolation(WesternElectricRules.Rule2_TwoOfThreeBeyond2Sigma, "2 of 3 beyond 2 sigma", chartData[i], i, "MEDIUM", new[] { i - 2, i - 1, i }));
        }

        // WE Rule 3: Four of five consecutive points beyond 1 sigma (same side)
        for (int i = 4; i < values.Count; i++)
        {
            var window = new[] { values[i - 4], values[i - 3], values[i - 2], values[i - 1], values[i] };
            var above = window.Count(v => v > cl + sigma);
            var below = window.Count(v => v < cl - sigma);
            if (above >= 4 || below >= 4)
                violations.Add(NewViolation(WesternElectricRules.Rule3_FourOfFiveBeyond1Sigma, "4 of 5 beyond 1 sigma", chartData[i], i, "MEDIUM", Enumerable.Range(i - 4, 5).ToArray()));
        }

        // WE Rule 4: Eight consecutive points on same side of center
        for (int i = 7; i < values.Count; i++)
        {
            var window = Enumerable.Range(i - 7, 8).Select(j => values[j]).ToArray();
            if (window.All(v => v > cl) || window.All(v => v < cl))
                violations.Add(NewViolation(WesternElectricRules.Rule4_EightOneSide, "8 points same side", chartData[i], i, "LOW", Enumerable.Range(i - 7, 8).ToArray()));
        }

        // Deduplicate by SubgroupNo+RuleId
        violations = violations.GroupBy(v => $"{v.RuleId}_{v.SubgroupNo}").Select(g => g.First()).ToList();

        var ruleSummary = violations.GroupBy(v => new { v.RuleId, v.RuleName })
            .Select(g => new RunRuleSummaryDto
            {
                RuleId = g.Key.RuleId, RuleName = g.Key.RuleName,
                ViolationCount = g.Count(),
                ViolationRate = chartData.Count > 0 ? Math.Round((decimal)g.Count() / chartData.Count * 100, 2) : 0,
                LastViolationDate = g.Last().WorkDate
            }).ToList();

        return new RunRuleCheckResponseDto
        {
            SpecSysId = request.SpecSysId,
            TotalPoints = chartData.Count,
            TotalViolations = violations.Count,
            Violations = violations,
            RuleSummary = ruleSummary,
            Metadata = new ChartMetadataDto
            {
                ChartType = "Run Rule Check", Title = "Western Electric Run Rule Check",
                DataPeriod = $"{request.StartDate} ~ {request.EndDate}",
                TotalPoints = chartData.Count, GeneratedAt = DateTime.UtcNow
            }
        };
    }

    private static RunRuleViolationDto NewViolation(string ruleId, string ruleName, ChartDataResultDto data, int idx, string severity, int[]? affected = null)
    {
        return new RunRuleViolationDto
        {
            RuleId = ruleId, RuleName = ruleName,
            SubgroupNo = data.SubgroupNo > 0 ? data.SubgroupNo : idx + 1,
            WorkDate = data.WorkDate, Shift = data.Shift,
            Value = data.Value, Description = ruleName, Severity = severity,
            AffectedPoints = affected?.ToList() ?? new List<int> { idx }
        };
    }

    #endregion

    #region Day/Month/Trend Analysis

    /// <inheritdoc />
    /// <remarks>
    /// DB USP: USP_SPC_SPH3010_SELECT (16 params)
    /// DB params: @P_Lang_Type, @P_div_seq, @P_work_date_fr, @P_work_date_to, @P_stat_type_id,
    ///            @P_vendor_id, @P_mtrl_class_id, @P_act_prod_id, @P_project_id, @P_mtrl_id,
    ///            @P_step_id, @P_item_id, @P_measm_id, @P_sum_type, @P_except_defect, @P_spec_sys_id
    /// </remarks>
    public async Task<DayAnalysisDto> GetDayAnalysisAsync(
        DayAnalysisQueryDto query,
        CancellationToken cancellationToken = default)
    {
        using var multi = await QueryMultipleAsync(
            "USP_SPC_SPH3010_SELECT",
            new
            {
                Lang_Type = "ko-KR",
                div_seq = query.DivSeq,
                work_date_fr = query.StartDate,
                work_date_to = query.EndDate,
                stat_type_id = "",
                vendor_id = "",
                mtrl_class_id = "",
                act_prod_id = "",
                project_id = "",
                mtrl_id = "",
                step_id = "",
                item_id = "",
                measm_id = "",
                sum_type = "SUM_INPUT",
                except_defect = "N",
                spec_sys_id = query.SpecSysId ?? ""
            });

        var metadata = await multi.ReadFirstOrDefaultAsync<dynamic>();
        var items = await multi.ReadAsync<DayAnalysisItemDto>();
        var summary = await multi.ReadFirstOrDefaultAsync<DayAnalysisSummaryDto>();

        return new DayAnalysisDto
        {
            SpecSysId = query.SpecSysId,
            SpecName = metadata?.spec_name ?? string.Empty,
            AnalysisDate = DateTime.UtcNow.ToString("yyyy-MM-dd"),
            Items = items.ToList(),
            Summary = summary ?? new DayAnalysisSummaryDto(),
            Metadata = new ChartMetadataDto
            {
                ChartType = "Day Analysis",
                Title = $"Daily Analysis - {metadata?.spec_name}",
                XAxisLabel = "Date",
                YAxisLabel = "Value",
                DataPeriod = $"{query.StartDate} ~ {query.EndDate}",
                TotalPoints = items.Count(),
                GeneratedAt = DateTime.UtcNow
            }
        };
    }

    /// <inheritdoc />
    /// <remarks>
    /// DB USP: USP_SPC_SPH3030_SELECT (12 params)
    /// DB params: @P_Lang_Type, @P_div_seq, @P_work_date_fr, @P_work_date_to,
    ///            @P_vendor_id, @P_mtrl_class_id, @P_act_prod_id, @P_step_id,
    ///            @P_RET_TYPE, @P_WORST_ROW, @P_defect_id, @P_RESULT_TYPE
    /// </remarks>
    public async Task<MonthAnalysisDto> GetMonthAnalysisAsync(
        MonthAnalysisQueryDto query,
        CancellationToken cancellationToken = default)
    {
        using var multi = await QueryMultipleAsync(
            "USP_SPC_SPH3030_SELECT",
            new
            {
                Lang_Type = "ko-KR",
                div_seq = query.DivSeq,
                work_date_fr = query.Year != null ? $"{query.Year}0101" : DateTime.Now.ToString("yyyy") + "0101",
                work_date_to = query.Year != null ? $"{query.Year}1231" : DateTime.Now.ToString("yyyy") + "1231",
                vendor_id = "",
                mtrl_class_id = "",
                act_prod_id = "",
                step_id = "",
                RET_TYPE = "*",
                WORST_ROW = 10,
                defect_id = "",
                RESULT_TYPE = "YMW"
            });

        var metadata = await multi.ReadFirstOrDefaultAsync<dynamic>();
        var items = await multi.ReadAsync<MonthAnalysisItemDto>();
        var summary = await multi.ReadFirstOrDefaultAsync<MonthAnalysisSummaryDto>();
        var comparison = query.IncludeYearComparison
            ? await multi.ReadAsync<MonthComparisonDto>()
            : Enumerable.Empty<MonthComparisonDto>();

        return new MonthAnalysisDto
        {
            SpecSysId = query.SpecSysId,
            SpecName = metadata?.spec_name ?? string.Empty,
            Year = query.Year ?? DateTime.Now.Year.ToString(),
            Items = items.ToList(),
            Summary = summary ?? new MonthAnalysisSummaryDto(),
            YearComparison = comparison.ToList(),
            Metadata = new ChartMetadataDto
            {
                ChartType = "Month Analysis",
                Title = $"Monthly Analysis - {metadata?.spec_name}",
                XAxisLabel = "Month",
                YAxisLabel = "Cpk",
                DataPeriod = query.Year ?? DateTime.Now.Year.ToString(),
                TotalPoints = items.Count(),
                GeneratedAt = DateTime.UtcNow
            }
        };
    }

    /// <inheritdoc />
    public async Task<TrendAnalysisDto> GetTrendAnalysisAsync(
        TrendAnalysisQueryDto query,
        CancellationToken cancellationToken = default)
    {
        // Fetch chart data via USP_SPC_SPH3010_CHART
        var rawData = (await QueryAsync<ChartDataResultDto>(
            "USP_SPC_SPH3010_CHART",
            new
            {
                Lang_Type = "ko-KR",
                div_seq = query.DivSeq,
                ret_type = "SUMDATA",
                work_date_fr = query.StartDate ?? "",
                work_date_to = query.EndDate ?? "",
                spec_sys_id = query.SpecSysId
            })).OrderBy(d => d.WorkDate).ThenBy(d => d.SubgroupNo).ToList();

        if (rawData.Count == 0)
        {
            return new TrendAnalysisDto
            {
                SpecSysId = query.SpecSysId, DataPoints = new(), Statistics = new(),
                Forecast = new TrendForecastDto { ForecastPoints = new(), ConfidenceLevel = 0.95m, ForecastMethod = "Linear Regression" },
                Metadata = new ChartMetadataDto { ChartType = "Trend Analysis", TotalPoints = 0, GeneratedAt = DateTime.UtcNow }
            };
        }

        var values = rawData.Select(d => d.Value).ToList();
        var window = query.MovingAvgWindow > 0 ? query.MovingAvgWindow : 7;

        // Build data points with moving average
        var dataPoints = new List<TrendDataPointDto>();
        for (int i = 0; i < rawData.Count; i++)
        {
            var maStart = Math.Max(0, i - window + 1);
            var maWindow = values.Skip(maStart).Take(i - maStart + 1).ToList();
            var movingAvg = maWindow.Average();
            dataPoints.Add(new TrendDataPointDto
            {
                Period = rawData[i].WorkDate,
                PeriodLabel = rawData[i].WorkDate,
                Value = rawData[i].Value,
                MovingAvg = Math.Round(movingAvg, 4),
                UpperBound = rawData[i].Ucl,
                LowerBound = rawData[i].Lcl,
                SampleCount = 1,
                IsAnomaly = rawData[i].Value > rawData[i].Ucl || rawData[i].Value < rawData[i].Lcl
            });
        }

        // Linear regression: y = slope*x + intercept
        var n = values.Count;
        var sumX = (decimal)Enumerable.Range(0, n).Sum();
        var sumY = values.Sum();
        var sumXX = (decimal)Enumerable.Range(0, n).Sum(x => x * x);
        var sumXY = Enumerable.Range(0, n).Sum(i => i * values[i]);

        var slope = n > 1 && (n * sumXX - sumX * sumX) != 0
            ? (n * sumXY - sumX * sumY) / (n * sumXX - sumX * sumX)
            : 0m;
        var intercept = n > 0 ? (sumY - slope * sumX) / n : 0m;

        // R-squared
        var mean = values.Average();
        var ssTotal = values.Sum(v => (v - mean) * (v - mean));
        var ssResidual = Enumerable.Range(0, n).Sum(i =>
        {
            var predicted = slope * i + intercept;
            return (values[i] - predicted) * (values[i] - predicted);
        });
        var rSquared = ssTotal != 0 ? Math.Round(1 - ssResidual / ssTotal, 4) : 0m;
        var correlation = rSquared >= 0 ? Math.Round((decimal)Math.Sqrt((double)Math.Abs(rSquared)) * Math.Sign(slope), 4) : 0m;

        var trendDirection = slope > 0.001m ? "INCREASING" : slope < -0.001m ? "DECREASING" : "STABLE";

        // Forecast (optional)
        var forecastPoints = new List<TrendForecastPointDto>();
        if (query.IncludeForecast && n > 2)
        {
            var stdErr = n > 2
                ? (decimal)Math.Sqrt((double)(ssResidual / (n - 2)))
                : 0m;
            for (int i = 0; i < query.ForecastPeriods; i++)
            {
                var x = n + i;
                var predicted = slope * x + intercept;
                forecastPoints.Add(new TrendForecastPointDto
                {
                    Period = $"F+{i + 1}",
                    PredictedValue = Math.Round(predicted, 4),
                    UpperConfidence = Math.Round(predicted + 1.96m * stdErr, 4),
                    LowerConfidence = Math.Round(predicted - 1.96m * stdErr, 4)
                });
            }
        }

        return new TrendAnalysisDto
        {
            SpecSysId = query.SpecSysId,
            SpecName = string.Empty,
            DataPoints = dataPoints,
            Statistics = new TrendStatisticsDto
            {
                Slope = Math.Round(slope, 6),
                Intercept = Math.Round(intercept, 4),
                RSquared = rSquared,
                Correlation = correlation,
                TrendDirection = trendDirection,
                TrendStrength = Math.Abs(rSquared),
                SeasonalityIndex = 0,
                DataPointCount = n
            },
            Forecast = new TrendForecastDto
            {
                ForecastPoints = forecastPoints,
                ConfidenceLevel = 0.95m,
                ForecastMethod = "Linear Regression"
            },
            Metadata = new ChartMetadataDto
            {
                ChartType = "Trend Analysis",
                Title = "Trend Analysis with Forecast",
                XAxisLabel = "Period",
                YAxisLabel = "Value",
                DataPeriod = $"{query.StartDate} ~ {query.EndDate}",
                TotalPoints = n,
                GeneratedAt = DateTime.UtcNow
            }
        };
    }

    /// <inheritdoc />
    /// <remarks>
    /// DB USP: USP_SPC_YIELD_SPEC_MST_SELECT
    /// DB params: @P_Lang_Type, @P_div_seq, @P_vendor_id(DEFAULT ''), @P_mtrl_class_id(DEFAULT ''),
    ///            @P_act_prod_id(DEFAULT ''), @P_step_id(DEFAULT ''), @P_use_yn(DEFAULT '')
    /// </remarks>
    public async Task<YieldSpecDataDto> GetYieldSpecDataAsync(
        string divSeq,
        string? vendorId = null,
        string? mtrlClassId = null,
        CancellationToken cancellationToken = default)
    {
        using var multi = await QueryMultipleAsync(
            "USP_SPC_YIELD_SPEC_MST_SELECT",
            new
            {
                Lang_Type = "ko-KR",
                div_seq = divSeq,
                vendor_id = vendorId ?? string.Empty,
                mtrl_class_id = mtrlClassId ?? string.Empty,
                act_prod_id = string.Empty,
                step_id = string.Empty,
                use_yn = string.Empty
            });

        var items = await multi.ReadAsync<YieldSpecItemDto>();
        YieldSpecSummaryDto? summary = null;
        if (!multi.IsConsumed)
        {
            summary = await multi.ReadFirstOrDefaultAsync<YieldSpecSummaryDto>();
        }

        return new YieldSpecDataDto
        {
            DivSeq = divSeq,
            Items = items.ToList(),
            Summary = summary ?? new YieldSpecSummaryDto(),
            Metadata = new ChartMetadataDto
            {
                ChartType = "Yield Spec",
                Title = "Yield by Specification",
                XAxisLabel = "Spec",
                YAxisLabel = "Yield Rate",
                TotalPoints = items.Count(),
                GeneratedAt = DateTime.UtcNow
            }
        };
    }

    #endregion

    #region Histogram & Pareto

    /// <inheritdoc />
    public async Task<HistogramDataDto> GetHistogramDataAsync(
        HistogramQueryDto query,
        CancellationToken cancellationToken = default)
    {
        // Fetch raw data from USP_SPC_SPH3010_CHART with ret_type='RAWDATA'
        var rawData = (await QueryAsync<ChartDataResultDto>(
            "USP_SPC_SPH3010_CHART",
            new
            {
                Lang_Type = "ko-KR",
                div_seq = query.DivSeq,
                ret_type = "RAWDATA",
                work_date_fr = query.StartDate ?? "",
                work_date_to = query.EndDate ?? "",
                spec_sys_id = query.SpecSysId
            })).ToList();

        var values = rawData.Where(r => r.Value != 0).Select(r => r.Value).OrderBy(v => v).ToList();

        if (values.Count == 0)
        {
            return new HistogramDataDto
            {
                SpecSysId = query.SpecSysId, Bins = new(), Statistics = new(),
                SpecLines = new(), NormalCurve = new(),
                Metadata = new ChartMetadataDto { ChartType = "Histogram", TotalPoints = 0, GeneratedAt = DateTime.UtcNow }
            };
        }

        var mean = values.Average();
        var stdDev = values.Count > 1
            ? (decimal)Math.Sqrt((double)values.Sum(v => (v - mean) * (v - mean)) / (values.Count - 1))
            : 0m;
        var min = values.Min();
        var max = values.Max();
        var range = max - min;

        // Bin calculation
        var binCount = query.BinCount ?? Math.Max(5, (int)Math.Ceiling(Math.Sqrt(values.Count)));
        var binWidth = query.BinWidth ?? (range > 0 ? range / binCount : 1m);

        var bins = new List<HistogramBinDto>();
        var cumFreq = 0m;
        for (int i = 0; i < binCount; i++)
        {
            var binStart = min + i * binWidth;
            var binEnd = binStart + binWidth;
            var freq = i == binCount - 1
                ? values.Count(v => v >= binStart && v <= binEnd)
                : values.Count(v => v >= binStart && v < binEnd);
            cumFreq += (decimal)freq / values.Count;
            bins.Add(new HistogramBinDto
            {
                BinStart = binStart, BinEnd = binEnd, BinCenter = binStart + binWidth / 2,
                Frequency = freq, RelativeFrequency = Math.Round((decimal)freq / values.Count, 4),
                CumulativeFrequency = Math.Round(cumFreq, 4)
            });
        }

        // Spec lines
        var firstData = rawData.First();
        var specLines = new HistogramSpecLinesDto
        {
            Usl = firstData.Usl != 0 ? firstData.Usl : null,
            Lsl = firstData.Lsl != 0 ? firstData.Lsl : null,
            Target = firstData.Target != 0 ? firstData.Target : null,
            Mean = mean,
            Plus1Sigma = mean + stdDev, Minus1Sigma = mean - stdDev,
            Plus2Sigma = mean + 2 * stdDev, Minus2Sigma = mean - 2 * stdDev,
            Plus3Sigma = mean + 3 * stdDev, Minus3Sigma = mean - 3 * stdDev
        };

        // Normal curve (optional)
        var normalCurve = new List<NormalCurvePointDto>();
        if (query.IncludeNormalCurve && stdDev > 0)
        {
            var step = range / 50m;
            for (decimal x = min - stdDev; x <= max + stdDev; x += step > 0 ? step : 0.1m)
            {
                var z = (double)(x - mean) / (double)stdDev;
                var y = (decimal)(Math.Exp(-z * z / 2) / (Math.Sqrt(2 * Math.PI) * (double)stdDev)) * values.Count * binWidth;
                normalCurve.Add(new NormalCurvePointDto { X = Math.Round(x, 4), Y = Math.Round(y, 4) });
            }
        }

        // Quartiles
        decimal Percentile(List<decimal> sorted, double p)
        {
            var idx = p * (sorted.Count - 1);
            var lo = (int)Math.Floor(idx);
            var hi = (int)Math.Ceiling(idx);
            return lo == hi ? sorted[lo] : sorted[lo] + (sorted[hi] - sorted[lo]) * (decimal)(idx - lo);
        }
        var q1 = Percentile(values, 0.25);
        var median = Percentile(values, 0.5);
        var q3 = Percentile(values, 0.75);

        return new HistogramDataDto
        {
            SpecSysId = query.SpecSysId,
            SpecName = string.Empty,
            Bins = bins,
            Statistics = new HistogramStatisticsDto
            {
                TotalCount = values.Count, Mean = Math.Round(mean, 4), Median = Math.Round(median, 4),
                StdDev = Math.Round(stdDev, 4), Variance = Math.Round(stdDev * stdDev, 4),
                Min = min, Max = max, Range = range,
                Q1 = Math.Round(q1, 4), Q3 = Math.Round(q3, 4), IQR = Math.Round(q3 - q1, 4)
            },
            SpecLines = specLines,
            NormalCurve = normalCurve,
            Metadata = new ChartMetadataDto
            {
                ChartType = "Histogram", Title = "Distribution Analysis",
                XAxisLabel = "Value", YAxisLabel = "Frequency",
                DataPeriod = $"{query.StartDate} ~ {query.EndDate}",
                TotalPoints = values.Count, GeneratedAt = DateTime.UtcNow
            }
        };
    }

    /// <inheritdoc />
    public async Task<ParetoDataDto> GetParetoDataAsync(
        ParetoQueryDto query,
        CancellationToken cancellationToken = default)
    {
        // Fetch alarm data via USP_SPC_SPH4020_SELECT for defect/alarm-based Pareto analysis
        var alarmData = (await QueryAsync<AlarmListItemDto>(
            "USP_SPC_SPH4020_SELECT",
            new
            {
                Lang_Type = "ko-KR",
                div_seq = query.DivSeq,
                Search_Type = "Total",
                My_vendor_id = "",
                login_id = "",
                vendor_id = "",
                start_date = query.StartDate ?? "",
                end_date = query.EndDate ?? "",
                alm_sys_id = "",
                alm_action_id = "",
                alm_proc_id = "",
                mtrl_class_id = "",
                mtrl_id = "",
                act_prod_id = "",
                project_id = "",
                step_id = "",
                item_id = "",
                measm_id = "",
                alm_proc_001_YN = "N",
                stop_YN = "N"
            })).ToList();

        // Group by analysis type
        var grouped = query.AnalysisType?.ToLower() switch
        {
            "vendor" => alarmData.GroupBy(x => new { Id = x.VendorId, Name = x.VendorName }),
            "material" => alarmData.GroupBy(x => new { Id = x.MtrlClassId, Name = x.MtrlClassName }),
            _ => alarmData.GroupBy(x => new { Id = x.AlmProcId, Name = x.AlmProcName }) // "defect" default
        };

        var sorted = grouped.Select(g => new { g.Key.Id, g.Key.Name, Count = g.Count() })
            .OrderByDescending(x => x.Count).ToList();

        if (query.TopN.HasValue && query.TopN > 0)
            sorted = sorted.Take(query.TopN.Value).ToList();

        var totalCount = sorted.Sum(x => x.Count);
        var cumCount = 0;
        var categories = sorted.Select((item, idx) =>
        {
            cumCount += item.Count;
            var cumPct = totalCount > 0 ? Math.Round((decimal)cumCount / totalCount * 100, 2) : 0m;
            return new ParetoCategoryDto
            {
                Rank = idx + 1,
                CategoryId = item.Id,
                CategoryName = item.Name,
                CategoryNameK = item.Name,
                CategoryNameE = item.Name,
                Count = item.Count,
                Percentage = totalCount > 0 ? Math.Round((decimal)item.Count / totalCount * 100, 2) : 0,
                CumulativeCount = cumCount,
                CumulativePercentage = cumPct,
                IsVitalFew = cumPct <= 80
            };
        }).ToList();

        var vitalFew = categories.Where(c => c.IsVitalFew).ToList();

        return new ParetoDataDto
        {
            SpecSysId = query.SpecSysId,
            SpecName = string.Empty,
            AnalysisType = query.AnalysisType,
            Categories = categories,
            Summary = new ParetoSummaryDto
            {
                TotalCategories = categories.Count,
                TotalCount = totalCount,
                VitalFewCount = vitalFew.Count,
                VitalFewPercentage = vitalFew.Count > 0 ? vitalFew.Last().CumulativePercentage : 0,
                VitalFewCategories = vitalFew.Select(c => c.CategoryName).ToList(),
                EightyPercentCutoff = 80,
                TopContributor = categories.FirstOrDefault()?.CategoryName ?? "",
                TopContributorPercentage = categories.FirstOrDefault()?.Percentage ?? 0
            },
            Metadata = new ChartMetadataDto
            {
                ChartType = "Pareto",
                Title = $"Pareto Analysis - {query.AnalysisType}",
                XAxisLabel = "Category",
                YAxisLabel = "Count",
                DataPeriod = $"{query.StartDate} ~ {query.EndDate}",
                TotalPoints = categories.Count,
                GeneratedAt = DateTime.UtcNow
            }
        };
    }

    #endregion

    #region Chart Configuration

    /// <inheritdoc />
    public async Task<ChartConfigDto?> GetChartConfigAsync(
        string userId,
        string chartType,
        CancellationToken cancellationToken = default)
    {
        // TODO [P3]: USP_SPC_CHART_CONFIG_SELECT은 DB에 존재하지 않음. 사용자 차트 설정 테이블 설계 필요.
        await Task.CompletedTask;
        return null;
    }

    /// <inheritdoc />
    public async Task<bool> SaveChartConfigAsync(
        string userId,
        ChartConfigSaveRequestDto request,
        CancellationToken cancellationToken = default)
    {
        // TODO [P3]: USP_SPC_CHART_CONFIG_INSERT은 DB에 존재하지 않음. 사용자 차트 설정 테이블 설계 필요.
        await Task.CompletedTask;
        return false;
    }

    #endregion

    #region Export

    /// <inheritdoc />
    public async Task<ChartExportDataDto> GetChartExportDataAsync(
        ChartExportRequestDto request,
        CancellationToken cancellationToken = default)
    {
        var query = new ChartDataQueryDto
        {
            SpecSysId = request.SpecSysId,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            Shift = request.Shift,
            ChartType = request.ChartType
        };

        var rawData = request.IncludeRawData
            ? await GetChartDataAsync(query, cancellationToken)
            : Enumerable.Empty<ChartDataResultDto>();

        var statistics = request.IncludeStatistics
            ? await CalculateStatisticsAsync(new StatisticsCalcQueryDto
            {
                SpecSysId = request.SpecSysId,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Shift = request.Shift
            }, cancellationToken)
            : null;

        var limits = await GetControlLimitsAsync(request.SpecSysId, request.ChartType, cancellationToken);

        return new ChartExportDataDto
        {
            Header = new ChartExportHeaderDto
            {
                SpecSysId = request.SpecSysId,
                ChartType = request.ChartType,
                DataPeriod = $"{request.StartDate} ~ {request.EndDate}",
                GeneratedAt = DateTime.UtcNow,
                TotalDataPoints = rawData.Count()
            },
            RawData = rawData.ToList(),
            Statistics = statistics,
            ControlLimits = limits.Current
        };
    }

    #endregion
}
