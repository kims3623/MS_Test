using MediatR;
using Sphere.Application.Common.Models;

namespace Sphere.Application.Features.Approval.Commands.RejectRequest;

/// <summary>
/// Command for rejecting a request.
/// </summary>
public record RejectRequestCommand : IRequest<Result>
{
    /// <summary>
    /// Approval ID.
    /// </summary>
    public string AprovId { get; init; } = string.Empty;

    /// <summary>
    /// Division sequence.
    /// </summary>
    public string DivSeq { get; init; } = string.Empty;

    /// <summary>
    /// User ID performing the action.
    /// </summary>
    public string UserId { get; init; } = string.Empty;

    /// <summary>
    /// Reason for rejection (required).
    /// </summary>
    public string Reason { get; init; } = string.Empty;
}
