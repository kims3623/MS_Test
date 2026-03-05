using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.SPC;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Application.Features.SPC.Queries.GetCpkData;

/// <summary>
/// Handler for GetCpkDataQuery.
/// </summary>
public class GetCpkDataQueryHandler : IRequestHandler<GetCpkDataQuery, Result<CpkAnalysisDto>>
{
    private readonly ISPCRepository _repository;
    private readonly ILogger<GetCpkDataQueryHandler> _logger;

    public GetCpkDataQueryHandler(
        ISPCRepository repository,
        ILogger<GetCpkDataQueryHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<CpkAnalysisDto>> Handle(
        GetCpkDataQuery request,
        CancellationToken cancellationToken)
    {
        _logger.LogDebug("Getting Cpk data for SpecSysId {SpecSysId}", request.SpecSysId);

        try
        {
            var query = new CpkQueryDto
            {
                DivSeq = request.DivSeq,
                SpecSysId = request.SpecSysId,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Shift = request.Shift,
                HistogramBins = request.HistogramBins,
                IncludeTrend = request.IncludeTrend
            };

            var data = await _repository.GetCpkDataAsync(query, cancellationToken);
            return Result<CpkAnalysisDto>.Success(data);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting Cpk data for SpecSysId {SpecSysId}", request.SpecSysId);
            return Result<CpkAnalysisDto>.Failure("Failed to retrieve Cpk analysis data.");
        }
    }
}
