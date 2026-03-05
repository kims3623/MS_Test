using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Data;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Application.Features.Data.Commands.DeleteRawData;

/// <summary>
/// Handler for DeleteRawDataCommand.
/// </summary>
public class DeleteRawDataCommandHandler : IRequestHandler<DeleteRawDataCommand, Result<RawDataOperationResultDto>>
{
    private readonly IRawDataRepository _repository;
    private readonly ILogger<DeleteRawDataCommandHandler> _logger;

    public DeleteRawDataCommandHandler(
        IRawDataRepository repository,
        ILogger<DeleteRawDataCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<RawDataOperationResultDto>> Handle(DeleteRawDataCommand request, CancellationToken cancellationToken)
    {
        _logger.LogDebug("Deleting raw data ID {RawDataId} for DivSeq {DivSeq}",
            request.RawDataId, request.DivSeq);

        try
        {
            var dto = new RawDataDeleteDto
            {
                RawDataId = request.RawDataId,
                SpecSysId = request.SpecSysId,
                WorkDate = request.WorkDate,
                Shift = request.Shift,
                DeleteUserId = request.UserId
            };

            var result = await _repository.DeleteRawDataAsync(request.DivSeq, dto, cancellationToken);

            if (!result.Success)
            {
                return Result<RawDataOperationResultDto>.Failure(result.Message);
            }

            return Result<RawDataOperationResultDto>.Success(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting raw data ID {RawDataId}", request.RawDataId);
            return Result<RawDataOperationResultDto>.Failure("Failed to delete raw data.");
        }
    }
}
