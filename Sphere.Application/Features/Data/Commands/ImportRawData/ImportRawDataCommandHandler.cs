using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Data;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Application.Features.Data.Commands.ImportRawData;

/// <summary>
/// Handler for ImportRawDataCommand.
/// </summary>
public class ImportRawDataCommandHandler : IRequestHandler<ImportRawDataCommand, Result<ImportRawDataResultDto>>
{
    private readonly IRawDataRepository _repository;
    private readonly ILogger<ImportRawDataCommandHandler> _logger;

    public ImportRawDataCommandHandler(
        IRawDataRepository repository,
        ILogger<ImportRawDataCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<ImportRawDataResultDto>> Handle(ImportRawDataCommand request, CancellationToken cancellationToken)
    {
        _logger.LogDebug("Importing {RowCount} raw data rows for DivSeq {DivSeq}, SpecSysId {SpecSysId}",
            request.Rows.Count, request.DivSeq, request.SpecSysId);

        try
        {
            var dto = new ImportRawDataDto
            {
                DivSeq = request.DivSeq,
                SpecSysId = request.SpecSysId,
                ImportUserId = request.UserId,
                Rows = request.Rows
            };

            var result = await _repository.ImportRawDataAsync(request.DivSeq, dto, cancellationToken);
            return Result<ImportRawDataResultDto>.Success(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error importing raw data for DivSeq {DivSeq}", request.DivSeq);
            return Result<ImportRawDataResultDto>.Failure("Failed to import raw data.");
        }
    }
}
