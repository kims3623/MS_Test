using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sphere.Application.DTOs.Account;
using Sphere.Application.Features.Accounts.Commands.RequestVendorAccount;

namespace Sphere.Api.Controllers;

/// <summary>
/// Controller for account-related operations.
/// </summary>
[ApiController]
[Route("api/v1/accounts")]
[Produces("application/json")]
public class AccountsController : ControllerBase
{
    private readonly ISender _mediator;
    private readonly ILogger<AccountsController> _logger;

    public AccountsController(ISender mediator, ILogger<AccountsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Request a new vendor account.
    /// This endpoint is public (no authorization required) for vendor registration.
    /// </summary>
    /// <param name="request">Vendor account request data.</param>
    /// <returns>Request result with request ID.</returns>
    [HttpPost("vendor-request")]
    [ProducesResponseType(typeof(VendorAccountRequestResultDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RequestVendorAccount([FromBody] VendorAccountRequestDto request)
    {
        _logger.LogInformation(
            "Received vendor account request for {VendorName}",
            request.VendorName);

        // Get client IP address
        var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();

        var command = new RequestVendorAccountCommand
        {
            DivSeq = "DEFAULT", // Default division for public requests
            VendorId = request.VendorId,
            VendorName = request.VendorName,
            ContactPerson = request.ContactPerson,
            ContactEmail = request.ContactEmail,
            ContactPhone = request.ContactPhone,
            RequestReason = request.RequestReason,
            AdditionalInfo = request.AdditionalInfo,
            IpAddress = ipAddress
        };

        var result = await _mediator.Send(command);

        if (!result.Succeeded)
        {
            return BadRequest(new ProblemDetails
            {
                Title = "Vendor Account Request Failed",
                Detail = result.Errors.FirstOrDefault(),
                Status = StatusCodes.Status400BadRequest
            });
        }

        return Ok(result.Data);
    }
}
