using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Interfaces;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Approval;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Application.Features.Approvals.Commands.BatchApprove;

public class BatchApproveCommandHandler : IRequestHandler<BatchApproveCommand, Result<BatchApproveResponseDto>>
{
    private readonly IApprovalRepository _approvalRepository;
    private readonly IDateTimeService _dateTimeService;
    private readonly ILogger<BatchApproveCommandHandler> _logger;

    public BatchApproveCommandHandler(
        IApprovalRepository approvalRepository,
        IDateTimeService dateTimeService,
        ILogger<BatchApproveCommandHandler> logger)
    {
        _approvalRepository = approvalRepository;
        _dateTimeService = dateTimeService;
        _logger = logger;
    }

    public async Task<Result<BatchApproveResponseDto>> Handle(BatchApproveCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Batch approving {Count} items by {UserId}", request.AprovIds.Count, request.UserId);

        var successCount = 0;
        var failCount = 0;
        var errors = new List<string>();

        foreach (var aprovId in request.AprovIds)
        {
            var affected = await _approvalRepository.UpdateApprovalStateAsync(
                request.DivSeq, aprovId, "A", request.UserId, cancellationToken);

            if (affected == 0)
            {
                failCount++;
                errors.Add($"Approval {aprovId} not found.");
                continue;
            }

            await _approvalRepository.InsertApprovalHistoryAsync(
                request.DivSeq,
                aprovId,
                "APPROVE",
                request.UserId,
                _dateTimeService.Now,
                request.Comment,
                cancellationToken);

            successCount++;
        }

        _logger.LogInformation("Batch approve completed: {Success} success, {Fail} fail", successCount, failCount);

        return Result<BatchApproveResponseDto>.Success(new BatchApproveResponseDto
        {
            SuccessCount = successCount,
            FailCount = failCount,
            Errors = errors
        });
    }
}
