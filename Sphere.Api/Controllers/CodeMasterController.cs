using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sphere.Application.DTOs.Master;
using Sphere.Application.Features.Master.Commands.CreateCodeMaster;
using Sphere.Application.Features.Master.Commands.DeleteCodeMaster;
using Sphere.Application.Features.Master.Commands.UpdateCodeMaster;
using Sphere.Application.Features.Master.Queries.GetCodeMaster;
using System.Security.Claims;

namespace Sphere.Api.Controllers;

/// <summary>
/// Code master controller for code management APIs.
/// </summary>
[ApiController]
[Route("api/v1/codes")]
[Authorize]
[Produces("application/json")]
public class CodeMasterController : ControllerBase
{
    private readonly ISender _mediator;
    private readonly ILogger<CodeMasterController> _logger;

    public CodeMasterController(ISender mediator, ILogger<CodeMasterController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Gets code master list with optional filters.
    /// </summary>
    /// <param name="codeClassId">Optional code class ID filter.</param>
    /// <param name="useYn">Optional use Y/N filter.</param>
    /// <param name="searchText">Optional search text filter.</param>
    /// <returns>List of code masters.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(CodeMasterListDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetCodeMasterList(
        [FromQuery] string? codeClassId = null,
        [FromQuery] string? useYn = null,
        [FromQuery] string? searchText = null)
    {
        var divSeq = User.FindFirstValue("div_seq") ?? "OPT001";

        var query = new GetCodeMasterQuery
        {
            DivSeq = divSeq,
            CodeClassId = codeClassId,
            UseYn = useYn,
            SearchText = searchText
        };

        var result = await _mediator.Send(query);

        if (!result.Succeeded)
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Failed to get code master list",
                Detail = result.Errors.FirstOrDefault()
            });
        }

        return Ok(result.Data);
    }

    /// <summary>
    /// Creates a new code master.
    /// </summary>
    /// <param name="request">Create code master request.</param>
    /// <returns>Created code master result.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(CodeMasterResultDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> CreateCodeMaster([FromBody] CreateCodeMasterDto request)
    {
        var divSeq = User.FindFirstValue("div_seq") ?? "OPT001";
        var userId = User.FindFirstValue("user_id") ?? string.Empty;

        var command = new CreateCodeMasterCommand
        {
            DivSeq = divSeq,
            UserId = userId,
            CodeId = request.CodeId,
            CodeClassId = request.CodeClassId,
            CodeAlias = request.CodeAlias,
            CodeNameK = request.CodeNameK,
            CodeNameE = request.CodeNameE,
            CodeNameC = request.CodeNameC,
            CodeNameV = request.CodeNameV,
            DisplaySeq = request.DisplaySeq,
            CodeOpt = request.CodeOpt,
            UseYn = request.UseYn,
            Description = request.Description
        };

        var result = await _mediator.Send(command);

        if (!result.Succeeded)
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Failed to create code master",
                Detail = result.Errors.FirstOrDefault()
            });
        }

        return CreatedAtAction(
            nameof(GetCodeMasterList),
            new { codeClassId = request.CodeClassId },
            result.Data);
    }

    /// <summary>
    /// Updates an existing code master.
    /// </summary>
    /// <param name="codeClassId">Code class ID.</param>
    /// <param name="codeId">Code ID.</param>
    /// <param name="request">Update code master request.</param>
    /// <returns>Updated code master result.</returns>
    [HttpPut("{codeClassId}/{codeId}")]
    [ProducesResponseType(typeof(CodeMasterResultDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateCodeMaster(
        string codeClassId,
        string codeId,
        [FromBody] UpdateCodeMasterDto request)
    {
        var divSeq = User.FindFirstValue("div_seq") ?? "OPT001";
        var userId = User.FindFirstValue("user_id") ?? string.Empty;

        var command = new UpdateCodeMasterCommand
        {
            DivSeq = divSeq,
            UserId = userId,
            CodeClassId = codeClassId,
            CodeId = codeId,
            CodeAlias = request.CodeAlias,
            CodeNameK = request.CodeNameK,
            CodeNameE = request.CodeNameE,
            CodeNameC = request.CodeNameC,
            CodeNameV = request.CodeNameV,
            DisplaySeq = request.DisplaySeq,
            CodeOpt = request.CodeOpt,
            UseYn = request.UseYn,
            Description = request.Description
        };

        var result = await _mediator.Send(command);

        if (!result.Succeeded)
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Failed to update code master",
                Detail = result.Errors.FirstOrDefault()
            });
        }

        return Ok(result.Data);
    }

    /// <summary>
    /// Deletes a code master.
    /// </summary>
    /// <param name="codeClassId">Code class ID.</param>
    /// <param name="codeId">Code ID.</param>
    /// <returns>Delete result.</returns>
    [HttpDelete("{codeClassId}/{codeId}")]
    [ProducesResponseType(typeof(CodeMasterResultDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCodeMaster(string codeClassId, string codeId)
    {
        var divSeq = User.FindFirstValue("div_seq") ?? "OPT001";
        var userId = User.FindFirstValue("user_id") ?? string.Empty;

        var command = new DeleteCodeMasterCommand
        {
            DivSeq = divSeq,
            UserId = userId,
            CodeClassId = codeClassId,
            CodeId = codeId
        };

        var result = await _mediator.Send(command);

        if (!result.Succeeded)
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Failed to delete code master",
                Detail = result.Errors.FirstOrDefault()
            });
        }

        return Ok(result.Data);
    }
}
