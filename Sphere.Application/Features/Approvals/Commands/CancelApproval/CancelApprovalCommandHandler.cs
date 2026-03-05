using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Approval;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Application.Features.Approvals.Commands.CancelApproval;

/// <summary>
/// Handler for CancelApprovalCommand.
/// </summary>
public class CancelApprovalCommandHandler : IRequestHandler<CancelApprovalCommand, Result<ApprovalActionResponseDto>>
{
    private readonly IApprovalRepository _approvalRepository;
    private readonly ILogger<CancelApprovalCommandHandler> _logger;

    public CancelApprovalCommandHandler(
        IApprovalRepository approvalRepository,
        ILogger<CancelApprovalCommandHandler> logger)
    {
        _approvalRepository = approvalRepository;
        _logger = logger;
    }

    public async Task<Result<ApprovalActionResponseDto>> Handle(CancelApprovalCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Cancelling request AprovId={AprovId} by UserId={UserId}",
            request.AprovId, request.UserId);

        try
        {
            // Check if user can cancel (must be writer)
            var buttonState = await _approvalRepository.GetDetailButtonAsync(
                request.DivSeq,
                request.AprovId,
                request.UserId);

            if (buttonState == null || !buttonState.CanCancel)
            {
                return Result<ApprovalActionResponseDto>.Failure("취소 권한이 없습니다. 작성자만 취소할 수 있습니다.");
            }

            var cancelRequest = new CancelApprovalRequestDto
            {
                DivSeq = request.DivSeq,
                AprovId = request.AprovId,
                UserId = request.UserId,
                Reason = request.Reason
            };

            var result = await _approvalRepository.CancelAsync(cancelRequest, cancellationToken);

            if (result.Result != "SUCCESS")
            {
                return Result<ApprovalActionResponseDto>.Failure(result.ResultMessage);
            }

            _logger.LogInformation("Approval {AprovId} cancelled successfully", request.AprovId);

            return Result<ApprovalActionResponseDto>.Success(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error cancelling {AprovId}", request.AprovId);
            return Result<ApprovalActionResponseDto>.Failure($"취소 처리 중 오류가 발생했습니다: {ex.Message}");
        }
    }
}
