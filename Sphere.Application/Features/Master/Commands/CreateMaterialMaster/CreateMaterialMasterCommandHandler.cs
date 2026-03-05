using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Master;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Application.Features.Master.Commands.CreateMaterialMaster;

public class CreateMaterialMasterCommandHandler : IRequestHandler<CreateMaterialMasterCommand, Result<MaterialMasterResultDto>>
{
    private readonly IMaterialMasterRepository _repository;
    private readonly ILogger<CreateMaterialMasterCommandHandler> _logger;

    public CreateMaterialMasterCommandHandler(
        IMaterialMasterRepository repository,
        ILogger<CreateMaterialMasterCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<MaterialMasterResultDto>> Handle(CreateMaterialMasterCommand request, CancellationToken cancellationToken)
    {
        _logger.LogDebug("Creating material master: DivSeq {DivSeq}, MtrlId {MtrlId}", request.DivSeq, request.MtrlId);

        try
        {
            var dto = new CreateMaterialMasterDto
            {
                MtrlId = request.MtrlId,
                MtrlName = request.MtrlName,
                MtrlClassId = request.MtrlClassId,
                MtrlClassGroupId = request.MtrlClassGroupId,
                VendorId = request.VendorId,
                Unit = request.Unit,
                SpecId = request.SpecId,
                UseYn = request.UseYn,
                Description = request.Description
            };

            var result = await _repository.CreateMaterialMasterAsync(request.DivSeq, dto, request.UserId, cancellationToken);

            if (!result.Success)
            {
                return Result<MaterialMasterResultDto>.Failure(result.Message);
            }

            return Result<MaterialMasterResultDto>.Success(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating material master: DivSeq {DivSeq}, MtrlId {MtrlId}", request.DivSeq, request.MtrlId);
            return Result<MaterialMasterResultDto>.Failure("Failed to create material master.");
        }
    }
}
