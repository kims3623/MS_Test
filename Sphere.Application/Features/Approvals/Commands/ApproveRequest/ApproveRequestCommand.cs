using MediatR;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Approval;

namespace Sphere.Application.Features.Approvals.Commands.ApproveRequest;

/// <summary>
/// Command to approve a pending approval request.
/// </summary>
public record ApproveRequestCommand : IRequest<Result<ApprovalActionResponseDto>>
{
    public string DivSeq { get; init; } = string.Empty;
    public string AprovId { get; init; } = string.Empty;
    public string UserId { get; init; } = string.Empty;
    public string? Comment { get; init; }
}
