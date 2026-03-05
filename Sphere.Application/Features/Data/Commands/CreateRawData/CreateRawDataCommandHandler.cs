using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Data;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Application.Features.Data.Commands.CreateRawData;

/// <summary>
/// Handler for CreateRawDataCommand.
/// </summary>
public class CreateRawDataCommandHandler : IRequestHandler<CreateRawDataCommand, Result<RawDataOperationResultDto>>
{
    private readonly IRawDataRepository _repository;
    private readonly ILogger<CreateRawDataCommandHandler> _logger;

    public CreateRawDataCommandHandler(
        IRawDataRepository repository,
        ILogger<CreateRawDataCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<RawDataOperationResultDto>> Handle(CreateRawDataCommand request, CancellationToken cancellationToken)
    {
        _logger.LogDebug("Creating raw data for DivSeq {DivSeq}, SpecSysId {SpecSysId}",
            request.DivSeq, request.SpecSysId);

        try
        {
            var dto = new RawDataInsertDto
            {
                DivSeq = request.DivSeq,
                SpecSysId = request.SpecSysId,
                WorkDate = request.WorkDate,
                Shift = request.Shift,
                LotName = request.LotName,
                Frequency = request.Frequency ?? string.Empty,
                RawDataValue = request.RawDataValue,
                InputQty = request.InputQty,
                DefectQty = request.DefectQty,
                CreateUserId = request.UserId
            };

            var result = await _repository.InsertRawDataAsync(request.DivSeq, dto, cancellationToken);

            if (!result.Success)
            {
                return Result<RawDataOperationResultDto>.Failure(result.Message);
            }

            return Result<RawDataOperationResultDto>.Success(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating raw data for DivSeq {DivSeq}", request.DivSeq);
            return Result<RawDataOperationResultDto>.Failure("Failed to create raw data.");
        }
    }
}
