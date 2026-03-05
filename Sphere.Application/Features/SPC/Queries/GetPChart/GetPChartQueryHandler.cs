using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.SPC;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Application.Features.SPC.Queries.GetPChart;

/// <summary>
/// Handler for GetPChartQuery.
/// </summary>
public class GetPChartQueryHandler : IRequestHandler<GetPChartQuery, Result<PChartDto>>
{
    private readonly ISPCRepository _repository;
    private readonly ILogger<GetPChartQueryHandler> _logger;

    public GetPChartQueryHandler(
        ISPCRepository repository,
        ILogger<GetPChartQueryHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<PChartDto>> Handle(
        GetPChartQuery request,
        CancellationToken cancellationToken)
    {
        _logger.LogDebug("Getting P chart for SpecSysId {SpecSysId}", request.SpecSysId);

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

            var data = await _repository.GetPChartAsync(query, cancellationToken);
            return Result<PChartDto>.Success(data);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting P chart for SpecSysId {SpecSysId}", request.SpecSysId);
            return Result<PChartDto>.Failure("Failed to retrieve P chart data.");
        }
    }
}
