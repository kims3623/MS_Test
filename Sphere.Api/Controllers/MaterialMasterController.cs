using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sphere.Application.DTOs.Master;
using Sphere.Application.Features.Master.Commands.CreateMaterialMaster;
using Sphere.Application.Features.Master.Queries.GetMaterialMaster;
using System.Security.Claims;

namespace Sphere.Api.Controllers;

/// <summary>
/// Material master controller for material management APIs.
/// </summary>
[ApiController]
[Route("api/v1/materials")]
[Authorize]
[Produces("application/json")]
public class MaterialMasterController : ControllerBase
{
    private readonly ISender _mediator;
    private readonly ILogger<MaterialMasterController> _logger;

    public MaterialMasterController(ISender mediator, ILogger<MaterialMasterController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Gets material master list with optional filters.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(MaterialMasterListDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetMaterialMasterList(
        [FromQuery] string? mtrlClassId = null,
        [FromQuery] string? vendorId = null,
        [FromQuery] string? useYn = null)
    {
        var divSeq = User.FindFirstValue("div_seq") ?? "OPT001";

        var query = new GetMaterialMasterQuery
        {
            DivSeq = divSeq,
            MtrlClassId = mtrlClassId,
            VendorId = vendorId,
            UseYn = useYn
        };

        var result = await _mediator.Send(query);

        if (!result.Succeeded)
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Failed to get material master list",
                Detail = result.Errors.FirstOrDefault()
            });
        }

        return Ok(result.Data);
    }

    /// <summary>
    /// Creates a new material master.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(MaterialMasterResultDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateMaterialMaster([FromBody] CreateMaterialMasterDto request)
    {
        var divSeq = User.FindFirstValue("div_seq") ?? "OPT001";
        var userId = User.FindFirstValue("user_id") ?? string.Empty;

        var command = new CreateMaterialMasterCommand
        {
            DivSeq = divSeq,
            UserId = userId,
            MtrlId = request.MtrlId,
            MtrlName = request.MtrlName,
            MtrlClassId = request.MtrlClassId,
            MtrlClassGroupId = request.MtrlClassGroupId,
            VendorId = request.VendorId,
            Unit = request.Unit,
            SpecId = request.SpecId,
            UseYn = request.UseYn,
            Description = request.Description
        };

        var result = await _mediator.Send(command);

        if (!result.Succeeded)
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Failed to create material master",
                Detail = result.Errors.FirstOrDefault()
            });
        }

        return CreatedAtAction(nameof(GetMaterialMasterList), null, result.Data);
    }
}
