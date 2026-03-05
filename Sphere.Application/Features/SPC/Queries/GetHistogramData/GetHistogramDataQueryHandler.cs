using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.SPC;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Application.Features.SPC.Queries.GetHistogramData;

/// <summary>
/// Handler for GetHistogramDataQuery.
/// </summary>
public class GetHistogramDataQueryHandler : IRequestHandler<GetHistogramDataQuery, Result<HistogramDataDto>>
{
    private readonly ISPCRepository _repository;
    private readonly ILogger<GetHistogramDataQueryHandler> _logger;

    public GetHistogramDataQueryHandler(
        ISPCRepository repository,
        ILogger<GetHistogramDataQueryHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<HistogramDataDto>> Handle(
        GetHistogramDataQuery request,
        CancellationToken cancellationToken)
    {
        _logger.LogDebug("Getting histogram data for SpecSysId {SpecSysId}", request.SpecSysId);

        try
        {
            var query = new HistogramQueryDto
            {
                DivSeq = request.DivSeq,
                SpecSysId = request.SpecSysId,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Shift = request.Shift,
                BinCount = request.BinCount,
                BinWidth = request.BinWidth,
                IncludeNormalCurve = request.IncludeNormalCurve
            };

            var data = await _repository.GetHistogramDataAsync(query, cancellationToken);
            return Result<HistogramDataDto>.Success(data);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting histogram data for SpecSysId {SpecSysId}", request.SpecSysId);
            return Result<HistogramDataDto>.Failure("Failed to retrieve histogram data.");
        }
    }
}
