using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Reports;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Application.Features.Reports.Queries.GetHomeIssueData;

/// <summary>
/// Handler for GetHomeIssueDataQuery.
/// </summary>
public class GetHomeIssueDataQueryHandler : IRequestHandler<GetHomeIssueDataQuery, Result<List<HomeIssueDataDto>>>
{
    private readonly IReportsRepository _repository;
    private readonly ILogger<GetHomeIssueDataQueryHandler> _logger;

    public GetHomeIssueDataQueryHandler(
        IReportsRepository repository,
        ILogger<GetHomeIssueDataQueryHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<List<HomeIssueDataDto>>> Handle(GetHomeIssueDataQuery request, CancellationToken cancellationToken)
    {
        _logger.LogDebug("Getting home issue data for DivSeq {DivSeq}", request.DivSeq);

        try
        {
            var filter = new IssueDataFilterDto
            {
                VendorType = request.VendorType,
                StatTypeId = request.StatTypeId,
                VendorId = request.VendorId
            };

            var data = await _repository.GetHomeIssueDataAsync(request.DivSeq, filter, cancellationToken);
            return Result<List<HomeIssueDataDto>>.Success(data.ToList());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting home issue data for DivSeq {DivSeq}", request.DivSeq);
            return Result<List<HomeIssueDataDto>>.Failure("Failed to retrieve issue data.");
        }
    }
}
