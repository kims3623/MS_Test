using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.SPC;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Application.Features.SPC.Queries.GetTrendAnalysis;

/// <summary>
/// Handler for GetTrendAnalysisQuery.
/// </summary>
public class GetTrendAnalysisQueryHandler : IRequestHandler<GetTrendAnalysisQuery, Result<TrendAnalysisDto>>
{
    private readonly ISPCRepository _repository;
    private readonly ILogger<GetTrendAnalysisQueryHandler> _logger;

    public GetTrendAnalysisQueryHandler(
        ISPCRepository repository,
        ILogger<GetTrendAnalysisQueryHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<TrendAnalysisDto>> Handle(
        GetTrendAnalysisQuery request,
        CancellationToken cancellationToken)
    {
        _logger.LogDebug("Getting trend analysis for SpecSysId {SpecSysId}", request.SpecSysId);

        try
        {
            var query = new TrendAnalysisQueryDto
            {
                DivSeq = request.DivSeq,
                SpecSysId = request.SpecSysId,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                GroupBy = request.GroupBy,
                MovingAvgWindow = request.MovingAvgWindow,
                IncludeForecast = request.IncludeForecast,
                ForecastPeriods = request.ForecastPeriods
            };

            var data = await _repository.GetTrendAnalysisAsync(query, cancellationToken);
            return Result<TrendAnalysisDto>.Success(data);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting trend analysis for SpecSysId {SpecSysId}", request.SpecSysId);
            return Result<TrendAnalysisDto>.Failure("Failed to retrieve trend analysis data.");
        }
    }
}
