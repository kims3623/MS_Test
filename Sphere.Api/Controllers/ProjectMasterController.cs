using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sphere.Application.DTOs.Master;
using Sphere.Application.Features.Master.Queries.GetProjectMaster;
using System.Security.Claims;

namespace Sphere.Api.Controllers;

[ApiController]
[Route("api/v1/projects")]
[Authorize]
[Produces("application/json")]
public class ProjectMasterController : ControllerBase
{
    private readonly ISender _mediator;

    public ProjectMasterController(ISender mediator) => _mediator = mediator;

    [HttpGet]
    [ProducesResponseType(typeof(ProjectMasterListDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProjectMasterList(
        [FromQuery] string? customerId = null, [FromQuery] string? status = null,
        [FromQuery] string? useYn = null, [FromQuery] string? searchText = null)
    {
        var divSeq = User.FindFirstValue("div_seq") ?? "OPT001";
        var result = await _mediator.Send(new GetProjectMasterQuery
        {
            DivSeq = divSeq, CustomerId = customerId, Status = status, UseYn = useYn, SearchText = searchText
        });
        return result.Succeeded ? Ok(result.Data) : BadRequest(new ProblemDetails { Detail = result.Errors.FirstOrDefault() });
    }
}
