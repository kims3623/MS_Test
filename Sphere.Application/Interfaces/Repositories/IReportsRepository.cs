using Sphere.Application.DTOs.Reports;

namespace Sphere.Application.Interfaces.Repositories;

/// <summary>
/// Reports repository interface for dashboard and report stored procedure operations.
/// </summary>
public interface IReportsRepository
{
    /// <summary>
    /// Gets aggregated dashboard data. (Multiple USPs (HOME_ISSUE_DATA + HOME_ALARM_*))
    /// </summary>
    /// <param name="divSeq">Division sequence</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Aggregated dashboard data</returns>
    Task<DashboardDataDto> GetDashboardDataAsync(string divSeq, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets home issue data. (USP_SPC_HOME_ISSUE_DATA)
    /// </summary>
    /// <param name="divSeq">Division sequence</param>
    /// <param name="filter">Optional filter</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of issue data</returns>
    Task<IEnumerable<HomeIssueDataDto>> GetHomeIssueDataAsync(
        string divSeq,
        IssueDataFilterDto? filter = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets home alarm data (grid, yearly and monthly).
    /// Calls 3 USPs: USP_SPC_HOME_ALARM_GRID, USP_SPC_HOME_ALARM_YEAR, USP_SPC_HOME_ALARM_MONTH.
    /// </summary>
    /// <param name="divSeq">Division sequence</param>
    /// <param name="filter">Alarm filter with year</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Alarm data with grid, yearly and monthly breakdown</returns>
    Task<HomeAlarmDataDto> GetHomeAlarmDataAsync(
        string divSeq,
        AlarmDataFilterDto filter,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets yield report data. (TODO [P3]: No DB USP - consider SPH3050_SELECT)
    /// </summary>
    /// <param name="divSeq">Division sequence</param>
    /// <param name="filter">Yield report filter</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Yield report data</returns>
    Task<YieldReportDto> GetYieldReportAsync(
        string divSeq,
        YieldReportFilterDto filter,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets statistics report data. (TODO [P3]: No DB USP - report redesign needed)
    /// </summary>
    /// <param name="divSeq">Division sequence</param>
    /// <param name="filter">Statistics filter</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Statistics report data</returns>
    Task<StatisticsReportDto> GetStatisticsReportAsync(
        string divSeq,
        StatisticsFilterDto filter,
        CancellationToken cancellationToken = default);
}
