using MediatR;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Approval;

namespace Sphere.Application.Features.Approvals.Commands.CancelApproval;

/// <summary>
/// Command to cancel a pending approval request (by writer only).
/// </summary>
public record CancelApprovalCommand : IRequest<Result<ApprovalActionResponseDto>>
{
    public string DivSeq { get; init; } = string.Empty;
    public string AprovId { get; init; } = string.Empty;
    public string UserId { get; init; } = string.Empty;
    public string? Reason { get; init; }
}
