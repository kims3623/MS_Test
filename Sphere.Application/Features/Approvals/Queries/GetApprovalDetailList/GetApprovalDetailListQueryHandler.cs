using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Approval;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Application.Features.Approvals.Queries.GetApprovalDetailList;

public class GetApprovalDetailListQueryHandler : IRequestHandler<GetApprovalDetailListQuery, Result<ApprovalDetailListResponseDto>>
{
    private readonly IApprovalRepository _approvalRepository;
    private readonly ILogger<GetApprovalDetailListQueryHandler> _logger;

    public GetApprovalDetailListQueryHandler(
        IApprovalRepository approvalRepository,
        ILogger<GetApprovalDetailListQueryHandler> logger)
    {
        _approvalRepository = approvalRepository;
        _logger = logger;
    }

    public async Task<Result<ApprovalDetailListResponseDto>> Handle(GetApprovalDetailListQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting approval detail list for {AprovId}", request.AprovId);

        var histories = await _approvalRepository.GetApprovalHistoriesAsync(
            request.DivSeq, request.AprovId, cancellationToken);

        var items = histories.Select(h => new ApprovalDetailListItemDto
        {
            AprovId = request.AprovId,
            Action = h.Action,
            ApproverId = h.UserId,
            ActionDate = h.ActionDate,
            Comment = h.Comment ?? string.Empty
        }).ToList();

        return Result<ApprovalDetailListResponseDto>.Success(new ApprovalDetailListResponseDto
        {
            AprovId = request.AprovId,
            Items = items,
            TotalCount = items.Count
        });
    }
}
