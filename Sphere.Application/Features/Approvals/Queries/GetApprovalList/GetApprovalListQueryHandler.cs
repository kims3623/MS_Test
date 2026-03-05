using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Approval;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Application.Features.Approvals.Queries.GetApprovalList;

/// <summary>
/// Handler for GetApprovalListQuery.
/// </summary>
public class GetApprovalListQueryHandler : IRequestHandler<GetApprovalListQuery, Result<ApprovalListResponseDto>>
{
    private readonly IApprovalRepository _approvalRepository;
    private readonly ILogger<GetApprovalListQueryHandler> _logger;

    public GetApprovalListQueryHandler(
        IApprovalRepository approvalRepository,
        ILogger<GetApprovalListQueryHandler> logger)
    {
        _approvalRepository = approvalRepository;
        _logger = logger;
    }

    public async Task<Result<ApprovalListResponseDto>> Handle(GetApprovalListQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Fetching approval list for DivSeq={DivSeq}, Page={Page}",
            request.DivSeq, request.PageNumber);

        try
        {
            var filter = new ApprovalFilterDto
            {
                DivSeq = request.DivSeq,
                UserId = request.UserId,
                AprovState = request.AprovState,
                ChgTypeId = request.ChgTypeId,
                StartDate = request.StartDate,
                EndDate = request.EndDate
            };

            var result = await _approvalRepository.GetListWithPaginationAsync(
                filter,
                request.PageNumber,
                request.PageSize,
                cancellationToken);

            _logger.LogInformation("Found {Count} approvals", result.TotalCount);

            return Result<ApprovalListResponseDto>.Success(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching approval list");
            return Result<ApprovalListResponseDto>.Failure($"승인 목록 조회 중 오류가 발생했습니다: {ex.Message}");
        }
    }
}
