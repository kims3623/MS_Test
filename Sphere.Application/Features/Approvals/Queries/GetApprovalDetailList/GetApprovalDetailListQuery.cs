using MediatR;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Approval;

namespace Sphere.Application.Features.Approvals.Queries.GetApprovalDetailList;

/// <summary>
/// Query for getting approval detail list (batch detail retrieval).
/// </summary>
public record GetApprovalDetailListQuery : IRequest<Result<ApprovalDetailListResponseDto>>
{
    public string DivSeq { get; init; } = string.Empty;
    public string AprovId { get; init; } = string.Empty;
    public string UserId { get; init; } = string.Empty;
}
