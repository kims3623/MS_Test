using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Data;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Application.Features.Data.Commands.BatchSaveRawData;

/// <summary>
/// Handler for BatchSaveRawDataCommand.
/// </summary>
public class BatchSaveRawDataCommandHandler : IRequestHandler<BatchSaveRawDataCommand, Result<BatchSaveResultDto>>
{
    private readonly IRawDataRepository _repository;
    private readonly ILogger<BatchSaveRawDataCommandHandler> _logger;

    public BatchSaveRawDataCommandHandler(
        IRawDataRepository repository,
        ILogger<BatchSaveRawDataCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<BatchSaveResultDto>> Handle(BatchSaveRawDataCommand request, CancellationToken cancellationToken)
    {
        _logger.LogDebug("Batch saving {RowCount} raw data rows for DivSeq {DivSeq}, SpecSysId {SpecSysId}",
            request.Rows.Count, request.DivSeq, request.SpecSysId);

        try
        {
            var dto = new BatchSaveRawDataDto
            {
                DivSeq = request.DivSeq,
                SpecSysId = request.SpecSysId,
                SaveUserId = request.UserId,
                Rows = request.Rows
            };

            var result = await _repository.BatchSaveRawDataAsync(request.DivSeq, dto, cancellationToken);
            return Result<BatchSaveResultDto>.Success(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error batch saving raw data for DivSeq {DivSeq}", request.DivSeq);
            return Result<BatchSaveResultDto>.Failure("Failed to batch save raw data.");
        }
    }
}
