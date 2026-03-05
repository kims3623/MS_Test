using MediatR;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.SPC;

namespace Sphere.Application.Features.SPC.Commands.ExportChartData;

/// <summary>
/// Command to export chart data.
/// </summary>
public record ExportChartDataCommand : IRequest<Result<ChartExportResponseDto>>
{
    public string UserId { get; init; } = string.Empty;
    public string SpecSysId { get; init; } = string.Empty;
    public string ChartType { get; init; } = string.Empty;
    public string ExportFormat { get; init; } = "xlsx";
    public string? StartDate { get; init; }
    public string? EndDate { get; init; }
    public string? Shift { get; init; }
    public bool IncludeRawData { get; init; } = true;
    public bool IncludeStatistics { get; init; } = true;
    public bool IncludeChartImage { get; init; }
}
