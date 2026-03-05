using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sphere.Application.DTOs.Approval;
using Sphere.Application.Features.Approval.Commands.ApproveRequest;
using Sphere.Application.Features.Approval.Commands.RejectRequest;
using Sphere.Application.Features.Approvals.Queries.GetApprovalList;
using Sphere.Application.Features.Approvals.Queries.GetApprovalDetail;
// Alias for old GetApprovalContentQuery to avoid ambiguity
using OldGetApprovalContentQuery = Sphere.Application.Features.Approval.Queries.GetApprovalContent.GetApprovalContentQuery;
using Sphere.Application.Features.Approvals.Commands.CancelApproval;
using Sphere.Application.Features.Approvals.Queries.GetApprovalDetailList;
using Sphere.Application.Features.Approvals.Commands.BatchApprove;
using System.Security.Claims;

namespace Sphere.Api.Controllers;

/// <summary>
/// Controller for approval workflow operations.
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
[Authorize]
public class ApprovalController : ControllerBase
{
    private readonly ISender _mediator;
    private readonly ILogger<ApprovalController> _logger;

    public ApprovalController(ISender mediator, ILogger<ApprovalController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    #region B6 New Endpoints

    /// <summary>
    /// Gets approval list with filter and pagination.
    /// </summary>
    /// <param name="aprovState">Approval state filter.</param>
    /// <param name="chgTypeId">Change type ID filter.</param>
    /// <param name="startDate">Start date filter (yyyy-MM-dd).</param>
    /// <param name="endDate">End date filter (yyyy-MM-dd).</param>
    /// <param name="page">Page number (default: 1).</param>
    /// <param name="pageSize">Page size (default: 20).</param>
    /// <returns>Paginated approval list.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ApprovalListResponseDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetList(
        [FromQuery] string? aprovState = null,
        [FromQuery] string? chgTypeId = null,
        [FromQuery] string? startDate = null,
        [FromQuery] string? endDate = null,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        var divSeq = User.FindFirstValue("div_seq") ?? "OPT001";
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;

        var query = new GetApprovalListQuery
        {
            DivSeq = divSeq,
            UserId = userId,
            AprovState = aprovState,
            ChgTypeId = chgTypeId,
            StartDate = startDate,
            EndDate = endDate,
            PageNumber = page,
            PageSize = pageSize
        };

        var result = await _mediator.Send(query);

        if (!result.Succeeded)
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Approval List Query Failed",
                Detail = result.Errors.FirstOrDefault()
            });
        }

        return Ok(result.Data);
    }

    /// <summary>
    /// Gets approval detail by ID.
    /// </summary>
    /// <param name="id">Approval ID.</param>
    /// <returns>Approval detail.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApprovalDetailDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetDetail(string id)
    {
        var divSeq = User.FindFirstValue("div_seq") ?? "OPT001";

        var query = new GetApprovalDetailQuery
        {
            DivSeq = divSeq,
            AprovId = id
        };

        var result = await _mediator.Send(query);

        if (!result.Succeeded)
        {
            return NotFound(new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Approval Not Found",
                Detail = result.Errors.FirstOrDefault()
            });
        }

        return Ok(result.Data);
    }

    /// <summary>
    /// Gets approval button state (can approve/reject/cancel).
    /// </summary>
    /// <param name="id">Approval ID.</param>
    /// <returns>Button state flags.</returns>
    [HttpGet("{id}/button-state")]
    [ProducesResponseType(typeof(ApprovalDetailButtonDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetButtonState(string id)
    {
        var divSeq = User.FindFirstValue("div_seq") ?? "OPT001";
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;

        var query = new GetApprovalButtonStateQuery
        {
            DivSeq = divSeq,
            AprovId = id,
            UserId = userId
        };

        var result = await _mediator.Send(query);

        if (!result.Succeeded)
        {
            return Ok(new ApprovalDetailButtonDto());
        }

        return Ok(result.Data);
    }

    /// <summary>
    /// Cancels a pending approval request (by writer only).
    /// </summary>
    /// <param name="id">Approval ID.</param>
    /// <param name="request">Cancel request body.</param>
    /// <returns>No content on success.</returns>
    [HttpPost("{id}/cancel")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Cancel(string id, [FromBody] CancelRequestBody? request = null)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
        var divSeq = User.FindFirstValue("div_seq") ?? "OPT001";

        var command = new CancelApprovalCommand
        {
            AprovId = id,
            DivSeq = divSeq,
            UserId = userId,
            Reason = request?.Reason
        };

        var result = await _mediator.Send(command);

        if (!result.Succeeded)
        {
            var errorDetail = result.Errors.FirstOrDefault();
            if (errorDetail?.Contains("권한") == true)
            {
                return StatusCode(StatusCodes.Status403Forbidden, new ProblemDetails
                {
                    Status = StatusCodes.Status403Forbidden,
                    Title = "Permission Denied",
                    Detail = errorDetail
                });
            }

            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Cancel Failed",
                Detail = errorDetail
            });
        }

        return NoContent();
    }

    #endregion

    /// <summary>
    /// Gets approval content detail.
    /// </summary>
    /// <param name="id">Approval ID.</param>
    /// <param name="divSeq">Division sequence.</param>
    /// <param name="chgTypeId">Change type ID.</param>
    /// <returns>Approval content detail.</returns>
    [HttpGet("{id}/content")]
    [ProducesResponseType(typeof(ApprovalContentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetContent(
        string id,
        [FromQuery] string? divSeq = null,
        [FromQuery] string? chgTypeId = null)
    {
        var userDivSeq = divSeq ?? User.FindFirstValue("div_seq") ?? "OPT001";

        var query = new OldGetApprovalContentQuery
        {
            AprovId = id,
            DivSeq = userDivSeq,
            ChgTypeId = chgTypeId ?? string.Empty
        };

        var result = await _mediator.Send(query);

        if (!result.Succeeded)
        {
            var errorDetail = result.Errors.FirstOrDefault();
            return NotFound(new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Approval Not Found",
                Detail = errorDetail
            });
        }

        return Ok(result.Data);
    }

    /// <summary>
    /// Approves a request.
    /// </summary>
    /// <param name="id">Approval ID.</param>
    /// <param name="request">Approval request body.</param>
    /// <returns>No content on success.</returns>
    [HttpPost("{id}/approve")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Approve(string id, [FromBody] ApproveRequestBody? request = null)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
        var divSeq = User.FindFirstValue("div_seq") ?? "OPT001";

        var command = new ApproveRequestCommand
        {
            AprovId = id,
            DivSeq = divSeq,
            UserId = userId,
            Comment = request?.Comment
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
                    Title = "Approval Not Found",
                    Detail = errorDetail
                });
            }

            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Approval Failed",
                Detail = errorDetail
            });
        }

        return NoContent();
    }

    /// <summary>
    /// Rejects a request.
    /// </summary>
    /// <param name="id">Approval ID.</param>
    /// <param name="request">Rejection request body.</param>
    /// <returns>No content on success.</returns>
    [HttpPost("{id}/reject")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Reject(string id, [FromBody] RejectRequestBody request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
        var divSeq = User.FindFirstValue("div_seq") ?? "OPT001";

        var command = new RejectRequestCommand
        {
            AprovId = id,
            DivSeq = divSeq,
            UserId = userId,
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
                    Title = "Approval Not Found",
                    Detail = errorDetail
                });
            }

            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Rejection Failed",
                Detail = errorDetail
            });
        }

        return NoContent();
    }

    /// <summary>
    /// Gets approval detail list (history/steps) for an approval.
    /// </summary>
    [HttpGet("{id}/details")]
    [ProducesResponseType(typeof(ApprovalDetailListResponseDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetDetailList(string id)
    {
        var divSeq = User.FindFirstValue("div_seq") ?? "OPT001";
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;

        var query = new GetApprovalDetailListQuery
        {
            DivSeq = divSeq,
            AprovId = id,
            UserId = userId
        };

        var result = await _mediator.Send(query);
        if (!result.Succeeded)
            return BadRequest(new ProblemDetails { Status = 400, Title = "Query Failed", Detail = result.Errors.FirstOrDefault() });

        return Ok(result.Data);
    }

    /// <summary>
    /// Batch approve multiple requests.
    /// </summary>
    [HttpPost("batch-approve")]
    [ProducesResponseType(typeof(BatchApproveResponseDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> BatchApprove([FromBody] BatchApproveRequestBody request)
    {
        var divSeq = User.FindFirstValue("div_seq") ?? "OPT001";
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;

        var command = new BatchApproveCommand
        {
            DivSeq = divSeq,
            UserId = userId,
            AprovIds = request.AprovIds ?? new List<string>(),
            Comment = request.Comment
        };

        var result = await _mediator.Send(command);
        if (!result.Succeeded)
            return BadRequest(new ProblemDetails { Status = 400, Title = "Batch Approve Failed", Detail = result.Errors.FirstOrDefault() });

        return Ok(result.Data);
    }
}

/// <summary>
/// Request body for batch approval.
/// </summary>
public class BatchApproveRequestBody
{
    public List<string>? AprovIds { get; set; }
    public string? Comment { get; set; }
}

/// <summary>
/// Request body for approval action.
/// </summary>
public class ApproveRequestBody
{
    public string? Comment { get; set; }
}

/// <summary>
/// Request body for rejection action.
/// </summary>
public class RejectRequestBody
{
    public string Reason { get; set; } = string.Empty;
}

/// <summary>
/// Request body for cancellation action.
/// </summary>
public class CancelRequestBody
{
    public string? Reason { get; set; }
}
