using MediatR;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Approval;

namespace Sphere.Application.Features.Approvals.Commands.RejectRequest;

/// <summary>
/// Command to reject a pending approval request.
/// </summary>
public record RejectRequestCommand : IRequest<Result<ApprovalActionResponseDto>>
{
    public string DivSeq { get; init; } = string.Empty;
    public string AprovId { get; init; } = string.Empty;
    public string UserId { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
}
