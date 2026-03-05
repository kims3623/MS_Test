using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Approval;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Application.Features.Approvals.Queries.GetApprovalDetail;

/// <summary>
/// Handler for GetApprovalDetailQuery.
/// </summary>
public class GetApprovalDetailQueryHandler : IRequestHandler<GetApprovalDetailQuery, Result<ApprovalDetailDto>>
{
    private readonly IApprovalRepository _approvalRepository;
    private readonly ILogger<GetApprovalDetailQueryHandler> _logger;

    public GetApprovalDetailQueryHandler(
        IApprovalRepository approvalRepository,
        ILogger<GetApprovalDetailQueryHandler> logger)
    {
        _approvalRepository = approvalRepository;
        _logger = logger;
    }

    public async Task<Result<ApprovalDetailDto>> Handle(GetApprovalDetailQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Fetching approval detail for AprovId={AprovId}",
            request.AprovId);

        try
        {
            var detail = await _approvalRepository.GetDetailAsync(request.DivSeq, request.AprovId);

            if (detail == null)
            {
                _logger.LogWarning("Approval not found: {AprovId}", request.AprovId);
                return Result<ApprovalDetailDto>.Failure("승인 정보를 찾을 수 없습니다.");
            }

            return Result<ApprovalDetailDto>.Success(detail);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching approval detail for {AprovId}", request.AprovId);
            return Result<ApprovalDetailDto>.Failure($"승인 상세 조회 중 오류가 발생했습니다: {ex.Message}");
        }
    }
}

/// <summary>
/// Handler for GetApprovalButtonStateQuery.
/// </summary>
public class GetApprovalButtonStateQueryHandler : IRequestHandler<GetApprovalButtonStateQuery, Result<ApprovalDetailButtonDto>>
{
    private readonly IApprovalRepository _approvalRepository;
    private readonly ILogger<GetApprovalButtonStateQueryHandler> _logger;

    public GetApprovalButtonStateQueryHandler(
        IApprovalRepository approvalRepository,
        ILogger<GetApprovalButtonStateQueryHandler> logger)
    {
        _approvalRepository = approvalRepository;
        _logger = logger;
    }

    public async Task<Result<ApprovalDetailButtonDto>> Handle(GetApprovalButtonStateQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Fetching button state for AprovId={AprovId}, UserId={UserId}",
            request.AprovId, request.UserId);

        try
        {
            var buttonState = await _approvalRepository.GetDetailButtonAsync(
                request.DivSeq,
                request.AprovId,
                request.UserId);

            if (buttonState == null)
            {
                return Result<ApprovalDetailButtonDto>.Success(new ApprovalDetailButtonDto());
            }

            return Result<ApprovalDetailButtonDto>.Success(buttonState);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching button state for {AprovId}", request.AprovId);
            return Result<ApprovalDetailButtonDto>.Failure($"버튼 상태 조회 중 오류가 발생했습니다: {ex.Message}");
        }
    }
}

/// <summary>
/// Handler for GetApprovalContentQuery.
/// </summary>
public class GetApprovalContentQueryHandler : IRequestHandler<GetApprovalContentQuery, Result<ApprovalDetailContentDto>>
{
    private readonly IApprovalRepository _approvalRepository;
    private readonly ILogger<GetApprovalContentQueryHandler> _logger;

    public GetApprovalContentQueryHandler(
        IApprovalRepository approvalRepository,
        ILogger<GetApprovalContentQueryHandler> logger)
    {
        _approvalRepository = approvalRepository;
        _logger = logger;
    }

    public async Task<Result<ApprovalDetailContentDto>> Handle(GetApprovalContentQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Fetching content for AprovId={AprovId}",
            request.AprovId);

        try
        {
            var content = await _approvalRepository.GetDetailContentAsync(request.DivSeq, request.AprovId);

            if (content == null)
            {
                return Result<ApprovalDetailContentDto>.Failure("승인 내용을 찾을 수 없습니다.");
            }

            return Result<ApprovalDetailContentDto>.Success(content);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching content for {AprovId}", request.AprovId);
            return Result<ApprovalDetailContentDto>.Failure($"승인 내용 조회 중 오류가 발생했습니다: {ex.Message}");
        }
    }
}
