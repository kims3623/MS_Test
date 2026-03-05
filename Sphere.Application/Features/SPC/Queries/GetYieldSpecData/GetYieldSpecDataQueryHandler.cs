using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.SPC;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Application.Features.SPC.Queries.GetYieldSpecData;

/// <summary>
/// Handler for GetYieldSpecDataQuery.
/// </summary>
public class GetYieldSpecDataQueryHandler : IRequestHandler<GetYieldSpecDataQuery, Result<YieldSpecDataDto>>
{
    private readonly ISPCRepository _repository;
    private readonly ILogger<GetYieldSpecDataQueryHandler> _logger;

    public GetYieldSpecDataQueryHandler(
        ISPCRepository repository,
        ILogger<GetYieldSpecDataQueryHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<YieldSpecDataDto>> Handle(
        GetYieldSpecDataQuery request,
        CancellationToken cancellationToken)
    {
        _logger.LogDebug("Getting yield spec data for DivSeq {DivSeq}", request.DivSeq);

        try
        {
            var data = await _repository.GetYieldSpecDataAsync(
                request.DivSeq,
                request.VendorId,
                request.MtrlClassId,
                cancellationToken);

            return Result<YieldSpecDataDto>.Success(data);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting yield spec data for DivSeq {DivSeq}", request.DivSeq);
            return Result<YieldSpecDataDto>.Failure("Failed to retrieve yield spec data.");
        }
    }
}
