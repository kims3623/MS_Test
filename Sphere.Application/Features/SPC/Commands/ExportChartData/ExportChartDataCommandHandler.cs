using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.SPC;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Application.Features.SPC.Commands.ExportChartData;

/// <summary>
/// Handler for ExportChartDataCommand.
/// </summary>
public class ExportChartDataCommandHandler : IRequestHandler<ExportChartDataCommand, Result<ChartExportResponseDto>>
{
    private readonly ISPCRepository _repository;
    private readonly ILogger<ExportChartDataCommandHandler> _logger;

    public ExportChartDataCommandHandler(
        ISPCRepository repository,
        ILogger<ExportChartDataCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<ChartExportResponseDto>> Handle(
        ExportChartDataCommand request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Exporting chart data for SpecSysId {SpecSysId}, Format {Format}",
            request.SpecSysId, request.ExportFormat);

        try
        {
            var exportRequest = new ChartExportRequestDto
            {
                SpecSysId = request.SpecSysId,
                ChartType = request.ChartType,
                ExportFormat = request.ExportFormat,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Shift = request.Shift,
                IncludeRawData = request.IncludeRawData,
                IncludeStatistics = request.IncludeStatistics,
                IncludeChartImage = request.IncludeChartImage
            };

            var exportData = await _repository.GetChartExportDataAsync(exportRequest, cancellationToken);

            // Generate export file based on format
            var response = await GenerateExportFileAsync(exportData, request.ExportFormat, request.SpecSysId);

            _logger.LogInformation("Successfully exported chart data for SpecSysId {SpecSysId}", request.SpecSysId);
            return Result<ChartExportResponseDto>.Success(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error exporting chart data for SpecSysId {SpecSysId}", request.SpecSysId);
            return Result<ChartExportResponseDto>.Failure("Failed to export chart data.");
        }
    }

    private static Task<ChartExportResponseDto> GenerateExportFileAsync(
        ChartExportDataDto data,
        string format,
        string specSysId)
    {
        var fileName = $"SPC_Chart_{specSysId}_{DateTime.UtcNow:yyyyMMdd_HHmmss}.{format}";
        var contentType = format.ToLower() switch
        {
            "xlsx" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            "csv" => "text/csv",
            "pdf" => "application/pdf",
            _ => "application/octet-stream"
        };

        // In production, this would generate actual file content
        // For now, return metadata
        var response = new ChartExportResponseDto
        {
            FileName = fileName,
            ContentType = contentType,
            FileContent = Array.Empty<byte>(), // Would be actual file bytes
            FileSize = 0,
            GeneratedAt = DateTime.UtcNow
        };

        return Task.FromResult(response);
    }
}
