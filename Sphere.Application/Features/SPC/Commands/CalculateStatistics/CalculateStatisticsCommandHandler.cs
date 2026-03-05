using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.SPC;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Application.Features.SPC.Commands.CalculateStatistics;

/// <summary>
/// Handler for CalculateStatisticsCommand.
/// </summary>
public class CalculateStatisticsCommandHandler : IRequestHandler<CalculateStatisticsCommand, Result<StatisticsCalcResultDto>>
{
    private readonly ISPCRepository _repository;
    private readonly ILogger<CalculateStatisticsCommandHandler> _logger;

    public CalculateStatisticsCommandHandler(
        ISPCRepository repository,
        ILogger<CalculateStatisticsCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<StatisticsCalcResultDto>> Handle(
        CalculateStatisticsCommand request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Calculating statistics for SpecSysId {SpecSysId}", request.SpecSysId);

        try
        {
            var query = new StatisticsCalcQueryDto
            {
                DivSeq = request.DivSeq,
                SpecSysId = request.SpecSysId,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Shift = request.Shift,
                StatType = request.StatType
            };

            var result = await _repository.CalculateStatisticsAsync(query, cancellationToken);

            if (result == null)
            {
                return Result<StatisticsCalcResultDto>.Failure("No data found for statistics calculation.");
            }

            return Result<StatisticsCalcResultDto>.Success(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error calculating statistics for SpecSysId {SpecSysId}", request.SpecSysId);
            return Result<StatisticsCalcResultDto>.Failure("Failed to calculate statistics.");
        }
    }
}
