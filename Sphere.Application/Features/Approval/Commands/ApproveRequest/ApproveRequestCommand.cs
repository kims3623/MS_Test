using MediatR;
using Sphere.Application.Common.Models;

namespace Sphere.Application.Features.Approval.Commands.ApproveRequest;

/// <summary>
/// Command for approving a request.
/// </summary>
public record ApproveRequestCommand : IRequest<Result>
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
    /// Optional comment for the approval.
    /// </summary>
    public string? Comment { get; init; }
}
