using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sphere.Application.DTOs.Master;
using Sphere.Application.Features.Master.Queries.GetDefaultRules;
using System.Security.Claims;

namespace Sphere.Api.Controllers;

[ApiController]
[Route("api/v1/rules")]
[Authorize]
[Produces("application/json")]
public class DefaultRuleController : ControllerBase
{
    private readonly ISender _mediator;

    public DefaultRuleController(ISender mediator) => _mediator = mediator;

    [HttpGet]
    [ProducesResponseType(typeof(DefaultRuleListDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetDefaultRules(
        [FromQuery] string? ruleType = null, [FromQuery] string? targetType = null,
        [FromQuery] string? useYn = null, [FromQuery] string? searchText = null)
    {
        var divSeq = User.FindFirstValue("div_seq") ?? "OPT001";
        var result = await _mediator.Send(new GetDefaultRulesQuery
        {
            DivSeq = divSeq, RuleType = ruleType, TargetType = targetType, UseYn = useYn, SearchText = searchText
        });
        return result.Succeeded ? Ok(result.Data) : BadRequest(new ProblemDetails { Detail = result.Errors.FirstOrDefault() });
    }
}
