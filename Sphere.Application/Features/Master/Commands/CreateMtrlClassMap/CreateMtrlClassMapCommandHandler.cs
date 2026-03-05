using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Master;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Application.Features.Master.Commands.CreateMtrlClassMap;

public class CreateMtrlClassMapCommandHandler : IRequestHandler<CreateMtrlClassMapCommand, Result<MtrlClassMapResultDto>>
{
    private readonly IMtrlClassMapRepository _repository;
    private readonly ILogger<CreateMtrlClassMapCommandHandler> _logger;

    public CreateMtrlClassMapCommandHandler(
        IMtrlClassMapRepository repository,
        ILogger<CreateMtrlClassMapCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<MtrlClassMapResultDto>> Handle(
        CreateMtrlClassMapCommand request,
        CancellationToken cancellationToken)
    {
        _logger.LogDebug(
            "Creating MtrlClassMap: DivSeq {DivSeq}, MtrlClassId {MtrlClassId}, ClassType {ClassType}",
            request.DivSeq, request.MtrlClassId, request.ClassType);

        try
        {
            var dto = new CreateMtrlClassMapDto
            {
                ParentTreeId = request.ParentTreeId,
                MtrlClassId = request.MtrlClassId,
                ClassType = request.ClassType
            };

            var result = await _repository.CreateAsync(request.DivSeq, dto, request.UserId, cancellationToken);

            if (!result.Success)
            {
                return Result<MtrlClassMapResultDto>.Failure(result.Message);
            }

            return Result<MtrlClassMapResultDto>.Success(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Error creating MtrlClassMap: DivSeq {DivSeq}, MtrlClassId {MtrlClassId}",
                request.DivSeq, request.MtrlClassId);
            return Result<MtrlClassMapResultDto>.Failure("Failed to create MtrlClassMap.");
        }
    }
}
