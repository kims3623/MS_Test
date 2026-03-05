using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Approval;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Application.Features.Approvals.Commands.ApproveRequest;

/// <summary>
/// Handler for ApproveRequestCommand.
/// </summary>
public class ApproveRequestCommandHandler : IRequestHandler<ApproveRequestCommand, Result<ApprovalActionResponseDto>>
{
    private readonly IApprovalRepository _approvalRepository;
    private readonly ILogger<ApproveRequestCommandHandler> _logger;

    public ApproveRequestCommandHandler(
        IApprovalRepository approvalRepository,
        ILogger<ApproveRequestCommandHandler> logger)
    {
        _approvalRepository = approvalRepository;
        _logger = logger;
    }

    public async Task<Result<ApprovalActionResponseDto>> Handle(ApproveRequestCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Approving request AprovId={AprovId} by UserId={UserId}",
            request.AprovId, request.UserId);

        try
        {
            // Check if user can approve
            var buttonState = await _approvalRepository.GetDetailButtonAsync(
                request.DivSeq,
                request.AprovId,
                request.UserId);

            if (buttonState == null || !buttonState.CanApproveReject)
            {
                return Result<ApprovalActionResponseDto>.Failure("승인 권한이 없습니다.");
            }

            var approveRequest = new ApproveRequestDto
            {
                DivSeq = request.DivSeq,
                AprovId = request.AprovId,
                UserId = request.UserId,
                Comment = request.Comment
            };

            var result = await _approvalRepository.ApproveAsync(approveRequest, cancellationToken);

            if (result.Result != "SUCCESS")
            {
                return Result<ApprovalActionResponseDto>.Failure(result.ResultMessage);
            }

            _logger.LogInformation("Approval {AprovId} approved successfully", request.AprovId);

            return Result<ApprovalActionResponseDto>.Success(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error approving {AprovId}", request.AprovId);
            return Result<ApprovalActionResponseDto>.Failure($"승인 처리 중 오류가 발생했습니다: {ex.Message}");
        }
    }
}
