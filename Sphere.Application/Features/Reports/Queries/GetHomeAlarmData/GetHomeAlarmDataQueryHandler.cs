using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Reports;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Application.Features.Reports.Queries.GetHomeAlarmData;

/// <summary>
/// Handler for GetHomeAlarmDataQuery.
/// </summary>
public class GetHomeAlarmDataQueryHandler : IRequestHandler<GetHomeAlarmDataQuery, Result<HomeAlarmDataDto>>
{
    private readonly IReportsRepository _repository;
    private readonly ILogger<GetHomeAlarmDataQueryHandler> _logger;

    public GetHomeAlarmDataQueryHandler(
        IReportsRepository repository,
        ILogger<GetHomeAlarmDataQueryHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<HomeAlarmDataDto>> Handle(GetHomeAlarmDataQuery request, CancellationToken cancellationToken)
    {
        _logger.LogDebug("Getting home alarm data for DivSeq {DivSeq}, Year {Year}", request.DivSeq, request.Year);

        try
        {
            var filter = new AlarmDataFilterDto
            {
                Year = request.Year,
                VendorType = request.VendorType
            };

            var data = await _repository.GetHomeAlarmDataAsync(request.DivSeq, filter, cancellationToken);
            return Result<HomeAlarmDataDto>.Success(data);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting home alarm data for DivSeq {DivSeq}", request.DivSeq);
            return Result<HomeAlarmDataDto>.Failure("Failed to retrieve alarm data.");
        }
    }
}
