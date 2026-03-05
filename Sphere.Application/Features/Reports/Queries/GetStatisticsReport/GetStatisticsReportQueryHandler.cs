using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Reports;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Application.Features.Reports.Queries.GetStatisticsReport;

/// <summary>
/// Handler for GetStatisticsReportQuery.
/// </summary>
public class GetStatisticsReportQueryHandler : IRequestHandler<GetStatisticsReportQuery, Result<StatisticsReportDto>>
{
    private readonly IReportsRepository _repository;
    private readonly ILogger<GetStatisticsReportQueryHandler> _logger;

    public GetStatisticsReportQueryHandler(
        IReportsRepository repository,
        ILogger<GetStatisticsReportQueryHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<StatisticsReportDto>> Handle(GetStatisticsReportQuery request, CancellationToken cancellationToken)
    {
        _logger.LogDebug("Getting statistics report for DivSeq {DivSeq}", request.DivSeq);

        try
        {
            var filter = new StatisticsFilterDto
            {
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                ReportType = request.ReportType,
                CategoryId = request.CategoryId,
                VendorId = request.VendorId,
                GroupBy = request.GroupBy
            };

            var data = await _repository.GetStatisticsReportAsync(request.DivSeq, filter, cancellationToken);
            return Result<StatisticsReportDto>.Success(data);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting statistics report for DivSeq {DivSeq}", request.DivSeq);
            return Result<StatisticsReportDto>.Failure("Failed to retrieve statistics report.");
        }
    }
}
