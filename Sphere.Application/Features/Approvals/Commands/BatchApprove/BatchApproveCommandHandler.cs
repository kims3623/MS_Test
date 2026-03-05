using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Interfaces;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Approval;

namespace Sphere.Application.Features.Approvals.Commands.BatchApprove;

public class BatchApproveCommandHandler : IRequestHandler<BatchApproveCommand, Result<BatchApproveResponseDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IDateTimeService _dateTimeService;
    private readonly ILogger<BatchApproveCommandHandler> _logger;

    public BatchApproveCommandHandler(
        IApplicationDbContext context,
        IDateTimeService dateTimeService,
        ILogger<BatchApproveCommandHandler> logger)
    {
        _context = context;
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
            var approval = await _context.Approvals
                .Where(a => a.DivSeq == request.DivSeq && a.AprovId == aprovId)
                .FirstOrDefaultAsync(cancellationToken);

            if (approval is null)
            {
                failCount++;
                errors.Add($"Approval {aprovId} not found.");
                continue;
            }

            approval.AprovState = "A"; // Approved
            approval.UpdateUserId = request.UserId;
            approval.UpdateDate = _dateTimeService.Now;

            var history = new Domain.Entities.Approval.ApprovalHistory
            {
                DivSeq = request.DivSeq,
                AprovId = aprovId,
                Action = "APPROVE",
                ApproverId = request.UserId,
                ActionDate = _dateTimeService.Now,
                Comment = request.Comment ?? string.Empty,
                CreateUserId = request.UserId,
                CreateDate = _dateTimeService.Now
            };

            _context.ApprovalHistories.Add(history);
            successCount++;
        }

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Batch approve completed: {Success} success, {Fail} fail", successCount, failCount);

        var response = new BatchApproveResponseDto
        {
            SuccessCount = successCount,
            FailCount = failCount,
            Errors = errors
        };

        return Result<BatchApproveResponseDto>.Success(response);
    }
}
