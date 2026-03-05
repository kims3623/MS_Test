using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sphere.Application.DTOs.Master;
using Sphere.Application.Features.Master.Commands.CreateSpecMaster;
using Sphere.Application.Features.Master.Commands.UpdateSpecMaster;
using Sphere.Application.Features.Master.Queries.GetSpecMaster;
using Sphere.Application.Features.Master.Queries.GetSpecDetail;
using Sphere.Application.Features.Master.Queries.GetCopyableSpecs;
using System.Security.Claims;

namespace Sphere.Api.Controllers;

/// <summary>
/// Spec master controller for specification management APIs.
/// </summary>
[ApiController]
[Route("api/v1/specs")]
[Authorize]
[Produces("application/json")]
public class SpecMasterController : ControllerBase
{
    private readonly ISender _mediator;
    private readonly ILogger<SpecMasterController> _logger;

    public SpecMasterController(ISender mediator, ILogger<SpecMasterController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Gets spec master list with optional filters.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(SpecMasterListDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetSpecMasterList(
        [FromQuery] string? vendorId = null,
        [FromQuery] string? mtrlClassId = null,
        [FromQuery] string? status = null,
        [FromQuery] string? useYn = null,
        [FromQuery] string? searchText = null)
    {
        var divSeq = User.FindFirstValue("div_seq") ?? "OPT001";
        _logger.LogWarning("[DIAG] DivSeq='{DivSeq}', Claims=[{Claims}]",
            divSeq, string.Join(", ", User.Claims.Select(c => $"{c.Type}={c.Value}")));
        var result = await _mediator.Send(new GetSpecMasterQuery
        {
            DivSeq = divSeq,
            VendorId = vendorId,
            MtrlClassId = mtrlClassId,
            Status = status,
            UseYn = useYn,
            SearchText = searchText
        });
        return result.Succeeded
            ? Ok(result.Data)
            : BadRequest(new ProblemDetails { Detail = result.Errors.FirstOrDefault() });
    }

    /// <summary>
    /// Gets spec detail by spec system ID.
    /// </summary>
    [HttpGet("{specSysId}/detail")]
    [ProducesResponseType(typeof(SpecDetailListDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetSpecDetail(string specSysId)
    {
        var divSeq = User.FindFirstValue("div_seq") ?? "OPT001";
        var result = await _mediator.Send(new GetSpecDetailQuery
        {
            DivSeq = divSeq,
            SpecSysId = specSysId
        });
        return result.Succeeded
            ? Ok(result.Data)
            : BadRequest(new ProblemDetails { Detail = result.Errors.FirstOrDefault() });
    }

    /// <summary>
    /// Creates a new spec master.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(SpecMasterResultDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateSpecMaster([FromBody] CreateSpecMasterDto request)
    {
        var divSeq = User.FindFirstValue("div_seq") ?? "OPT001";
        var userId = User.FindFirstValue("user_id") ?? string.Empty;

        var command = new CreateSpecMasterCommand
        {
            DivSeq = divSeq,
            UserId = userId,
            SpecId = request.SpecId,
            SpecName = request.SpecName,
            SpecVersion = request.SpecVersion,
            VendorId = request.VendorId,
            MtrlClassId = request.MtrlClassId,
            UseYn = request.UseYn,
            Description = request.Description
        };

        var result = await _mediator.Send(command);

        if (!result.Succeeded)
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Failed to create spec master",
                Detail = result.Errors.FirstOrDefault()
            });
        }

        return CreatedAtAction(nameof(GetSpecMasterList), null, result.Data);
    }

    /// <summary>
    /// Updates an existing spec master.
    /// </summary>
    [HttpPut("{specSysId}")]
    [ProducesResponseType(typeof(SpecMasterResultDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateSpecMaster(string specSysId, [FromBody] UpdateSpecMasterDto request)
    {
        var divSeq = User.FindFirstValue("div_seq") ?? "OPT001";
        var userId = User.FindFirstValue("user_id") ?? string.Empty;

        var command = new UpdateSpecMasterCommand
        {
            DivSeq = divSeq,
            UserId = userId,
            SpecSysId = specSysId,
            SpecName = request.SpecName,
            SpecVersion = request.SpecVersion,
            Status = request.Status,
            UseYn = request.UseYn,
            Description = request.Description
        };

        var result = await _mediator.Send(command);

        if (!result.Succeeded)
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Failed to update spec master",
                Detail = result.Errors.FirstOrDefault()
            });
        }

        return Ok(result.Data);
    }

    /// <summary>
    /// Get copyable specifications for spec copy popup.
    /// Returns active specifications filtered by vendor and material class.
    /// </summary>
    [HttpGet("copyable")]
    [ProducesResponseType(typeof(SpecMasterListDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetCopyableSpecs(
        [FromQuery] string? vendorId = null,
        [FromQuery] string? mtrlClassId = null,
        [FromQuery] string? searchText = null)
    {
        var divSeq = User.FindFirstValue("div_seq") ?? "OPT001";
        var result = await _mediator.Send(new GetCopyableSpecsQuery
        {
            DivSeq = divSeq,
            VendorId = vendorId,
            MtrlClassId = mtrlClassId,
            SearchText = searchText,
            ActiveOnly = true
        });
        return result.Succeeded
            ? Ok(result.Data)
            : BadRequest(new ProblemDetails { Detail = result.Errors.FirstOrDefault() });
    }
}
