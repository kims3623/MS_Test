using System.Data;
using Sphere.Application.DTOs.Reports;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Infrastructure.Persistence.Repositories.Dapper;

/// <summary>
/// Reports repository implementation using Dapper for stored procedures.
/// </summary>
public class ReportsRepository : DapperRepositoryBase, IReportsRepository
{
    public ReportsRepository(IDbConnection connection) : base(connection) { }

    /// <inheritdoc />
    public async Task<DashboardDataDto> GetDashboardDataAsync(
        string divSeq,
        CancellationToken cancellationToken = default)
    {
        // Execute sequentially - SqlConnection is not thread-safe even with MARS enabled
        var issueData = await GetHomeIssueDataAsync(divSeq, null, cancellationToken);
        var alarmData = await GetHomeAlarmDataAsync(divSeq, new AlarmDataFilterDto { Year = DateTime.Now.Year.ToString() }, cancellationToken);

        return new DashboardDataDto
        {
            IssueData = issueData.ToList(),
            AlarmData = alarmData,
            Summary = new DashboardSummaryDto
            {
                TotalIssues = issueData.Sum(x => x.Count),
                TotalAlarms = alarmData.YearlyData.Sum(x => x.AlmCount),
                CriticalAlerts = issueData.Count(x => x.DiffDay > 7),
                YieldRate = 0, // Will be calculated from yield report
                LastUpdated = DateTime.UtcNow
            }
        };
    }

    /// <inheritdoc />
    /// <remarks>
    /// DB USP: USP_SPC_HOME_ISSUE_DATA
    /// DB params: @P_Lang_Type, @P_div_seq, @P_work_date, @P_vendor_id
    /// </remarks>
    public async Task<IEnumerable<HomeIssueDataDto>> GetHomeIssueDataAsync(
        string divSeq,
        IssueDataFilterDto? filter = null,
        CancellationToken cancellationToken = default)
    {
        return await QueryAsync<HomeIssueDataDto>(
            "USP_SPC_HOME_ISSUE_DATA",
            new
            {
                Lang_Type = "ko-KR",
                div_seq = divSeq,
                work_date = filter?.WorkDate ?? DateTime.Now.ToString("yyyy-MM-dd"),
                vendor_id = filter?.VendorId
            });
    }

    /// <inheritdoc />
    /// <remarks>
    /// DB USPs (3 separate calls):
    ///   - USP_SPC_HOME_ALARM_GRID:  @P_Lang_Type, @P_div_seq, @P_work_date, @P_vendor_id
    ///   - USP_SPC_HOME_ALARM_YEAR:  @P_Lang_Type, @P_div_seq, @P_work_date, @P_vendor_id
    ///   - USP_SPC_HOME_ALARM_MONTH: @P_Lang_Type, @P_div_seq, @P_work_date, @P_vendor_id
    /// </remarks>
    public async Task<HomeAlarmDataDto> GetHomeAlarmDataAsync(
        string divSeq,
        AlarmDataFilterDto filter,
        CancellationToken cancellationToken = default)
    {
        var workDate = !string.IsNullOrEmpty(filter.Year)
            ? $"{filter.Year}0101"
            : DateTime.Now.ToString("yyyyMMdd");

        var parameters = new
        {
            Lang_Type = "ko-KR",
            div_seq = divSeq,
            work_date = workDate,
            vendor_id = filter.VendorId
        };

        // Execute sequentially - SqlConnection is not thread-safe even with MARS enabled
        var gridData = await QueryAsync<HomeAlarmGridDto>("USP_SPC_HOME_ALARM_GRID", parameters);
        var yearData = await QueryAsync<HomeAlarmYearDto>("USP_SPC_HOME_ALARM_YEAR", parameters);
        var monthData = await QueryAsync<HomeAlarmMonthDto>("USP_SPC_HOME_ALARM_MONTH", parameters);

        return new HomeAlarmDataDto
        {
            GridData = gridData.ToList(),
            YearlyData = yearData.ToList(),
            MonthlyData = monthData.ToList()
        };
    }

