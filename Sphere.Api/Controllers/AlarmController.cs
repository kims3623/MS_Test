using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sphere.Application.DTOs.Alarm;
using Sphere.Application.Features.Alarms.Commands.CloseAlarm;
using Sphere.Application.Features.Alarms.Commands.CreateAlarm;
using Sphere.Application.Features.Alarms.Commands.UpdateAlarm;
using Sphere.Application.Features.Alarms.Commands.ExecuteAlarmAction;
using Sphere.Application.Features.Alarms.Commands.UploadAlarmAttachment;
using Sphere.Application.Features.Alarms.Commands.SendAlarmNotification;
using Sphere.Application.Features.Alarms.Commands.AcknowledgeAlarm;
using Sphere.Application.Features.Alarms.Commands.EscalateAlarm;
using Sphere.Application.Features.Alarms.Queries.GetAlarmActions;
using Sphere.Application.Features.Alarms.Queries.GetAlarmList;
using Sphere.Application.Features.Alarms.Queries.GetAlarmDetail;
using Sphere.Application.Features.Alarms.Queries.GetAlarmHistory;
using Sphere.Application.Features.Alarms.Queries.GetAlarmAttachments;
using Sphere.Application.Features.Alarms.Queries.GetAlarmStatistics;
using Sphere.Application.Features.Alarms.Queries.GetAlarmTrend;
using System.Security.Claims;

namespace Sphere.Api.Controllers;

/// <summary>
/// Controller for alarm management operations.
/// </summary>
[ApiController]
[Route("api/v1/[controller]s")]
[Produces("application/json")]
[Authorize]
public class AlarmController : ControllerBase
{
    private readonly ISender _mediator;
    private readonly ILogger<AlarmController> _logger;

