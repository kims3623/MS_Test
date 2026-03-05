using MediatR;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Approval;

namespace Sphere.Application.Features.Approvals.Queries.GetApprovalDetail;

/// <summary>
/// Query to get approval detail by ID.
/// </summary>
public record GetApprovalDetailQuery : IRequest<Result<ApprovalDetailDto>>
{
    public string DivSeq { get; init; } = string.Empty;
    public string AprovId { get; init; } = string.Empty;
}

/// <summary>
/// Query to get approval button state.
/// </summary>
public record GetApprovalButtonStateQuery : IRequest<Result<ApprovalDetailButtonDto>>
{
    public string DivSeq { get; init; } = string.Empty;
    public string AprovId { get; init; } = string.Empty;
    public string UserId { get; init; } = string.Empty;
}

/// <summary>
/// Query to get approval content (title and HTML contents).
/// </summary>
public record GetApprovalContentQuery : IRequest<Result<ApprovalDetailContentDto>>
{
    public string DivSeq { get; init; } = string.Empty;
    public string AprovId { get; init; } = string.Empty;
}
