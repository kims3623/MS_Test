using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Interfaces;
using Sphere.Application.Common.Models;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Application.Features.Approval.Commands.ApproveRequest;

/// <summary>
/// Handler for ApproveRequestCommand.
/// </summary>
public class ApproveRequestCommandHandler : IRequestHandler<ApproveRequestCommand, Result>
{
    private readonly IApprovalRepository _approvalRepository;
    private readonly IDateTimeService _dateTimeService;
    private readonly ILogger<ApproveRequestCommandHandler> _logger;

    public ApproveRequestCommandHandler(
        IApprovalRepository approvalRepository,
        IDateTimeService dateTimeService,
        ILogger<ApproveRequestCommandHandler> logger)
    {
        _approvalRepository = approvalRepository;
        _dateTimeService = dateTimeService;
        _logger = logger;
    }

    public async Task<Result> Handle(ApproveRequestCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Processing approval for {AprovId} by {UserId}", request.AprovId, request.UserId);

        var affected = await _approvalRepository.UpdateApprovalStateAsync(
            request.DivSeq, request.AprovId, "A", request.UserId, cancellationToken);

        if (affected == 0)
        {
            _logger.LogWarning("Approval {AprovId} not found", request.AprovId);
            return Result.Failure("Approval not found.");
        }

        await _approvalRepository.InsertApprovalHistoryAsync(
            request.DivSeq,
            request.AprovId,
            "APPROVE",
            request.UserId,
            _dateTimeService.Now,
            request.Comment,
            cancellationToken);

        _logger.LogInformation("Approval {AprovId} approved by {UserId}", request.AprovId, request.UserId);
        return Result.Success();
    }
}
