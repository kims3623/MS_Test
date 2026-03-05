using MediatR;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.SPC;

namespace Sphere.Application.Features.SPC.Commands.SaveChartConfig;

/// <summary>
/// Command to save chart configuration.
/// </summary>
public record SaveChartConfigCommand : IRequest<Result<bool>>
{
    public string UserId { get; init; } = string.Empty;
    public string? ConfigId { get; init; }
    public string ChartType { get; init; } = string.Empty;
    public ChartDisplaySettingsDto? DisplaySettings { get; init; }
    public ChartDataSettingsDto? DataSettings { get; init; }
    public ChartExportSettingsDto? ExportSettings { get; init; }
    public bool IsDefault { get; init; }
}
