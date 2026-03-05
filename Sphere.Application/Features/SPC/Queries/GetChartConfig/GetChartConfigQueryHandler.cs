using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.SPC;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Application.Features.SPC.Queries.GetChartConfig;

/// <summary>
/// Handler for GetChartConfigQuery.
/// </summary>
public class GetChartConfigQueryHandler : IRequestHandler<GetChartConfigQuery, Result<ChartConfigDto>>
{
    private readonly ISPCRepository _repository;
    private readonly ILogger<GetChartConfigQueryHandler> _logger;

    public GetChartConfigQueryHandler(
        ISPCRepository repository,
        ILogger<GetChartConfigQueryHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<ChartConfigDto>> Handle(
        GetChartConfigQuery request,
        CancellationToken cancellationToken)
    {
        _logger.LogDebug("Getting chart config for UserId {UserId}, ChartType {ChartType}",
            request.UserId, request.ChartType);

        try
        {
            var config = await _repository.GetChartConfigAsync(
                request.UserId,
                request.ChartType,
                cancellationToken);

            // Return default config if none found
            if (config == null)
            {
                config = new ChartConfigDto
                {
                    UserId = request.UserId,
                    ChartType = request.ChartType,
                    DisplaySettings = new ChartDisplaySettingsDto(),
                    DataSettings = new ChartDataSettingsDto(),
                    ExportSettings = new ChartExportSettingsDto(),
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };
            }

            return Result<ChartConfigDto>.Success(config);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting chart config for UserId {UserId}", request.UserId);
            return Result<ChartConfigDto>.Failure("Failed to retrieve chart configuration.");
        }
    }
}
