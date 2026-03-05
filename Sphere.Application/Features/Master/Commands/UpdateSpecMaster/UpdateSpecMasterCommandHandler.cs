using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Master;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Application.Features.Master.Commands.UpdateSpecMaster;

/// <summary>
/// Handler for UpdateSpecMasterCommand.
/// </summary>
public class UpdateSpecMasterCommandHandler : IRequestHandler<UpdateSpecMasterCommand, Result<SpecMasterResultDto>>
{
    private readonly ISpecMasterRepository _repository;
    private readonly ILogger<UpdateSpecMasterCommandHandler> _logger;

    public UpdateSpecMasterCommandHandler(
        ISpecMasterRepository repository,
        ILogger<UpdateSpecMasterCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<SpecMasterResultDto>> Handle(UpdateSpecMasterCommand request, CancellationToken cancellationToken)
    {
        _logger.LogDebug("Updating spec master: DivSeq {DivSeq}, SpecSysId {SpecSysId}",
            request.DivSeq, request.SpecSysId);

        try
        {
            var dto = new UpdateSpecMasterDto
            {
                SpecName = request.SpecName,
                SpecVersion = request.SpecVersion,
                Status = request.Status,
                UseYn = request.UseYn,
                Description = request.Description
            };

            var result = await _repository.UpdateSpecMasterAsync(request.DivSeq, request.SpecSysId, dto, request.UserId, cancellationToken);

            if (!result.Success)
            {
                return Result<SpecMasterResultDto>.Failure(result.Message);
            }

            return Result<SpecMasterResultDto>.Success(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating spec master: DivSeq {DivSeq}, SpecSysId {SpecSysId}",
                request.DivSeq, request.SpecSysId);
            return Result<SpecMasterResultDto>.Failure("Failed to update spec master.");
        }
    }
}
