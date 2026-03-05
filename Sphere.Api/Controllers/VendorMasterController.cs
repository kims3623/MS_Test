using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sphere.Application.DTOs.Master;
using Sphere.Application.Features.Master.Commands.CreateVendorMaster;
using Sphere.Application.Features.Master.Queries.GetVendorMaster;
using System.Security.Claims;

namespace Sphere.Api.Controllers;

/// <summary>
/// Vendor master controller for vendor management APIs.
/// </summary>
[ApiController]
[Route("api/v1/vendors")]
[Authorize]
[Produces("application/json")]
public class VendorMasterController : ControllerBase
{
    private readonly ISender _mediator;
    private readonly ILogger<VendorMasterController> _logger;

    public VendorMasterController(ISender mediator, ILogger<VendorMasterController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Gets vendor master list with optional filters.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(VendorMasterListDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetVendorMasterList(
        [FromQuery] string? vendorType = null,
        [FromQuery] string? useYn = null,
        [FromQuery] string? approvalStatus = null,
        [FromQuery] string? searchText = null)
    {
        var divSeq = User.FindFirstValue("div_seq") ?? "OPT001";

        var query = new GetVendorMasterQuery
        {
            DivSeq = divSeq,
            VendorType = vendorType,
            UseYn = useYn,
            ApprovalStatus = approvalStatus,
            SearchText = searchText
        };

        var result = await _mediator.Send(query);

        if (!result.Succeeded)
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Failed to get vendor master list",
                Detail = result.Errors.FirstOrDefault()
            });
        }

        return Ok(result.Data);
    }

    /// <summary>
    /// Creates a new vendor master.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(VendorMasterResultDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateVendorMaster([FromBody] CreateVendorMasterDto request)
    {
        var divSeq = User.FindFirstValue("div_seq") ?? "OPT001";
        var userId = User.FindFirstValue("user_id") ?? string.Empty;

        var command = new CreateVendorMasterCommand
        {
            DivSeq = divSeq,
            UserId = userId,
            VendorId = request.VendorId,
            VendorName = request.VendorName,
            VendorType = request.VendorType,
            VendorCode = request.VendorCode,
            ContactPerson = request.ContactPerson,
            ContactEmail = request.ContactEmail,
            ContactPhone = request.ContactPhone,
            Address = request.Address,
            Country = request.Country,
            UseYn = request.UseYn,
            Description = request.Description
        };

        var result = await _mediator.Send(command);

        if (!result.Succeeded)
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Failed to create vendor master",
                Detail = result.Errors.FirstOrDefault()
            });
        }

        return CreatedAtAction(nameof(GetVendorMasterList), null, result.Data);
    }
}
