using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.SPC;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Application.Features.SPC.Queries.GetChartData;

/// <summary>
/// Handler for GetChartDataQuery.
/// </summary>
public class GetChartDataQueryHandler : IRequestHandler<GetChartDataQuery, Result<IEnumerable<ChartDataResultDto>>>
{
    private readonly ISPCRepository _repository;
    private readonly ILogger<GetChartDataQueryHandler> _logger;

    public GetChartDataQueryHandler(
        ISPCRepository repository,
        ILogger<GetChartDataQueryHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<IEnumerable<ChartDataResultDto>>> Handle(
        GetChartDataQuery request,
        CancellationToken cancellationToken)
    {
        _logger.LogDebug("Getting chart data for SpecSysId {SpecSysId}", request.SpecSysId);

        try
        {
            var query = new ChartDataQueryDto
            {
                DivSeq = request.DivSeq,
                VendorId = request.VendorId,
                MtrlClassId = request.MtrlClassId,
                ProjectId = request.ProjectId,
                SpecSysId = request.SpecSysId,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Shift = request.Shift,
                ChartType = request.ChartType
            };

            var data = await _repository.GetChartDataAsync(query, cancellationToken);
            return Result<IEnumerable<ChartDataResultDto>>.Success(data);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting chart data for SpecSysId {SpecSysId}", request.SpecSysId);
            return Result<IEnumerable<ChartDataResultDto>>.Failure("Failed to retrieve chart data.");
        }
    }
}
