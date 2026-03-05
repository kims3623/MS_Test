using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Data;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Application.Features.Data.Queries.GetTempRawData;

/// <summary>
/// Handler for GetTempRawDataQuery.
/// </summary>
public class GetTempRawDataQueryHandler : IRequestHandler<GetTempRawDataQuery, Result<TempRawDataListDto>>
{
    private readonly IRawDataRepository _repository;
    private readonly ILogger<GetTempRawDataQueryHandler> _logger;

    public GetTempRawDataQueryHandler(
        IRawDataRepository repository,
        ILogger<GetTempRawDataQueryHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<TempRawDataListDto>> Handle(GetTempRawDataQuery request, CancellationToken cancellationToken)
    {
        _logger.LogDebug("Getting temp raw data for DivSeq {DivSeq}", request.DivSeq);

        try
        {
            var filter = new RawDataFilterDto
            {
                VendorId = request.VendorId,
                SpecSysId = request.SpecSysId,
                StartDate = request.StartDate,
                EndDate = request.EndDate
            };

            var result = await _repository.GetTempRawDataAsync(request.DivSeq, filter, cancellationToken);
            return Result<TempRawDataListDto>.Success(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting temp raw data for DivSeq {DivSeq}", request.DivSeq);
            return Result<TempRawDataListDto>.Failure("Failed to retrieve temporary raw data.");
        }
    }
}
