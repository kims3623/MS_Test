using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.SPC;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Application.Features.SPC.Queries.GetDayAnalysis;

/// <summary>
/// Handler for GetDayAnalysisQuery.
/// </summary>
public class GetDayAnalysisQueryHandler : IRequestHandler<GetDayAnalysisQuery, Result<DayAnalysisDto>>
{
    private readonly ISPCRepository _repository;
    private readonly ILogger<GetDayAnalysisQueryHandler> _logger;

    public GetDayAnalysisQueryHandler(
        ISPCRepository repository,
        ILogger<GetDayAnalysisQueryHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<DayAnalysisDto>> Handle(
        GetDayAnalysisQuery request,
        CancellationToken cancellationToken)
    {
        _logger.LogDebug("Getting day analysis for SpecSysId {SpecSysId}", request.SpecSysId);

        try
        {
            var query = new DayAnalysisQueryDto
            {
                DivSeq = request.DivSeq,
                SpecSysId = request.SpecSysId,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Shift = request.Shift,
                IncludeShiftBreakdown = request.IncludeShiftBreakdown
            };

            var data = await _repository.GetDayAnalysisAsync(query, cancellationToken);
            return Result<DayAnalysisDto>.Success(data);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting day analysis for SpecSysId {SpecSysId}", request.SpecSysId);
            return Result<DayAnalysisDto>.Failure("Failed to retrieve day analysis data.");
        }
    }
}
