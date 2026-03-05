using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sphere.Application.DTOs.SPC;
using Sphere.Application.Features.SPC.Commands.CalculateStatistics;
using Sphere.Application.Features.SPC.Commands.CheckRunRule;
using Sphere.Application.Features.SPC.Commands.ExportChartData;
using Sphere.Application.Features.SPC.Commands.SaveChartConfig;
using Sphere.Application.Features.SPC.Commands.UpdateControlLimits;
using Sphere.Application.Features.SPC.Queries.GetChartData;
using Sphere.Application.Features.SPC.Queries.GetControlLimits;
using Sphere.Application.Features.SPC.Queries.GetCpkData;
using Sphere.Application.Features.SPC.Queries.GetDayAnalysis;
using Sphere.Application.Features.SPC.Queries.GetHistogramData;
using Sphere.Application.Features.SPC.Queries.GetMonthAnalysis;
using Sphere.Application.Features.SPC.Queries.GetPChart;
using Sphere.Application.Features.SPC.Queries.GetRunRules;
using Sphere.Application.Features.SPC.Queries.GetTrendAnalysis;
using Sphere.Application.Features.SPC.Queries.GetXBarRChart;
using Sphere.Application.Features.SPC.Queries.GetParetoData;
using Sphere.Application.Features.SPC.Queries.GetYieldSpecData;
using Sphere.Application.Features.SPC.Queries.GetChartConfig;
using System.Security.Claims;

namespace Sphere.Api.Controllers;

/// <summary>
/// SPC (Statistical Process Control) controller for chart and analysis APIs.
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Authorize]
[Produces("application/json")]
public class SPCController : ControllerBase
{
    private readonly ISender _mediator;
    private readonly ILogger<SPCController> _logger;

