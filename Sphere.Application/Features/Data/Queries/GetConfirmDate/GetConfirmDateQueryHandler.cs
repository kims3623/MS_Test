using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Data;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Application.Features.Data.Queries.GetConfirmDate;

/// <summary>
/// Handler for GetConfirmDateQuery.
/// </summary>
public class GetConfirmDateQueryHandler : IRequestHandler<GetConfirmDateQuery, Result<ConfirmDateListDto>>
{
    private readonly IRawDataRepository _repository;
    private readonly ILogger<GetConfirmDateQueryHandler> _logger;

    public GetConfirmDateQueryHandler(
        IRawDataRepository repository,
        ILogger<GetConfirmDateQueryHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<ConfirmDateListDto>> Handle(GetConfirmDateQuery request, CancellationToken cancellationToken)
    {
        _logger.LogDebug("Getting confirm dates for DivSeq {DivSeq}", request.DivSeq);

        try
        {
            var filter = new RawDataFilterDto
            {
                MtrlClassId = request.MtrlClassId,
                VendorId = request.VendorId,
                StatTypeId = request.StatTypeId
            };

            var result = await _repository.GetConfirmDateAsync(request.DivSeq, filter, cancellationToken);
            return Result<ConfirmDateListDto>.Success(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting confirm dates for DivSeq {DivSeq}", request.DivSeq);
            return Result<ConfirmDateListDto>.Failure("Failed to retrieve confirm dates.");
        }
    }
}
