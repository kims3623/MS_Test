using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.SPC;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Application.Features.SPC.Commands.SaveChartConfig;

/// <summary>
/// Handler for SaveChartConfigCommand.
/// </summary>
public class SaveChartConfigCommandHandler : IRequestHandler<SaveChartConfigCommand, Result<bool>>
{
    private readonly ISPCRepository _repository;
    private readonly ILogger<SaveChartConfigCommandHandler> _logger;

    public SaveChartConfigCommandHandler(
        ISPCRepository repository,
        ILogger<SaveChartConfigCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<bool>> Handle(
        SaveChartConfigCommand request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Saving chart config for User {UserId}, ChartType {ChartType}",
            request.UserId, request.ChartType);

        try
        {
            var configRequest = new ChartConfigSaveRequestDto
            {
                ConfigId = request.ConfigId,
                ChartType = request.ChartType,
                DisplaySettings = request.DisplaySettings,
                DataSettings = request.DataSettings,
                ExportSettings = request.ExportSettings,
                IsDefault = request.IsDefault
            };

            var success = await _repository.SaveChartConfigAsync(request.UserId, configRequest, cancellationToken);

            if (!success)
            {
                return Result<bool>.Failure("Failed to save chart configuration.");
            }

            _logger.LogInformation("Successfully saved chart config for User {UserId}", request.UserId);
            return Result<bool>.Success(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error saving chart config for User {UserId}", request.UserId);
            return Result<bool>.Failure("Failed to save chart configuration.");
        }
    }
}