    public AlarmController(ISender mediator, ILogger<AlarmController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    #region B6 New Endpoints

    /// <summary>
    /// Gets alarm list with filter and pagination.
    /// </summary>
    /// <param name="startDate">Start date filter (yyyy-MM-dd).</param>
    /// <param name="endDate">End date filter (yyyy-MM-dd).</param>
    /// <param name="vendorId">Vendor ID filter.</param>
    /// <param name="mtrlClassId">Material class ID filter.</param>
    /// <param name="almActionId">Alarm action ID filter.</param>
    /// <param name="almProcYn">Alarm process status filter (Y/N).</param>
    /// <param name="stopYn">Stop status filter (Y/N).</param>
    /// <param name="page">Page number (default: 1).</param>
    /// <param name="pageSize">Page size (default: 20).</param>
    /// <returns>Paginated alarm list.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(AlarmListResponseDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetList(
        [FromQuery] string? startDate = null,
        [FromQuery] string? endDate = null,
        [FromQuery] string? vendorId = null,
        [FromQuery] string? mtrlClassId = null,
        [FromQuery] string? almActionId = null,
        [FromQuery] string? almProcYn = null,
        [FromQuery] string? stopYn = null,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        var divSeq = User.FindFirstValue("div_seq") ?? "OPT001";
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;

        var query = new GetAlarmListQuery
        {
            DivSeq = divSeq,
            StartDate = startDate,
            EndDate = endDate,
            VendorId = vendorId,
            MtrlClassId = mtrlClassId,
            AlmActionId = almActionId,
            AlmProcYn = almProcYn,
            StopYn = stopYn,
            UserId = userId,
            PageNumber = page,
            PageSize = pageSize
        };

        var result = await _mediator.Send(query);

        if (!result.Succeeded)
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Alarm List Query Failed",
                Detail = result.Errors.FirstOrDefault()
            });
        }

        return Ok(result.Data);
    }

    /// <summary>
    /// Gets alarm detail by ID.
    /// </summary>
    /// <param name="id">Alarm system ID.</param>
    /// <returns>Alarm detail with actions, history count, and attachment count.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(AlarmDetailDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetDetail(string id)
    {
        var divSeq = User.FindFirstValue("div_seq") ?? "OPT001";

        var query = new GetAlarmDetailQuery
        {
            DivSeq = divSeq,
            AlmSysId = id
        };

        var result = await _mediator.Send(query);

        if (!result.Succeeded)
        {
            return NotFound(new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Alarm Not Found",
                Detail = result.Errors.FirstOrDefault()
            });
        }

        return Ok(result.Data);
    }

    /// <summary>
    /// Gets alarm history timeline.
    /// </summary>
    /// <param name="id">Alarm system ID.</param>
    /// <param name="page">Page number (default: 1).</param>
    /// <param name="pageSize">Page size (default: 50).</param>
    /// <returns>Alarm history items.</returns>
    [HttpGet("{id}/history")]
    [ProducesResponseType(typeof(AlarmHistoryResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetHistory(
        string id,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 50)
    {
        var divSeq = User.FindFirstValue("div_seq") ?? "OPT001";

        var query = new GetAlarmHistoryQuery
        {
            DivSeq = divSeq,
            AlmSysId = id,
            PageNumber = page,
            PageSize = pageSize
        };

        var result = await _mediator.Send(query);

        if (!result.Succeeded)
        {
            return NotFound(new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Alarm History Not Found",
                Detail = result.Errors.FirstOrDefault()
            });
        }

        return Ok(result.Data);
    }

    /// <summary>
    /// Gets alarm attachments.
    /// </summary>
    /// <param name="id">Alarm system ID.</param>
    /// <returns>List of attachments.</returns>
    [HttpGet("{id}/attachments")]
    [ProducesResponseType(typeof(AlarmAttachmentListResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAttachments(string id)
    {
        var divSeq = User.FindFirstValue("div_seq") ?? "OPT001";

        var query = new GetAlarmAttachmentsQuery
        {
            DivSeq = divSeq,
            AlmSysId = id
        };

        var result = await _mediator.Send(query);

        if (!result.Succeeded)
        {
            return NotFound(new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Alarm Attachments Not Found",
                Detail = result.Errors.FirstOrDefault()
            });
        }

        return Ok(result.Data);
    }

    /// <summary>
    /// Gets alarm statistics summary.
    /// </summary>
    /// <param name="startDate">Start date filter (yyyy-MM-dd).</param>
    /// <param name="endDate">End date filter (yyyy-MM-dd).</param>
    /// <returns>Alarm statistics.</returns>
    [HttpGet("statistics")]
    [ProducesResponseType(typeof(AlarmStatisticsDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetStatistics(
        [FromQuery] string? startDate = null,
        [FromQuery] string? endDate = null)
    {
        var divSeq = User.FindFirstValue("div_seq") ?? "OPT001";

        var query = new GetAlarmStatisticsQuery
        {
            DivSeq = divSeq,
            StartDate = startDate,
            EndDate = endDate
        };

        var result = await _mediator.Send(query);

        if (!result.Succeeded)
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Statistics Query Failed",
                Detail = result.Errors.FirstOrDefault()
            });
        }

        return Ok(result.Data);
    }

    /// <summary>
    /// Gets alarm trend data.
    /// </summary>
    /// <param name="startDate">Start date (yyyy-MM-dd).</param>
    /// <param name="endDate">End date (yyyy-MM-dd).</param>
    /// <param name="groupBy">Group by period (DAY, WEEK, MONTH).</param>
    /// <returns>Alarm trend data.</returns>
    [HttpGet("trend")]
    [ProducesResponseType(typeof(AlarmTrendResponseDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTrend(
        [FromQuery] string startDate,
        [FromQuery] string endDate,
        [FromQuery] string groupBy = "DAY")
    {
        var divSeq = User.FindFirstValue("div_seq") ?? "OPT001";

        var query = new GetAlarmTrendQuery
        {
            DivSeq = divSeq,
            StartDate = startDate,
            EndDate = endDate,
            GroupBy = groupBy
        };

        var result = await _mediator.Send(query);

        if (!result.Succeeded)
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Trend Query Failed",
                Detail = result.Errors.FirstOrDefault()
            });
        }

        return Ok(result.Data);
    }

    /// <summary>
    /// Creates a new alarm.
    /// </summary>
    /// <param name="request">Create alarm request body.</param>
    /// <returns>Created alarm info.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(CreateAlarmResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateAlarmRequestBody request)
    {
        var divSeq = User.FindFirstValue("div_seq") ?? "OPT001";
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;

        var command = new CreateAlarmCommand
        {
            DivSeq = divSeq,
            AlmProcId = request.AlmProcId,
            Title = request.Title,
            Contents = request.Contents,
            VendorId = request.VendorId,
            MtrlClassId = request.MtrlClassId,
            SpecSysId = request.SpecSysId,
            Severity = request.Severity,
            CreateUserId = userId
        };

        var result = await _mediator.Send(command);

        if (!result.Succeeded)
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Create Alarm Failed",
                Detail = result.Errors.FirstOrDefault()
            });
        }

        return CreatedAtAction(nameof(GetDetail), new { id = result.Data?.AlmSysId }, result.Data);
    }

    /// <summary>
    /// Updates an alarm.
    /// </summary>
    /// <param name="id">Alarm system ID.</param>
    /// <param name="request">Update alarm request body.</param>
    /// <returns>Updated alarm info.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(UpdateAlarmResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(string id, [FromBody] UpdateAlarmRequestBody request)
    {
        var divSeq = User.FindFirstValue("div_seq") ?? "OPT001";
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;

        var command = new UpdateAlarmCommand
        {
            AlmSysId = id,
            DivSeq = divSeq,
            Title = request.Title,
            Contents = request.Contents,
            AlmActionId = request.AlmActionId,
            Severity = request.Severity,
            UpdateUserId = userId
        };

        var result = await _mediator.Send(command);

        if (!result.Succeeded)
        {
            var errorDetail = result.Errors.FirstOrDefault();
            if (errorDetail?.Contains("not found") == true)
            {
                return NotFound(new ProblemDetails
                {
                    Status = StatusCodes.Status404NotFound,
                    Title = "Alarm Not Found",
                    Detail = errorDetail
                });
            }

            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Update Alarm Failed",
                Detail = errorDetail
            });
        }

        return Ok(result.Data);
    }

    /// <summary>
    /// Executes an alarm action.
    /// </summary>
    /// <param name="id">Alarm system ID.</param>
    /// <param name="request">Execute action request body.</param>
    /// <returns>Action execution result.</returns>
    [HttpPost("{id}/execute-action")]
    [ProducesResponseType(typeof(ExecuteAlarmActionResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ExecuteAction(string id, [FromBody] ExecuteAlarmActionRequestBody request)
    {
        var divSeq = User.FindFirstValue("div_seq") ?? "OPT001";
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;

        var command = new ExecuteAlarmActionCommand
        {
            DivSeq = divSeq,
            AlmSysId = id,
            AlmActionId = request.AlmActionId,
            Comment = request.Comment,
            UserId = userId,
            NotifyUserIds = request.NotifyUserIds
        };

        var result = await _mediator.Send(command);

        if (!result.Succeeded)
        {
            var errorDetail = result.Errors.FirstOrDefault();
            if (errorDetail?.Contains("not found") == true)
            {
                return NotFound(new ProblemDetails
                {
                    Status = StatusCodes.Status404NotFound,
                    Title = "Alarm Not Found",
                    Detail = errorDetail
                });
            }

            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Execute Action Failed",
                Detail = errorDetail
            });
        }

        return Ok(result.Data);
    }

    /// <summary>
    /// Uploads an attachment to an alarm.
    /// </summary>
    /// <param name="id">Alarm system ID.</param>
    /// <param name="file">File to upload.</param>
    /// <returns>Upload result.</returns>
    [HttpPost("{id}/attachments")]
    [ProducesResponseType(typeof(UploadAlarmAttachmentResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UploadAttachment(string id, IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Invalid File",
                Detail = "Please provide a valid file."
            });
        }

        var divSeq = User.FindFirstValue("div_seq") ?? "OPT001";
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;

        using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream);

        var command = new UploadAlarmAttachmentCommand
        {
            DivSeq = divSeq,
            AlmSysId = id,
            FileName = Path.GetFileName(file.FileName),
            OriginalFileName = file.FileName,
            FileSize = file.Length,
            FileType = Path.GetExtension(file.FileName),
            MimeType = file.ContentType,
            FileContent = memoryStream.ToArray(),
            CreateUserId = userId
        };

        var result = await _mediator.Send(command);

        if (!result.Succeeded)
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Upload Failed",
                Detail = result.Errors.FirstOrDefault()
            });
        }

        return Created($"/api/v1/alarms/{id}/attachments/{result.Data?.AttachSeq}", result.Data);
    }

    /// <summary>
    /// Sends alarm notification to users.
    /// </summary>
    /// <param name="id">Alarm system ID.</param>
    /// <param name="request">Notification request body.</param>
    /// <returns>Notification result.</returns>
    [HttpPost("{id}/notify")]
    [ProducesResponseType(typeof(SendAlarmNotificationResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SendNotification(string id, [FromBody] SendNotificationRequestBody request)
    {
        var divSeq = User.FindFirstValue("div_seq") ?? "OPT001";
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;

        var command = new SendAlarmNotificationCommand
        {
            DivSeq = divSeq,
            AlmSysId = id,
            RecipientUserIds = request.RecipientUserIds,
            Subject = request.Subject,
            Message = request.Message,
            SendEmail = request.SendEmail,
            SendPush = request.SendPush,
            SenderUserId = userId
        };

        var result = await _mediator.Send(command);

        if (!result.Succeeded)
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Send Notification Failed",
                Detail = result.Errors.FirstOrDefault()
            });
        }

        return Ok(result.Data);
    }

    /// <summary>
    /// Acknowledges an alarm (confirms receipt).
    /// </summary>
    /// <param name="id">Alarm system ID.</param>
    /// <param name="request">Acknowledge request body.</param>
    /// <returns>Acknowledge result.</returns>
    [HttpPost("{id}/acknowledge")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Acknowledge(string id, [FromBody] AcknowledgeAlarmRequestBody request)
    {
        var divSeq = User.FindFirstValue("div_seq") ?? "OPT001";
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;

        var command = new AcknowledgeAlarmCommand
        {
            DivSeq = divSeq,
            AlmSysId = id,
            UserId = userId,
            Comment = request.Comment
        };

        var result = await _mediator.Send(command);

        if (!result.Succeeded)
        {
            var errorDetail = result.Errors.FirstOrDefault();
            if (errorDetail?.Contains("not found") == true)
            {
                return NotFound(new ProblemDetails
                {
                    Status = StatusCodes.Status404NotFound,
                    Title = "Alarm Not Found",
                    Detail = errorDetail
                });
            }

            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Acknowledge Alarm Failed",
                Detail = errorDetail
            });
        }

        return Ok(new { message = "Alarm acknowledged successfully." });
    }

    /// <summary>
    /// Escalates an alarm to a supervisor.
    /// </summary>
    /// <param name="id">Alarm system ID.</param>
    /// <param name="request">Escalate request body.</param>
    /// <returns>Escalate result.</returns>
    [HttpPost("{id}/escalate")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Escalate(string id, [FromBody] EscalateAlarmRequestBody request)
    {
        var divSeq = User.FindFirstValue("div_seq") ?? "OPT001";
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;

        var command = new EscalateAlarmCommand
        {
            DivSeq = divSeq,
            AlmSysId = id,
            UserId = userId,
            EscalateTo = request.EscalateTo,
            Reason = request.Reason
        };

        var result = await _mediator.Send(command);

        if (!result.Succeeded)
        {
            var errorDetail = result.Errors.FirstOrDefault();
            if (errorDetail?.Contains("not found") == true)
            {
                return NotFound(new ProblemDetails
                {
                    Status = StatusCodes.Status404NotFound,
                    Title = "Alarm Not Found",
                    Detail = errorDetail
                });
            }

            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Escalate Alarm Failed",
                Detail = errorDetail
            });
        }

        return Ok(new { message = "Alarm escalated successfully." });
    }

    #endregion

    /// <summary>
    /// Gets alarm actions for a specific alarm.
    /// </summary>
    /// <param name="id">Alarm system ID.</param>
    /// <returns>List of available alarm actions.</returns>
    [HttpGet("{id}/actions")]
    [ProducesResponseType(typeof(List<AlarmActionDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetActions(string id)
    {
        var divSeq = User.FindFirstValue("div_seq") ?? "OPT001";

        var query = new GetAlarmActionsQuery
        {
            AlarmSysId = id,
            DivSeq = divSeq
        };

        var result = await _mediator.Send(query);

        if (!result.Succeeded)
        {
            return NotFound(new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Alarm Actions Not Found",
                Detail = result.Errors.FirstOrDefault()
            });
        }

        return Ok(result.Data);
    }

    /// <summary>
    /// Closes/stops an alarm.
    /// </summary>
    /// <param name="id">Alarm system ID.</param>
    /// <param name="request">Close alarm request body.</param>
    /// <returns>Close alarm result.</returns>
    [HttpPost("{id}/close")]
    [ProducesResponseType(typeof(CloseAlarmResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Close(string id, [FromBody] CloseAlarmRequestBody request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
        var divSeq = User.FindFirstValue("div_seq") ?? "OPT001";

        var command = new CloseAlarmCommand
        {
            AlarmSysId = id,
            DivSeq = divSeq,
            ActionId = request.ActionId,
            StopReason = request.StopReason,
            CustomerIds = request.CustomerIds,
            UserId = userId
        };

        var result = await _mediator.Send(command);

        if (!result.Succeeded)
        {
            var errorDetail = result.Errors.FirstOrDefault();
            if (errorDetail?.Contains("not found") == true)
            {
                return NotFound(new ProblemDetails
                {
                    Status = StatusCodes.Status404NotFound,
                    Title = "Alarm Not Found",
                    Detail = errorDetail
                });
            }

            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Close Alarm Failed",
                Detail = errorDetail
            });
        }

        return Ok(result.Data);
    }
}

/// <summary>
/// Request body for closing an alarm.
/// </summary>
public class CloseAlarmRequestBody
{
    /// <summary>
    /// Action ID for the close action.
    /// </summary>
    public string ActionId { get; set; } = string.Empty;

    /// <summary>
    /// Reason for stopping the alarm.
    /// </summary>
    public string StopReason { get; set; } = string.Empty;

    /// <summary>
    /// Customer IDs to notify (optional).
    /// </summary>
    public List<string>? CustomerIds { get; set; }
}

/// <summary>
/// Request body for creating an alarm.
/// </summary>
public class CreateAlarmRequestBody
{
    public string AlmProcId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Contents { get; set; } = string.Empty;
    public string VendorId { get; set; } = string.Empty;
    public string MtrlClassId { get; set; } = string.Empty;
    public string? SpecSysId { get; set; }
    public string Severity { get; set; } = "3";
}

/// <summary>
/// Request body for updating an alarm.
/// </summary>
public class UpdateAlarmRequestBody
{
    public string? Title { get; set; }
    public string? Contents { get; set; }
    public string? AlmActionId { get; set; }
    public string? Severity { get; set; }
}

/// <summary>
/// Request body for executing an alarm action.
/// </summary>
public class ExecuteAlarmActionRequestBody
{
    public string AlmActionId { get; set; } = string.Empty;
    public string? Comment { get; set; }
    public List<string>? NotifyUserIds { get; set; }
}

/// <summary>
/// Request body for sending alarm notification.
/// </summary>
public class SendNotificationRequestBody
{
    public List<string> RecipientUserIds { get; set; } = new();
    public string? Subject { get; set; }
    public string? Message { get; set; }
    public bool SendEmail { get; set; } = true;
    public bool SendPush { get; set; } = false;
}

/// <summary>
/// Request body for acknowledging an alarm.
/// </summary>
public class AcknowledgeAlarmRequestBody
{
    public string? Comment { get; set; }
}

/// <summary>
/// Request body for escalating an alarm.
/// </summary>
public class EscalateAlarmRequestBody
{
    public string? EscalateTo { get; set; }
    public string? Reason { get; set; }
}
