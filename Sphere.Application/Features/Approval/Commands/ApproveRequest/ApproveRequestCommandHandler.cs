using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Interfaces;
using Sphere.Application.Common.Models;

namespace Sphere.Application.Features.Approval.Commands.ApproveRequest;

/// <summary>
/// Handler for ApproveRequestCommand.
/// </summary>
public class ApproveRequestCommandHandler : IRequestHandler<ApproveRequestCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly IDateTimeService _dateTimeService;
    private readonly ILogger<ApproveRequestCommandHandler> _logger;

    public ApproveRequestCommandHandler(
        IApplicationDbContext context,
        IDateTimeService dateTimeService,
        ILogger<ApproveRequestCommandHandler> logger)
    {
        _context = context;
        _dateTimeService = dateTimeService;
        _logger = logger;
    }

    public async Task<Result> Handle(ApproveRequestCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Processing approval for {AprovId} by {UserId}", request.AprovId, request.UserId);

        var approval = await _context.Approvals
            .Where(a => a.DivSeq == request.DivSeq && a.AprovId == request.AprovId)
            .FirstOrDefaultAsync(cancellationToken);

        if (approval is null)
        {
            _logger.LogWarning("Approval {AprovId} not found", request.AprovId);
            return Result.Failure("Approval not found.");
        }

        // Update approval status
        approval.AprovState = "A"; // Approved
        approval.UpdateUserId = request.UserId;
        approval.UpdateDate = _dateTimeService.Now;

        // Add to history
        var history = new Domain.Entities.Approval.ApprovalHistory
        {
            DivSeq = request.DivSeq,
            AprovId = request.AprovId,
            Action = "APPROVE",
            ApproverId = request.UserId,
            ActionDate = _dateTimeService.Now,
            Comment = request.Comment ?? string.Empty,
            CreateUserId = request.UserId,
            CreateDate = _dateTimeService.Now
        };

        _context.ApprovalHistories.Add(history);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Approval {AprovId} approved by {UserId}", request.AprovId, request.UserId);

        return Result.Success();
    }
}
