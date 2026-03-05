using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.SPC;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Application.Features.SPC.Queries.GetParetoData;

/// <summary>
/// Handler for GetParetoDataQuery.
/// </summary>
public class GetParetoDataQueryHandler : IRequestHandler<GetParetoDataQuery, Result<ParetoDataDto>>
{
    private readonly ISPCRepository _repository;
    private readonly ILogger<GetParetoDataQueryHandler> _logger;

    public GetParetoDataQueryHandler(
        ISPCRepository repository,
        ILogger<GetParetoDataQueryHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<ParetoDataDto>> Handle(
        GetParetoDataQuery request,
        CancellationToken cancellationToken)
    {
        _logger.LogDebug("Getting Pareto data for SpecSysId {SpecSysId}", request.SpecSysId);

        try
        {
            var query = new ParetoQueryDto
            {
                DivSeq = request.DivSeq,
                SpecSysId = request.SpecSysId,
                AnalysisType = request.AnalysisType,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Shift = request.Shift,
                TopN = request.TopN,
                GroupOthers = request.GroupOthers
            };

            var data = await _repository.GetParetoDataAsync(query, cancellationToken);
            return Result<ParetoDataDto>.Success(data);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting Pareto data for SpecSysId {SpecSysId}", request.SpecSysId);
            return Result<ParetoDataDto>.Failure("Failed to retrieve Pareto data.");
        }
    }
}
