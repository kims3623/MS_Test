using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Reports;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Application.Features.Reports.Queries.GetYieldReport;

/// <summary>
/// Handler for GetYieldReportQuery.
/// </summary>
public class GetYieldReportQueryHandler : IRequestHandler<GetYieldReportQuery, Result<YieldReportDto>>
{
    private readonly IReportsRepository _repository;
    private readonly ILogger<GetYieldReportQueryHandler> _logger;

    public GetYieldReportQueryHandler(
        IReportsRepository repository,
        ILogger<GetYieldReportQueryHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<YieldReportDto>> Handle(GetYieldReportQuery request, CancellationToken cancellationToken)
    {
        _logger.LogDebug("Getting yield report for DivSeq {DivSeq}", request.DivSeq);

        try
        {
            var filter = new YieldReportFilterDto
            {
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                VendorId = request.VendorId,
                MtrlClassId = request.MtrlClassId,
                SpecId = request.SpecId,
                GroupBy = request.GroupBy
            };

            var data = await _repository.GetYieldReportAsync(request.DivSeq, filter, cancellationToken);
            return Result<YieldReportDto>.Success(data);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting yield report for DivSeq {DivSeq}", request.DivSeq);
            return Result<YieldReportDto>.Failure("Failed to retrieve yield report.");
        }
    }
}
