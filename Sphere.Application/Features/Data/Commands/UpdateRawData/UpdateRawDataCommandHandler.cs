using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Data;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Application.Features.Data.Commands.UpdateRawData;

/// <summary>
/// Handler for UpdateRawDataCommand.
/// </summary>
public class UpdateRawDataCommandHandler : IRequestHandler<UpdateRawDataCommand, Result<RawDataOperationResultDto>>
{
    private readonly IRawDataRepository _repository;
    private readonly ILogger<UpdateRawDataCommandHandler> _logger;

    public UpdateRawDataCommandHandler(
        IRawDataRepository repository,
        ILogger<UpdateRawDataCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<RawDataOperationResultDto>> Handle(UpdateRawDataCommand request, CancellationToken cancellationToken)
    {
        _logger.LogDebug("Updating raw data ID {RawDataId} for DivSeq {DivSeq}",
            request.RawDataId, request.DivSeq);

        try
        {
            var dto = new RawDataUpdateDto
            {
                RawDataId = request.RawDataId,
                SpecSysId = request.SpecSysId,
                WorkDate = request.WorkDate,
                Shift = request.Shift,
                LotName = request.LotName,
                RawDataValue = request.RawDataValue,
                InputQty = request.InputQty,
                DefectQty = request.DefectQty,
                UpdateUserId = request.UserId
            };

            var result = await _repository.UpdateRawDataAsync(request.DivSeq, dto, cancellationToken);

            if (!result.Success)
            {
                return Result<RawDataOperationResultDto>.Failure(result.Message);
            }

            return Result<RawDataOperationResultDto>.Success(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating raw data ID {RawDataId}", request.RawDataId);
            return Result<RawDataOperationResultDto>.Failure("Failed to update raw data.");
        }
    }
}
