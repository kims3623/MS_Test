using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Data;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Application.Features.Data.Commands.SaveTempRawData;

/// <summary>
/// Handler for SaveTempRawDataCommand.
/// </summary>
public class SaveTempRawDataCommandHandler : IRequestHandler<SaveTempRawDataCommand, Result<RawDataOperationResultDto>>
{
    private readonly IRawDataRepository _repository;
    private readonly ILogger<SaveTempRawDataCommandHandler> _logger;

    public SaveTempRawDataCommandHandler(
        IRawDataRepository repository,
        ILogger<SaveTempRawDataCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<RawDataOperationResultDto>> Handle(SaveTempRawDataCommand request, CancellationToken cancellationToken)
    {
        _logger.LogDebug("Saving temp raw data for DivSeq {DivSeq}, SpecSysId {SpecSysId}",
            request.DivSeq, request.SpecSysId);

        try
        {
            var dto = new SaveTempRawDataDto
            {
                DivSeq = request.DivSeq,
                SpecSysId = request.SpecSysId,
                WorkDate = request.WorkDate,
                Shift = request.Shift,
                LotName = request.LotName,
                RawDataValue = request.RawDataValue,
                InputQty = request.InputQty,
                DefectQty = request.DefectQty,
                CreateUserId = request.UserId
            };

            var result = await _repository.SaveTempRawDataAsync(request.DivSeq, dto, cancellationToken);

            if (!result.Success)
            {
                return Result<RawDataOperationResultDto>.Failure(result.Message);
            }

            return Result<RawDataOperationResultDto>.Success(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error saving temp raw data for DivSeq {DivSeq}", request.DivSeq);
            return Result<RawDataOperationResultDto>.Failure("Failed to save temporary raw data.");
        }
    }
}
