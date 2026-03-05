using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.SPC;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Application.Features.SPC.Queries.GetXBarRChart;

/// <summary>
/// Handler for GetXBarRChartQuery.
/// </summary>
public class GetXBarRChartQueryHandler : IRequestHandler<GetXBarRChartQuery, Result<XBarRChartDto>>
{
    private readonly ISPCRepository _repository;
    private readonly ILogger<GetXBarRChartQueryHandler> _logger;

    public GetXBarRChartQueryHandler(
        ISPCRepository repository,
        ILogger<GetXBarRChartQueryHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<XBarRChartDto>> Handle(
        GetXBarRChartQuery request,
        CancellationToken cancellationToken)
    {
        _logger.LogDebug("Getting X-Bar R chart for SpecSysId {SpecSysId}", request.SpecSysId);

        try
        {
            var query = new ChartDataQueryDto
            {
                DivSeq = request.DivSeq,
                SpecSysId = request.SpecSysId,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Shift = request.Shift
            };

            var data = await _repository.GetXBarRChartAsync(query, cancellationToken);
            return Result<XBarRChartDto>.Success(data);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting X-Bar R chart for SpecSysId {SpecSysId}", request.SpecSysId);
            return Result<XBarRChartDto>.Failure("Failed to retrieve X-Bar R chart data.");
        }
    }
}
