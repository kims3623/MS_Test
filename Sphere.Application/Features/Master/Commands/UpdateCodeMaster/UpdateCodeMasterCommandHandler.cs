using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Master;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Application.Features.Master.Commands.UpdateCodeMaster;

/// <summary>
/// Handler for UpdateCodeMasterCommand.
/// </summary>
public class UpdateCodeMasterCommandHandler : IRequestHandler<UpdateCodeMasterCommand, Result<CodeMasterResultDto>>
{
    private readonly ICodeMasterRepository _repository;
    private readonly ILogger<UpdateCodeMasterCommandHandler> _logger;

    public UpdateCodeMasterCommandHandler(
        ICodeMasterRepository repository,
        ILogger<UpdateCodeMasterCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<CodeMasterResultDto>> Handle(UpdateCodeMasterCommand request, CancellationToken cancellationToken)
    {
        _logger.LogDebug("Updating code master: DivSeq {DivSeq}, CodeClassId {CodeClassId}, CodeId {CodeId}",
            request.DivSeq, request.CodeClassId, request.CodeId);

        try
        {
            var dto = new UpdateCodeMasterDto
            {
                CodeAlias = request.CodeAlias,
                CodeNameK = request.CodeNameK,
                CodeNameE = request.CodeNameE,
                CodeNameC = request.CodeNameC,
                CodeNameV = request.CodeNameV,
                DisplaySeq = request.DisplaySeq,
                CodeOpt = request.CodeOpt,
                UseYn = request.UseYn,
                Description = request.Description
            };

            var result = await _repository.UpdateCodeMasterAsync(
                request.DivSeq,
                request.CodeClassId,
                request.CodeId,
                dto,
                request.UserId,
                cancellationToken);

            if (!result.Success)
            {
                return Result<CodeMasterResultDto>.Failure(result.Message);
            }

            return Result<CodeMasterResultDto>.Success(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating code master: DivSeq {DivSeq}, CodeId {CodeId}",
                request.DivSeq, request.CodeId);
            return Result<CodeMasterResultDto>.Failure("Failed to update code master.");
        }
    }
}