    public SPCController(ISender mediator, ILogger<SPCController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    #region Chart Data

    /// <summary>
    /// Gets basic SPC chart data.
    /// </summary>
    [HttpGet("charts/{specId}")]
    [ProducesResponseType(typeof(IEnumerable<ChartDataResultDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetChartData(
        string specId,
        [FromQuery] string? startDate = null,
        [FromQuery] string? endDate = null,
        [FromQuery] string? shift = null,
        [FromQuery] string chartType = "xbar-r")
    {
        var divSeq = User.FindFirstValue("div_seq") ?? "OPT001";

        var query = new GetChartDataQuery
        {
            DivSeq = divSeq,
            SpecSysId = specId,
            StartDate = startDate,
            EndDate = endDate,
            Shift = shift,
            ChartType = chartType
        };

        var result = await _mediator.Send(query);

        if (!result.Succeeded)
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Failed to get chart data",
                Detail = result.Errors.FirstOrDefault()
            });
        }

        return Ok(result.Data);
    }

    /// <summary>
    /// Gets X-Bar R chart data.
    /// </summary>
    [HttpGet("charts/xbar-r/{specId}")]
    [ProducesResponseType(typeof(XBarRChartDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetXBarRChart(
        string specId,
        [FromQuery] string? startDate = null,
        [FromQuery] string? endDate = null,
        [FromQuery] string? shift = null)
    {
        var divSeq = User.FindFirstValue("div_seq") ?? "OPT001";

        var query = new GetXBarRChartQuery
        {
            DivSeq = divSeq,
            SpecSysId = specId,
            StartDate = startDate,
            EndDate = endDate,
            Shift = shift
        };

        var result = await _mediator.Send(query);

        if (!result.Succeeded)
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Failed to get X-Bar R chart",
                Detail = result.Errors.FirstOrDefault()
            });
        }

        return Ok(result.Data);
    }

    /// <summary>
    /// Gets P chart data.
    /// </summary>
    [HttpGet("charts/p/{specId}")]
    [ProducesResponseType(typeof(PChartDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetPChart(
        string specId,
        [FromQuery] string? startDate = null,
        [FromQuery] string? endDate = null,
        [FromQuery] string? shift = null)
    {
        var divSeq = User.FindFirstValue("div_seq") ?? "OPT001";

        var query = new GetPChartQuery
        {
            DivSeq = divSeq,
            SpecSysId = specId,
            StartDate = startDate,
            EndDate = endDate,
            Shift = shift
        };

        var result = await _mediator.Send(query);

        if (!result.Succeeded)
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Failed to get P chart",
                Detail = result.Errors.FirstOrDefault()
            });
        }

        return Ok(result.Data);
    }

    /// <summary>
    /// Gets histogram data.
    /// </summary>
    [HttpGet("charts/histogram/{specId}")]
    [ProducesResponseType(typeof(HistogramDataDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetHistogramData(
        string specId,
        [FromQuery] string? startDate = null,
        [FromQuery] string? endDate = null,
        [FromQuery] string? shift = null,
        [FromQuery] int? binCount = null,
        [FromQuery] bool includeNormalCurve = true)
    {
        var divSeq = User.FindFirstValue("div_seq") ?? "OPT001";

        var query = new GetHistogramDataQuery
        {
            DivSeq = divSeq,
            SpecSysId = specId,
            StartDate = startDate,
            EndDate = endDate,
            Shift = shift,
            BinCount = binCount,
            IncludeNormalCurve = includeNormalCurve
        };

        var result = await _mediator.Send(query);

        if (!result.Succeeded)
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Failed to get histogram data",
                Detail = result.Errors.FirstOrDefault()
            });
        }

        return Ok(result.Data);
    }

    /// <summary>
    /// Gets Pareto diagram data.
    /// </summary>
    [HttpGet("charts/pareto/{specId}")]
    [ProducesResponseType(typeof(ParetoDataDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetParetoData(
        string specId,
        [FromQuery] string? startDate = null,
        [FromQuery] string? endDate = null,
        [FromQuery] string? shift = null,
        [FromQuery] string analysisType = "defect",
        [FromQuery] int? topN = null)
    {
        var divSeq = User.FindFirstValue("div_seq") ?? "OPT001";

        var query = new GetParetoDataQuery
        {
            DivSeq = divSeq,
            SpecSysId = specId,
            StartDate = startDate,
            EndDate = endDate,
            Shift = shift,
            AnalysisType = analysisType,
            TopN = topN
        };

        var result = await _mediator.Send(query);

        if (!result.Succeeded)
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Failed to get Pareto data",
                Detail = result.Errors.FirstOrDefault()
            });
        }

        return Ok(result.Data);
    }

    #endregion

    #region Cpk Analysis

    /// <summary>
    /// Gets Cpk analysis data.
    /// </summary>
    [HttpGet("cpk/{specId}")]
    [ProducesResponseType(typeof(CpkAnalysisDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetCpkData(
        string specId,
        [FromQuery] string? startDate = null,
        [FromQuery] string? endDate = null,
        [FromQuery] string? shift = null,
        [FromQuery] int? histogramBins = null,
        [FromQuery] bool includeTrend = true)
    {
        var divSeq = User.FindFirstValue("div_seq") ?? "OPT001";

        var query = new GetCpkDataQuery
        {
            DivSeq = divSeq,
            SpecSysId = specId,
            StartDate = startDate,
            EndDate = endDate,
            Shift = shift,
            HistogramBins = histogramBins,
            IncludeTrend = includeTrend
        };

        var result = await _mediator.Send(query);

        if (!result.Succeeded)
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Failed to get Cpk data",
                Detail = result.Errors.FirstOrDefault()
            });
        }

        return Ok(result.Data);
    }

    /// <summary>
    /// Calculates statistics for SPC data.
    /// </summary>
    [HttpPost("statistics/calculate")]
    [ProducesResponseType(typeof(StatisticsCalcResultDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CalculateStatistics([FromBody] CalculateStatisticsRequest request)
    {
        var divSeq = User.FindFirstValue("div_seq") ?? "OPT001";

        var command = new CalculateStatisticsCommand
        {
            DivSeq = divSeq,
            SpecSysId = request.SpecSysId,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            Shift = request.Shift,
            StatType = request.StatType
        };

        var result = await _mediator.Send(command);

        if (!result.Succeeded)
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Failed to calculate statistics",
                Detail = result.Errors.FirstOrDefault()
            });
        }

        return Ok(result.Data);
    }

    #endregion

    #region Control Limits

    /// <summary>
    /// Gets control limits for a spec.
    /// </summary>
    [HttpGet("limits/{specId}")]
    [ProducesResponseType(typeof(ControlLimitsResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetControlLimits(
        string specId,
        [FromQuery] string chartType = "xbar-r")
    {
        var query = new GetControlLimitsQuery
        {
            SpecSysId = specId,
            ChartType = chartType
        };

        var result = await _mediator.Send(query);

        if (!result.Succeeded)
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Failed to get control limits",
                Detail = result.Errors.FirstOrDefault()
            });
        }

        return Ok(result.Data);
    }

    /// <summary>
    /// Updates control limits for a spec.
    /// </summary>
    [HttpPut("limits/{specId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateControlLimits(
        string specId,
        [FromBody] UpdateControlLimitsRequest request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;

        var command = new UpdateControlLimitsCommand
        {
            UserId = userId,
            SpecSysId = specId,
            ChartType = request.ChartType,
            Ucl = request.Ucl,
            Cl = request.Cl,
            Lcl = request.Lcl,
            Usl = request.Usl,
            Lsl = request.Lsl,
            Target = request.Target,
            Reason = request.Reason
        };

        var result = await _mediator.Send(command);

        if (!result.Succeeded)
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Failed to update control limits",
                Detail = result.Errors.FirstOrDefault()
            });
        }

