using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Interfaces;
using Sphere.Application.Common.Models;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Application.Features.Approval.Commands.RejectRequest;

/// <summary>
/// Handler for RejectRequestCommand.
/// </summary>
public class RejectRequestCommandHandler : IRequestHandler<RejectRequestCommand, Result>
{
    private readonly IApprovalRepository _approvalRepository;
    private readonly IDateTimeService _dateTimeService;
    private readonly ILogger<RejectRequestCommandHandler> _logger;

    public RejectRequestCommandHandler(
        IApprovalRepository approvalRepository,
        IDateTimeService dateTimeService,
        ILogger<RejectRequestCommandHandler> logger)
    {
        _approvalRepository = approvalRepository;
        _dateTimeService = dateTimeService;
        _logger = logger;
    }

    public async Task<Result> Handle(RejectRequestCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Processing rejection for {AprovId} by {UserId}", request.AprovId, request.UserId);

        if (string.IsNullOrWhiteSpace(request.Reason))
            return Result.Failure("Rejection reason is required.");

        var affected = await _approvalRepository.UpdateApprovalStateAsync(
            request.DivSeq, request.AprovId, "R", request.UserId, cancellationToken);

        if (affected == 0)
        {
            _logger.LogWarning("Approval {AprovId} not found", request.AprovId);
            return Result.Failure("Approval not found.");
        }

        await _approvalRepository.InsertApprovalHistoryAsync(
            request.DivSeq,
            request.AprovId,
            "REJECT",
            request.UserId,
            _dateTimeService.Now,
            request.Reason,
            cancellationToken);

        _logger.LogInformation("Approval {AprovId} rejected by {UserId}", request.AprovId, request.UserId);
        return Result.Success();
    }
}
