using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Data;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Application.Features.Data.Commands.ValidateRawData;

/// <summary>
/// Handler for ValidateRawDataCommand.
/// </summary>
public class ValidateRawDataCommandHandler : IRequestHandler<ValidateRawDataCommand, Result<ValidationResultDto>>
{
    private readonly IRawDataRepository _repository;
    private readonly ILogger<ValidateRawDataCommandHandler> _logger;

    public ValidateRawDataCommandHandler(
        IRawDataRepository repository,
        ILogger<ValidateRawDataCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<ValidationResultDto>> Handle(ValidateRawDataCommand request, CancellationToken cancellationToken)
    {
        _logger.LogDebug("Validating raw data for DivSeq {DivSeq}, SpecSysId {SpecSysId}",
            request.DivSeq, request.SpecSysId);

        try
        {
            var dto = new ValidateRawDataDto
            {
                DivSeq = request.DivSeq,
                SpecSysId = request.SpecSysId,
                WorkDate = request.WorkDate,
                RawDataValue = request.RawDataValue,
                InputQty = request.InputQty,
                DefectQty = request.DefectQty
            };

            var result = await _repository.ValidateRawDataAsync(request.DivSeq, dto, cancellationToken);
            return Result<ValidationResultDto>.Success(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error validating raw data for DivSeq {DivSeq}", request.DivSeq);
            return Result<ValidationResultDto>.Failure("Failed to validate raw data.");
        }
    }
}