        return Ok(new { success = true });
    }

    #endregion

    #region Run Rules

    /// <summary>
    /// Gets run rules configuration for a spec.
    /// </summary>
    [HttpGet("runrules/{specId}")]
    [ProducesResponseType(typeof(RunRulesConfigDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetRunRules(string specId)
    {
        var query = new GetRunRulesQuery { SpecSysId = specId };

        var result = await _mediator.Send(query);

        if (!result.Succeeded)
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Failed to get run rules",
                Detail = result.Errors.FirstOrDefault()
            });
        }

        return Ok(result.Data);
    }

    /// <summary>
    /// Checks run rule violations.
    /// </summary>
    [HttpPost("runrules/check")]
    [ProducesResponseType(typeof(RunRuleCheckResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CheckRunRules([FromBody] CheckRunRulesRequest request)
    {
        var divSeq = User.FindFirstValue("div_seq") ?? "OPT001";

        var command = new CheckRunRuleCommand
        {
            DivSeq = divSeq,
            SpecSysId = request.SpecSysId,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            Shift = request.Shift,
            RuleIds = request.RuleIds
        };

        var result = await _mediator.Send(command);

        if (!result.Succeeded)
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Failed to check run rules",
                Detail = result.Errors.FirstOrDefault()
            });
        }

        return Ok(result.Data);
    }

    #endregion

    #region Analysis

    /// <summary>
    /// Gets day analysis data.
    /// </summary>
    [HttpGet("analysis/day/{specId}")]
    [ProducesResponseType(typeof(DayAnalysisDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetDayAnalysis(
        string specId,
        [FromQuery] string? startDate = null,
        [FromQuery] string? endDate = null,
        [FromQuery] string? shift = null,
        [FromQuery] bool includeShiftBreakdown = false)
    {
        var divSeq = User.FindFirstValue("div_seq") ?? "OPT001";

        var query = new GetDayAnalysisQuery
        {
            DivSeq = divSeq,
            SpecSysId = specId,
            StartDate = startDate,
            EndDate = endDate,
            Shift = shift,
            IncludeShiftBreakdown = includeShiftBreakdown
        };

        var result = await _mediator.Send(query);

        if (!result.Succeeded)
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Failed to get day analysis",
                Detail = result.Errors.FirstOrDefault()
            });
        }

        return Ok(result.Data);
    }

    /// <summary>
    /// Gets month analysis data.
    /// </summary>
    [HttpGet("analysis/month/{specId}")]
    [ProducesResponseType(typeof(MonthAnalysisDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetMonthAnalysis(
        string specId,
        [FromQuery] string? year = null,
        [FromQuery] bool includeYearComparison = false)
    {
        var divSeq = User.FindFirstValue("div_seq") ?? "OPT001";

        var query = new GetMonthAnalysisQuery
        {
            DivSeq = divSeq,
            SpecSysId = specId,
            Year = year,
            IncludeYearComparison = includeYearComparison
        };

        var result = await _mediator.Send(query);

        if (!result.Succeeded)
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Failed to get month analysis",
                Detail = result.Errors.FirstOrDefault()
            });
        }

        return Ok(result.Data);
    }

    /// <summary>
    /// Gets trend analysis data.
    /// </summary>
    [HttpGet("analysis/trend/{specId}")]
    [ProducesResponseType(typeof(TrendAnalysisDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetTrendAnalysis(
        string specId,
        [FromQuery] string? startDate = null,
        [FromQuery] string? endDate = null,
        [FromQuery] string groupBy = "day",
        [FromQuery] int movingAvgWindow = 7,
        [FromQuery] bool includeForecast = false,
        [FromQuery] int forecastPeriods = 7)
    {
        var divSeq = User.FindFirstValue("div_seq") ?? "OPT001";

        var query = new GetTrendAnalysisQuery
        {
            DivSeq = divSeq,
            SpecSysId = specId,
            StartDate = startDate,
            EndDate = endDate,
            GroupBy = groupBy,
            MovingAvgWindow = movingAvgWindow,
            IncludeForecast = includeForecast,
            ForecastPeriods = forecastPeriods
        };

        var result = await _mediator.Send(query);

        if (!result.Succeeded)
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Failed to get trend analysis",
                Detail = result.Errors.FirstOrDefault()
            });
        }

        return Ok(result.Data);
    }

    /// <summary>
    /// Gets yield spec data.
    /// </summary>
    [HttpGet("yield")]
    [ProducesResponseType(typeof(YieldSpecDataDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetYieldSpecData(
        [FromQuery] string? vendorId = null,
        [FromQuery] string? mtrlClassId = null,
        [FromQuery] string? startDate = null,
        [FromQuery] string? endDate = null)
    {
        var divSeq = User.FindFirstValue("div_seq") ?? "OPT001";

        var query = new GetYieldSpecDataQuery
        {
            DivSeq = divSeq,
            VendorId = vendorId,
            MtrlClassId = mtrlClassId,
            StartDate = startDate,
            EndDate = endDate
        };

        var result = await _mediator.Send(query);

        if (!result.Succeeded)
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Failed to get yield spec data",
                Detail = result.Errors.FirstOrDefault()
            });
        }

        return Ok(result.Data);
    }

    #endregion

    #region Chart Configuration

    /// <summary>
    /// Gets chart configuration.
    /// </summary>
    [HttpGet("charts/config")]
    [ProducesResponseType(typeof(ChartConfigDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetChartConfig([FromQuery] string chartType = "xbar-r")
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;

        var query = new GetChartConfigQuery
        {
            UserId = userId,
            ChartType = chartType
        };

        var result = await _mediator.Send(query);

        if (!result.Succeeded)
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Failed to get chart configuration",
                Detail = result.Errors.FirstOrDefault()
            });
        }

        return Ok(result.Data);
    }

    /// <summary>
    /// Saves chart configuration.
    /// </summary>
    [HttpPost("charts/config")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SaveChartConfig([FromBody] SaveChartConfigRequest request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;

        var command = new SaveChartConfigCommand
        {
            UserId = userId,
            ConfigId = request.ConfigId,
            ChartType = request.ChartType,
            DisplaySettings = request.DisplaySettings,
            DataSettings = request.DataSettings,
            ExportSettings = request.ExportSettings,
            IsDefault = request.IsDefault
        };

        var result = await _mediator.Send(command);

        if (!result.Succeeded)
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Failed to save chart config",
                Detail = result.Errors.FirstOrDefault()
            });
        }

        return Ok(new { success = true });
    }

    #endregion

    #region Export

    /// <summary>
    /// Exports chart data.
    /// </summary>
    [HttpGet("charts/{specId}/export")]
    [ProducesResponseType(typeof(FileContentResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ExportChartData(
        string specId,
        [FromQuery] string chartType = "xbar-r",
        [FromQuery] string format = "xlsx",
        [FromQuery] string? startDate = null,
        [FromQuery] string? endDate = null,
        [FromQuery] string? shift = null,
        [FromQuery] bool includeRawData = true,
        [FromQuery] bool includeStatistics = true)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;

        var command = new ExportChartDataCommand
        {
            UserId = userId,
            SpecSysId = specId,
            ChartType = chartType,
            ExportFormat = format,
            StartDate = startDate,
            EndDate = endDate,
            Shift = shift,
            IncludeRawData = includeRawData,
            IncludeStatistics = includeStatistics
        };

        var result = await _mediator.Send(command);

        if (!result.Succeeded)
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Failed to export chart data",
                Detail = result.Errors.FirstOrDefault()
            });
        }

        return File(result.Data!.FileContent, result.Data.ContentType, result.Data.FileName);
    }

    #endregion
}

