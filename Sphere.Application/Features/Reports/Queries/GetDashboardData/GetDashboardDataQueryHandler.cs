using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Reports;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Application.Features.Reports.Queries.GetDashboardData;

/// <summary>
/// Handler for GetDashboardDataQuery.
/// </summary>
public class GetDashboardDataQueryHandler : IRequestHandler<GetDashboardDataQuery, Result<DashboardDataDto>>
{
    private readonly IReportsRepository _repository;
    private readonly ILogger<GetDashboardDataQueryHandler> _logger;

    public GetDashboardDataQueryHandler(
        IReportsRepository repository,
        ILogger<GetDashboardDataQueryHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<DashboardDataDto>> Handle(GetDashboardDataQuery request, CancellationToken cancellationToken)
    {
        _logger.LogDebug("Getting dashboard data for DivSeq {DivSeq}", request.DivSeq);

        try
        {
            var data = await _repository.GetDashboardDataAsync(request.DivSeq, cancellationToken);
            return Result<DashboardDataDto>.Success(data);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting dashboard data for DivSeq {DivSeq}", request.DivSeq);
            return Result<DashboardDataDto>.Failure("Failed to retrieve dashboard data.");
        }
    }
}
