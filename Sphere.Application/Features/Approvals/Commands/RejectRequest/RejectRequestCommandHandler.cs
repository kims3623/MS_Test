using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Approval;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Application.Features.Approvals.Commands.RejectRequest;

/// <summary>
/// Handler for RejectRequestCommand.
/// </summary>
public class RejectRequestCommandHandler : IRequestHandler<RejectRequestCommand, Result<ApprovalActionResponseDto>>
{
    private readonly IApprovalRepository _approvalRepository;
    private readonly ILogger<RejectRequestCommandHandler> _logger;

    public RejectRequestCommandHandler(
        IApprovalRepository approvalRepository,
        ILogger<RejectRequestCommandHandler> logger)
    {
        _approvalRepository = approvalRepository;
        _logger = logger;
    }

    public async Task<Result<ApprovalActionResponseDto>> Handle(RejectRequestCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Rejecting request AprovId={AprovId} by UserId={UserId}",
            request.AprovId, request.UserId);

        try
        {
            // Check if user can reject
            var buttonState = await _approvalRepository.GetDetailButtonAsync(
                request.DivSeq,
                request.AprovId,
                request.UserId);

            if (buttonState == null || !buttonState.CanApproveReject)
            {
                return Result<ApprovalActionResponseDto>.Failure("반려 권한이 없습니다.");
            }

            if (string.IsNullOrWhiteSpace(request.Description))
            {
                return Result<ApprovalActionResponseDto>.Failure("반려 사유를 입력해주세요.");
            }

            var rejectRequest = new RejectRequestDto
            {
                DivSeq = request.DivSeq,
                AprovId = request.AprovId,
                UserId = request.UserId,
                Description = request.Description
            };

            var result = await _approvalRepository.RejectAsync(rejectRequest, cancellationToken);

            if (result.Result != "SUCCESS")
            {
                return Result<ApprovalActionResponseDto>.Failure(result.ResultMessage);
            }

            _logger.LogInformation("Approval {AprovId} rejected successfully", request.AprovId);

            return Result<ApprovalActionResponseDto>.Success(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error rejecting {AprovId}", request.AprovId);
            return Result<ApprovalActionResponseDto>.Failure($"반려 처리 중 오류가 발생했습니다: {ex.Message}");
        }
    }
}
