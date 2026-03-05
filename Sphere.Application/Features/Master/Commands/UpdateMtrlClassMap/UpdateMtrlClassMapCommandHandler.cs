using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Master;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Application.Features.Master.Commands.UpdateMtrlClassMap;

public class UpdateMtrlClassMapCommandHandler : IRequestHandler<UpdateMtrlClassMapCommand, Result<MtrlClassMapResultDto>>
{
    private readonly IMtrlClassMapRepository _repository;
    private readonly ILogger<UpdateMtrlClassMapCommandHandler> _logger;

    public UpdateMtrlClassMapCommandHandler(
        IMtrlClassMapRepository repository,
        ILogger<UpdateMtrlClassMapCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<MtrlClassMapResultDto>> Handle(
        UpdateMtrlClassMapCommand request,
        CancellationToken cancellationToken)
    {
        _logger.LogDebug(
            "Updating MtrlClassMap: DivSeq {DivSeq}, TreeId {TreeId}, UseYn {UseYn}",
            request.DivSeq, request.TreeId, request.UseYn);

        try
        {
            var dto = new UpdateMtrlClassMapDto
            {
                TreeId = request.TreeId,
                UseYn = request.UseYn
            };

            var result = await _repository.UpdateAsync(request.DivSeq, dto, request.UserId, cancellationToken);

            if (!result.Success)
            {
                return Result<MtrlClassMapResultDto>.Failure(result.Message);
            }

            return Result<MtrlClassMapResultDto>.Success(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Error updating MtrlClassMap: DivSeq {DivSeq}, TreeId {TreeId}",
                request.DivSeq, request.TreeId);
            return Result<MtrlClassMapResultDto>.Failure("Failed to update MtrlClassMap.");
        }
    }
}
