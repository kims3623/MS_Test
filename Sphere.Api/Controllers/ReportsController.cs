using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sphere.Application.DTOs.Reports;
using Sphere.Application.Features.Reports.Queries.GetDashboardData;
using Sphere.Application.Features.Reports.Queries.GetHomeAlarmData;
using Sphere.Application.Features.Reports.Queries.GetHomeIssueData;
using Sphere.Application.Features.Reports.Queries.GetStatisticsReport;
using Sphere.Application.Features.Reports.Queries.GetYieldReport;
using System.Security.Claims;

namespace Sphere.Api.Controllers;

/// <summary>
/// Reports controller for dashboard and reporting APIs.
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Authorize]
[Produces("application/json")]
public class ReportsController : ControllerBase
{
    private readonly ISender _mediator;
    private readonly ILogger<ReportsController> _logger;

    public ReportsController(ISender mediator, ILogger<ReportsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Gets aggregated dashboard data.
    /// </summary>
    /// <returns>Dashboard data including issues, alarms, and summary.</returns>
    [HttpGet("dashboard")]
    [ProducesResponseType(typeof(DashboardDataDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetDashboardData()
    {
        var divSeq = User.FindFirstValue("div_seq") ?? "OPT001";

        var query = new GetDashboardDataQuery
        {
            DivSeq = divSeq
        };

        var result = await _mediator.Send(query);

        if (!result.Succeeded)
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Failed to get dashboard data",
                Detail = result.Errors.FirstOrDefault()
            });
        }

        return Ok(result.Data);
    }

    /// <summary>
    /// Gets issue data for dashboard.
    /// </summary>
    /// <param name="vendorType">Optional vendor type filter.</param>
    /// <param name="statTypeId">Optional stat type ID filter.</param>
    /// <param name="vendorId">Optional vendor ID filter.</param>
    /// <returns>List of issue data.</returns>
    [HttpGet("issues")]
    [ProducesResponseType(typeof(List<HomeIssueDataDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetIssues(
        [FromQuery] string? vendorType = null,
        [FromQuery] string? statTypeId = null,
        [FromQuery] string? vendorId = null)
    {
        var divSeq = User.FindFirstValue("div_seq") ?? "OPT001";

        var query = new GetHomeIssueDataQuery
        {
            DivSeq = divSeq,
            VendorType = vendorType,
            StatTypeId = statTypeId,
            VendorId = vendorId
        };

        var result = await _mediator.Send(query);

        if (!result.Succeeded)
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Failed to get issue data",
                Detail = result.Errors.FirstOrDefault()
            });
        }

        return Ok(result.Data);
    }

    /// <summary>
    /// Gets alarm data for dashboard (yearly and monthly).
    /// </summary>
    /// <param name="year">Year for alarm data (required).</param>
    /// <param name="vendorType">Optional vendor type filter.</param>
    /// <returns>Alarm data with yearly and monthly breakdown.</returns>
    [HttpGet("alarms")]
    [ProducesResponseType(typeof(HomeAlarmDataDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetAlarms(
        [FromQuery] string year,
        [FromQuery] string? vendorType = null)
    {
        if (string.IsNullOrWhiteSpace(year))
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Validation Error",
                Detail = "Year parameter is required."
            });
        }

        var divSeq = User.FindFirstValue("div_seq") ?? "OPT001";

        var query = new GetHomeAlarmDataQuery
        {
            DivSeq = divSeq,
            Year = year,
            VendorType = vendorType
        };

        var result = await _mediator.Send(query);

        if (!result.Succeeded)
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Failed to get alarm data",
                Detail = result.Errors.FirstOrDefault()
            });
        }

        return Ok(result.Data);
    }

    /// <summary>
    /// Gets yield report data.
    /// </summary>
    /// <param name="startDate">Start date filter (yyyy-MM-dd).</param>
    /// <param name="endDate">End date filter (yyyy-MM-dd).</param>
    /// <param name="vendorId">Optional vendor ID filter.</param>
    /// <param name="mtrlClassId">Optional material class ID filter.</param>
    /// <param name="specId">Optional spec ID filter.</param>
    /// <param name="groupBy">Group by option (day, week, month).</param>
    /// <returns>Yield report data.</returns>
    [HttpGet("yield")]
    [ProducesResponseType(typeof(YieldReportDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetYieldReport(
        [FromQuery] string? startDate = null,
        [FromQuery] string? endDate = null,
        [FromQuery] string? vendorId = null,
        [FromQuery] string? mtrlClassId = null,
        [FromQuery] string? specId = null,
        [FromQuery] string? groupBy = null)
    {
        var divSeq = User.FindFirstValue("div_seq") ?? "OPT001";

        var query = new GetYieldReportQuery
        {
            DivSeq = divSeq,
            StartDate = startDate,
            EndDate = endDate,
            VendorId = vendorId,
            MtrlClassId = mtrlClassId,
            SpecId = specId,
            GroupBy = groupBy
        };

        var result = await _mediator.Send(query);

        if (!result.Succeeded)
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Failed to get yield report",
                Detail = result.Errors.FirstOrDefault()
            });
        }

        return Ok(result.Data);
    }

    /// <summary>
    /// Gets statistics report data.
    /// </summary>
    /// <param name="startDate">Start date filter (yyyy-MM-dd).</param>
    /// <param name="endDate">End date filter (yyyy-MM-dd).</param>
    /// <param name="reportType">Report type filter.</param>
    /// <param name="categoryId">Optional category ID filter.</param>
    /// <param name="vendorId">Optional vendor ID filter.</param>
    /// <param name="groupBy">Group by option.</param>
    /// <returns>Statistics report data.</returns>
    [HttpGet("statistics")]
    [ProducesResponseType(typeof(StatisticsReportDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetStatisticsReport(
        [FromQuery] string? startDate = null,
        [FromQuery] string? endDate = null,
        [FromQuery] string? reportType = null,
        [FromQuery] string? categoryId = null,
        [FromQuery] string? vendorId = null,
        [FromQuery] string? groupBy = null)
    {
        var divSeq = User.FindFirstValue("div_seq") ?? "OPT001";

        var query = new GetStatisticsReportQuery
        {
            DivSeq = divSeq,
            StartDate = startDate,
            EndDate = endDate,
            ReportType = reportType,
            CategoryId = categoryId,
            VendorId = vendorId,
            GroupBy = groupBy
        };

        var result = await _mediator.Send(query);

        if (!result.Succeeded)
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Failed to get statistics report",
                Detail = result.Errors.FirstOrDefault()
            });
        }

        return Ok(result.Data);
    }
}
