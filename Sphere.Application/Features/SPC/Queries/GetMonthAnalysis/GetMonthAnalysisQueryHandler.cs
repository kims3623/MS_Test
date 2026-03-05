using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.SPC;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Application.Features.SPC.Queries.GetMonthAnalysis;

/// <summary>
/// Handler for GetMonthAnalysisQuery.
/// </summary>
public class GetMonthAnalysisQueryHandler : IRequestHandler<GetMonthAnalysisQuery, Result<MonthAnalysisDto>>
{
    private readonly ISPCRepository _repository;
    private readonly ILogger<GetMonthAnalysisQueryHandler> _logger;

    public GetMonthAnalysisQueryHandler(
        ISPCRepository repository,
        ILogger<GetMonthAnalysisQueryHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<MonthAnalysisDto>> Handle(
        GetMonthAnalysisQuery request,
        CancellationToken cancellationToken)
    {
        _logger.LogDebug("Getting month analysis for SpecSysId {SpecSysId}, Year {Year}",
            request.SpecSysId, request.Year);

        try
        {
            var query = new MonthAnalysisQueryDto
            {
                DivSeq = request.DivSeq,
                SpecSysId = request.SpecSysId,
                Year = request.Year,
                IncludeYearComparison = request.IncludeYearComparison
            };

            var data = await _repository.GetMonthAnalysisAsync(query, cancellationToken);
            return Result<MonthAnalysisDto>.Success(data);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting month analysis for SpecSysId {SpecSysId}", request.SpecSysId);
            return Result<MonthAnalysisDto>.Failure("Failed to retrieve month analysis data.");
        }
    }
}
