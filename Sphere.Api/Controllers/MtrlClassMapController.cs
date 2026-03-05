using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sphere.Application.DTOs.Master;
using Sphere.Application.Features.Master.Commands.CreateMtrlClassMap;
using Sphere.Application.Features.Master.Commands.UpdateMtrlClassMap;
using Sphere.Application.Features.Master.Queries.GetMtrlClassMapTree;
using System.Security.Claims;

namespace Sphere.Api.Controllers;

/// <summary>
/// MtrlClassMap controller for material class map tree management APIs (SPH1050).
/// </summary>
[ApiController]
[Route("api/v1/mtrl-class-map")]
[Authorize]
[Produces("application/json")]
public class MtrlClassMapController : ControllerBase
{
    private readonly ISender _mediator;
    private readonly ILogger<MtrlClassMapController> _logger;

    public MtrlClassMapController(ISender mediator, ILogger<MtrlClassMapController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Gets MtrlClassMap tree structure.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(List<MtrlClassMapTreeDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetTree()
    {
        var divSeq = User.FindFirstValue("div_seq") ?? "OPT001";

        var query = new GetMtrlClassMapTreeQuery
        {
            DivSeq = divSeq
        };

        var result = await _mediator.Send(query);

        if (!result.Succeeded)
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Failed to get MtrlClassMap tree",
                Detail = result.Errors.FirstOrDefault()
            });
        }

        return Ok(result.Data);
    }

    /// <summary>
    /// Creates a new class or subclass.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(MtrlClassMapResultDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateMtrlClassMapDto request)
    {
        var divSeq = User.FindFirstValue("div_seq") ?? "OPT001";
        var userId = User.FindFirstValue("user_id") ?? string.Empty;

        var command = new CreateMtrlClassMapCommand
        {
            DivSeq = divSeq,
            UserId = userId,
            ParentTreeId = request.ParentTreeId,
            MtrlClassId = request.MtrlClassId,
            ClassType = request.ClassType
        };

        var result = await _mediator.Send(command);

        if (!result.Succeeded)
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Failed to create MtrlClassMap",
                Detail = result.Errors.FirstOrDefault()
            });
        }

        return CreatedAtAction(nameof(GetTree), null, result.Data);
    }

    /// <summary>
    /// Updates an existing class (useYn, etc.).
    /// </summary>
    [HttpPut("{treeId}")]
    [ProducesResponseType(typeof(MtrlClassMapResultDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(string treeId, [FromBody] UpdateMtrlClassMapDto request)
    {
        if (request.TreeId != treeId)
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "TreeId mismatch",
                Detail = "The TreeId in the URL does not match the TreeId in the request body."
            });
        }

        var divSeq = User.FindFirstValue("div_seq") ?? "OPT001";
        var userId = User.FindFirstValue("user_id") ?? string.Empty;

        var command = new UpdateMtrlClassMapCommand
        {
            DivSeq = divSeq,
            UserId = userId,
            TreeId = request.TreeId,
            UseYn = request.UseYn
        };

        var result = await _mediator.Send(command);

        if (!result.Succeeded)
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Failed to update MtrlClassMap",
                Detail = result.Errors.FirstOrDefault()
            });
        }

        return Ok(result.Data);
    }
}
