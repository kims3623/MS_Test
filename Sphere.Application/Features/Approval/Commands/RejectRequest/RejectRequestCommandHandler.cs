using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Interfaces;
using Sphere.Application.Common.Models;

namespace Sphere.Application.Features.Approval.Commands.RejectRequest;

/// <summary>
/// Handler for RejectRequestCommand.
/// </summary>
public class RejectRequestCommandHandler : IRequestHandler<RejectRequestCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly IDateTimeService _dateTimeService;
    private readonly ILogger<RejectRequestCommandHandler> _logger;

    public RejectRequestCommandHandler(
        IApplicationDbContext context,
        IDateTimeService dateTimeService,
        ILogger<RejectRequestCommandHandler> logger)
    {
        _context = context;
        _dateTimeService = dateTimeService;
        _logger = logger;
    }

    public async Task<Result> Handle(RejectRequestCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Processing rejection for {AprovId} by {UserId}", request.AprovId, request.UserId);

        if (string.IsNullOrWhiteSpace(request.Reason))
        {
            return Result.Failure("Rejection reason is required.");
        }

        var approval = await _context.Approvals
            .Where(a => a.DivSeq == request.DivSeq && a.AprovId == request.AprovId)
            .FirstOrDefaultAsync(cancellationToken);

        if (approval is null)
        {
            _logger.LogWarning("Approval {AprovId} not found", request.AprovId);
            return Result.Failure("Approval not found.");
        }

        // Update approval status
        approval.AprovState = "R"; // Rejected
        approval.UpdateUserId = request.UserId;
        approval.UpdateDate = _dateTimeService.Now;

        // Add to history
        var history = new Domain.Entities.Approval.ApprovalHistory
        {
            DivSeq = request.DivSeq,
            AprovId = request.AprovId,
            Action = "REJECT",
            ApproverId = request.UserId,
            ActionDate = _dateTimeService.Now,
            Comment = request.Reason,
            CreateUserId = request.UserId,
            CreateDate = _dateTimeService.Now
        };

        _context.ApprovalHistories.Add(history);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Approval {AprovId} rejected by {UserId}", request.AprovId, request.UserId);

        return Result.Success();
    }
}