    /// <inheritdoc />
    public async Task<YieldReportDto> GetYieldReportAsync(
        string divSeq,
        YieldReportFilterDto filter,
        CancellationToken cancellationToken = default)
    {
        // Reuse USP_SPC_YIELD_SPEC_MST_SELECT (same as SPCRepository.GetYieldSpecDataAsync)
        try
        {
            using var multi = await QueryMultipleAsync(
                "USP_SPC_YIELD_SPEC_MST_SELECT",
                new
                {
                    Lang_Type = "ko-KR",
                    div_seq = divSeq,
                    vendor_id = filter.VendorId ?? "",
                    mtrl_class_id = filter.MtrlClassId ?? "",
                    act_prod_id = "",
                    step_id = "",
                    use_yn = ""
                });

            var specItems = (await multi.ReadAsync<dynamic>()).ToList();

            var items = specItems.Select(r => new YieldReportItemDto
            {
                DivSeq = divSeq,
                VendorId = (string)(r.vendor_id ?? r.VendorId ?? ""),
                VendorName = (string)(r.vendor_name ?? r.VendorName ?? ""),
                MtrlClassId = (string)(r.mtrl_class_id ?? r.MtrlClassId ?? ""),
                MtrlClassName = (string)(r.mtrl_class_name ?? r.MtrlClassName ?? ""),
                SpecId = (string)(r.spec_id ?? r.SpecId ?? ""),
                SpecName = (string)(r.spec_name ?? r.SpecName ?? ""),
                TotalCount = (int)(r.total_qty ?? r.TotalQty ?? 0),
                PassCount = (int)(r.pass_qty ?? r.PassQty ?? 0),
                FailCount = (int)(r.fail_qty ?? r.FailQty ?? 0),
                YieldRate = (decimal)(r.yield_rate ?? r.YieldRate ?? 0m)
            }).ToList();

            var totalCount = items.Sum(x => x.TotalCount);
            var totalPass = items.Sum(x => x.PassCount);
            var totalFail = items.Sum(x => x.FailCount);

            return new YieldReportDto
            {
                Items = items,
                Summary = new YieldSummaryDto
                {
                    TotalItems = items.Count,
                    TotalPass = totalPass,
                    TotalFail = totalFail,
                    OverallYieldRate = totalCount > 0 ? Math.Round((decimal)totalPass / totalCount * 100, 2) : 0,
                    DailyAvgYield = items.Count > 0 ? Math.Round(items.Average(x => x.YieldRate), 2) : 0,
                    WeeklyAvgYield = items.Count > 0 ? Math.Round(items.Average(x => x.YieldRate), 2) : 0,
                    MonthlyAvgYield = items.Count > 0 ? Math.Round(items.Average(x => x.YieldRate), 2) : 0
                }
            };
        }
        catch
        {
            return new YieldReportDto
            {
                Items = new List<YieldReportItemDto>(),
                Summary = new YieldSummaryDto()
            };
        }
    }

    /// <inheritdoc />
    public async Task<StatisticsReportDto> GetStatisticsReportAsync(
        string divSeq,
        StatisticsFilterDto filter,
        CancellationToken cancellationToken = default)
    {
        // Reuse USP_SPC_SPH3010_SELECT (same as SPCRepository.GetDayAnalysisAsync)
        try
        {
            using var multi = await QueryMultipleAsync(
                "USP_SPC_SPH3010_SELECT",
                new
                {
                    Lang_Type = "ko-KR",
                    div_seq = divSeq,
                    work_date_fr = filter.StartDate ?? "",
                    work_date_to = filter.EndDate ?? "",
                    stat_type_id = "",
                    vendor_id = filter.VendorId ?? "",
                    mtrl_class_id = "",
                    act_prod_id = "",
                    project_id = "",
                    mtrl_id = "",
                    step_id = "",
                    item_id = "",
                    measm_id = "",
                    sum_type = "SUM_INPUT",
                    except_defect = "N",
                    spec_sys_id = ""
                });

            var metadata = await multi.ReadFirstOrDefaultAsync<dynamic>();
            var rows = (await multi.ReadAsync<dynamic>()).ToList();

            var items = rows.Select(r =>
            {
                decimal val = 0;
                try { val = (decimal)(r.avg_value ?? r.AvgValue ?? r.raw_data_value ?? 0m); } catch { }
                return new StatisticsItemDto
                {
                    DivSeq = divSeq,
                    CategoryId = (string)(r.spec_sys_id ?? r.SpecSysId ?? ""),
                    CategoryName = (string)(r.spec_name ?? r.SpecName ?? ""),
                    SubCategoryId = (string)(r.vendor_id ?? r.VendorId ?? ""),
                    SubCategoryName = (string)(r.vendor_name ?? r.VendorName ?? ""),
                    Period = (string)(r.work_date ?? r.WorkDate ?? ""),
                    Value = val,
                    Unit = "",
                    ChangeRate = 0,
                    Trend = ""
                };
            }).ToList();

            var values = items.Select(x => x.Value).ToList();

            return new StatisticsReportDto
            {
                Items = items,
                Charts = new List<StatisticsChartDto>(),
                Summary = new StatisticsSummaryDto
                {
                    TotalRecords = items.Count,
                    AvgValue = values.Count > 0 ? Math.Round(values.Average(), 4) : 0,
                    MaxValue = values.Count > 0 ? values.Max() : 0,
                    MinValue = values.Count > 0 ? values.Min() : 0,
                    AnalysisPeriod = $"{filter.StartDate} ~ {filter.EndDate}"
                }
            };
        }
        catch
        {
            return new StatisticsReportDto
            {
                Items = new List<StatisticsItemDto>(),
                Charts = new List<StatisticsChartDto>(),
                Summary = new StatisticsSummaryDto
                {
                    TotalRecords = 0,
                    AnalysisPeriod = $"{filter.StartDate} ~ {filter.EndDate}"
                }
            };
        }
    }
}
