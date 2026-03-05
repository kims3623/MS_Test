using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.SPC;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Application.Features.SPC.Commands.CheckRunRule;

/// <summary>
/// Handler for CheckRunRuleCommand.
/// </summary>
public class CheckRunRuleCommandHandler : IRequestHandler<CheckRunRuleCommand, Result<RunRuleCheckResponseDto>>
{
    private readonly ISPCRepository _repository;
    private readonly ILogger<CheckRunRuleCommandHandler> _logger;

    public CheckRunRuleCommandHandler(
        ISPCRepository repository,
        ILogger<CheckRunRuleCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<RunRuleCheckResponseDto>> Handle(
        CheckRunRuleCommand request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Checking run rules for SpecSysId {SpecSysId}", request.SpecSysId);

        try
        {
            var checkRequest = new RunRuleCheckRequestDto
            {
                DivSeq = request.DivSeq,
                SpecSysId = request.SpecSysId,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Shift = request.Shift,
                RuleIds = request.RuleIds
            };

            var result = await _repository.CheckRunRulesAsync(checkRequest, cancellationToken);

            _logger.LogInformation(
                "Run rule check completed for SpecSysId {SpecSysId}: {ViolationCount} violations found",
                request.SpecSysId, result.TotalViolations);

            return Result<RunRuleCheckResponseDto>.Success(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking run rules for SpecSysId {SpecSysId}", request.SpecSysId);
            return Result<RunRuleCheckResponseDto>.Failure("Failed to check run rules.");
        }
    }
}