#region Request DTOs

public record CalculateStatisticsRequest
{
    public string SpecSysId { get; init; } = string.Empty;
    public string? StartDate { get; init; }
    public string? EndDate { get; init; }
    public string? Shift { get; init; }
    public string StatType { get; init; } = string.Empty;
}

public record UpdateControlLimitsRequest
{
    public string ChartType { get; init; } = string.Empty;
    public decimal Ucl { get; init; }
    public decimal Cl { get; init; }
    public decimal Lcl { get; init; }
    public decimal? Usl { get; init; }
    public decimal? Lsl { get; init; }
    public decimal? Target { get; init; }
    public string? Reason { get; init; }
}

public record CheckRunRulesRequest
{
    public string SpecSysId { get; init; } = string.Empty;
    public string? StartDate { get; init; }
    public string? EndDate { get; init; }
    public string? Shift { get; init; }
    public List<string>? RuleIds { get; init; }
}

public record SaveChartConfigRequest
{
    public string? ConfigId { get; init; }
    public string ChartType { get; init; } = string.Empty;
    public ChartDisplaySettingsDto? DisplaySettings { get; init; }
    public ChartDataSettingsDto? DataSettings { get; init; }
    public ChartExportSettingsDto? ExportSettings { get; init; }
    public bool IsDefault { get; init; }
}

#endregion
