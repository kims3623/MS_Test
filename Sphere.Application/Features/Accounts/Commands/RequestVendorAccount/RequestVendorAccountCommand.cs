using MediatR;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Account;

namespace Sphere.Application.Features.Accounts.Commands.RequestVendorAccount;

/// <summary>
/// Command to request a new vendor account.
/// Creates an account request for approval workflow.
/// </summary>
public record RequestVendorAccountCommand : IRequest<Result<VendorAccountRequestResultDto>>
{
    /// <summary>
    /// Division sequence.
    /// </summary>
    public string DivSeq { get; init; } = string.Empty;

    /// <summary>
    /// Vendor ID (if existing vendor).
    /// </summary>
    public string? VendorId { get; init; }

    /// <summary>
    /// Vendor/Company name.
    /// </summary>
    public string VendorName { get; init; } = string.Empty;

    /// <summary>
    /// Contact person name.
    /// </summary>
    public string ContactPerson { get; init; } = string.Empty;

    /// <summary>
    /// Contact email address.
    /// </summary>
    public string ContactEmail { get; init; } = string.Empty;

    /// <summary>
    /// Contact phone number.
    /// </summary>
    public string? ContactPhone { get; init; }

    /// <summary>
    /// Reason for account request.
    /// </summary>
    public string RequestReason { get; init; } = string.Empty;

    /// <summary>
    /// Additional information or notes.
    /// </summary>
    public string? AdditionalInfo { get; init; }

    /// <summary>
    /// IP address of the requester.
    /// </summary>
    public string? IpAddress { get; init; }
}
