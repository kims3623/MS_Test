using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Master;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Application.Features.Master.Commands.CreateSpecMaster;

/// <summary>
/// Handler for CreateSpecMasterCommand.
/// </summary>
public class CreateSpecMasterCommandHandler : IRequestHandler<CreateSpecMasterCommand, Result<SpecMasterResultDto>>
{
    private readonly ISpecMasterRepository _repository;
    private readonly ILogger<CreateSpecMasterCommandHandler> _logger;

    public CreateSpecMasterCommandHandler(
        ISpecMasterRepository repository,
        ILogger<CreateSpecMasterCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<SpecMasterResultDto>> Handle(CreateSpecMasterCommand request, CancellationToken cancellationToken)
    {
        _logger.LogDebug("Creating spec master: DivSeq {DivSeq}, SpecId {SpecId}",
            request.DivSeq, request.SpecId);

        try
        {
            var dto = new CreateSpecMasterDto
            {
                SpecId = request.SpecId,
                SpecName = request.SpecName,
                SpecVersion = request.SpecVersion,
                VendorId = request.VendorId,
                MtrlClassId = request.MtrlClassId,
                UseYn = request.UseYn,
                Description = request.Description
            };

            var result = await _repository.CreateSpecMasterAsync(request.DivSeq, dto, request.UserId, cancellationToken);

            if (!result.Success)
            {
                return Result<SpecMasterResultDto>.Failure(result.Message);
            }

            return Result<SpecMasterResultDto>.Success(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating spec master: DivSeq {DivSeq}, SpecId {SpecId}",
                request.DivSeq, request.SpecId);
            return Result<SpecMasterResultDto>.Failure("Failed to create spec master.");
        }
    }
}
