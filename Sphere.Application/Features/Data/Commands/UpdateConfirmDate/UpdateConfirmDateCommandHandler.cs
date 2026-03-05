using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Data;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Application.Features.Data.Commands.UpdateConfirmDate;

/// <summary>
/// Handler for UpdateConfirmDateCommand.
/// </summary>
public class UpdateConfirmDateCommandHandler : IRequestHandler<UpdateConfirmDateCommand, Result<RawDataOperationResultDto>>
{
    private readonly IRawDataRepository _repository;
    private readonly ILogger<UpdateConfirmDateCommandHandler> _logger;

    public UpdateConfirmDateCommandHandler(
        IRawDataRepository repository,
        ILogger<UpdateConfirmDateCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<RawDataOperationResultDto>> Handle(UpdateConfirmDateCommand request, CancellationToken cancellationToken)
    {
        _logger.LogDebug("Updating confirm date for DivSeq {DivSeq}, SpecSysId {SpecSysId}",
            request.DivSeq, request.SpecSysId);

        try
        {
            var dto = new UpdateConfirmDateDto
            {
                DivSeq = request.DivSeq,
                SpecSysId = request.SpecSysId,
                MtrlClassId = request.MtrlClassId,
                VendorId = request.VendorId,
                StatTypeId = request.StatTypeId,
                ConfirmDate = request.ConfirmDate,
                UpdateUserId = request.UserId
            };

            var result = await _repository.UpdateConfirmDateAsync(request.DivSeq, dto, cancellationToken);

            if (!result.Success)
            {
                return Result<RawDataOperationResultDto>.Failure(result.Message);
            }

            return Result<RawDataOperationResultDto>.Success(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating confirm date for DivSeq {DivSeq}", request.DivSeq);
            return Result<RawDataOperationResultDto>.Failure("Failed to update confirm date.");
        }
    }
}
