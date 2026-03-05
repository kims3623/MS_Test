using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.SPC;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Application.Features.SPC.Queries.GetControlLimits;

/// <summary>
/// Handler for GetControlLimitsQuery.
/// </summary>
public class GetControlLimitsQueryHandler : IRequestHandler<GetControlLimitsQuery, Result<ControlLimitsResponseDto>>
{
    private readonly ISPCRepository _repository;
    private readonly ILogger<GetControlLimitsQueryHandler> _logger;

    public GetControlLimitsQueryHandler(
        ISPCRepository repository,
        ILogger<GetControlLimitsQueryHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<ControlLimitsResponseDto>> Handle(
        GetControlLimitsQuery request,
        CancellationToken cancellationToken)
    {
        _logger.LogDebug("Getting control limits for SpecSysId {SpecSysId}, ChartType {ChartType}",
            request.SpecSysId, request.ChartType);

        try
        {
            var data = await _repository.GetControlLimitsAsync(
                request.SpecSysId,
                request.ChartType,
                cancellationToken);

            return Result<ControlLimitsResponseDto>.Success(data);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting control limits for SpecSysId {SpecSysId}", request.SpecSysId);
            return Result<ControlLimitsResponseDto>.Failure("Failed to retrieve control limits.");
        }
    }
}
