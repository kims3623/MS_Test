using MediatR;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Approval;

namespace Sphere.Application.Features.Approval.Queries.GetApprovalContent;

/// <summary>
/// Query to get approval content detail.
/// </summary>
public record GetApprovalContentQuery : IRequest<Result<ApprovalContentDto>>
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
    /// Change type ID.
    /// </summary>
    public string ChgTypeId { get; init; } = string.Empty;
}
