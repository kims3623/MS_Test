using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sphere.Application.DTOs.Auth;
using Sphere.Application.Features.Users.Queries.GetUserById;
using System.Security.Claims;

namespace Sphere.Api.Controllers;

/// <summary>
/// Controller for user management operations.
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
[Authorize]
public class UsersController : ControllerBase
{
    private readonly ISender _mediator;
    private readonly ILogger<UsersController> _logger;

    public UsersController(ISender mediator, ILogger<UsersController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Gets a user's profile by ID.
    /// </summary>
    /// <param name="id">User identifier.</param>
    /// <returns>User profile.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(UserProfileDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUserById(string id)
    {
        var divSeq = User.FindFirstValue("div_seq") ?? "OPT001";

        var query = new GetUserByIdQuery
        {
            UserId = id,
            DivSeq = divSeq
        };

        var result = await _mediator.Send(query);

        if (!result.Succeeded)
        {
            return NotFound(new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "User Not Found",
                Detail = result.Errors.FirstOrDefault()
            });
        }

        return Ok(result.Data);
    }
}
