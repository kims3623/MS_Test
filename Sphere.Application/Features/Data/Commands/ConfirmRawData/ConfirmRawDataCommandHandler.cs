using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Data;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Application.Features.Data.Commands.ConfirmRawData;

/// <summary>
/// Handler for ConfirmRawDataCommand.
/// </summary>
public class ConfirmRawDataCommandHandler : IRequestHandler<ConfirmRawDataCommand, Result<RawDataOperationResultDto>>
{
    private readonly IRawDataRepository _repository;
    private readonly ILogger<ConfirmRawDataCommandHandler> _logger;

    public ConfirmRawDataCommandHandler(
        IRawDataRepository repository,
        ILogger<ConfirmRawDataCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<RawDataOperationResultDto>> Handle(ConfirmRawDataCommand request, CancellationToken cancellationToken)
    {
        _logger.LogDebug("Confirming raw data for DivSeq {DivSeq}, SpecSysId {SpecSysId}, WorkDate {WorkDate}",
            request.DivSeq, request.SpecSysId, request.WorkDate);

        try
        {
            var dto = new ConfirmRawDataDto
            {
                DivSeq = request.DivSeq,
                SpecSysId = request.SpecSysId,
                WorkDate = request.WorkDate,
                ConfirmUserId = request.UserId
            };

            var result = await _repository.ConfirmRawDataAsync(request.DivSeq, dto, cancellationToken);

            if (!result.Success)
            {
                return Result<RawDataOperationResultDto>.Failure(result.Message);
            }

            return Result<RawDataOperationResultDto>.Success(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error confirming raw data for DivSeq {DivSeq}", request.DivSeq);
            return Result<RawDataOperationResultDto>.Failure("Failed to confirm raw data.");
        }
    }
}
