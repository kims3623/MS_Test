using MediatR;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Approval;

namespace Sphere.Application.Features.Approvals.Queries.GetApprovalList;

/// <summary>
/// Query to get approval list with filter and pagination.
/// </summary>
public record GetApprovalListQuery : IRequest<Result<ApprovalListResponseDto>>
{
    public string DivSeq { get; init; } = string.Empty;
    public string? UserId { get; init; }
    public string? AprovState { get; init; }
    public string? ChgTypeId { get; init; }
    public string? StartDate { get; init; }
    public string? EndDate { get; init; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 20;
}
