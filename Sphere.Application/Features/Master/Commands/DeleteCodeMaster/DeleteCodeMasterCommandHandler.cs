using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Master;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Application.Features.Master.Commands.DeleteCodeMaster;

/// <summary>
/// Handler for DeleteCodeMasterCommand.
/// </summary>
public class DeleteCodeMasterCommandHandler : IRequestHandler<DeleteCodeMasterCommand, Result<CodeMasterResultDto>>
{
    private readonly ICodeMasterRepository _repository;
    private readonly ILogger<DeleteCodeMasterCommandHandler> _logger;

    public DeleteCodeMasterCommandHandler(
        ICodeMasterRepository repository,
        ILogger<DeleteCodeMasterCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<CodeMasterResultDto>> Handle(DeleteCodeMasterCommand request, CancellationToken cancellationToken)
    {
        _logger.LogDebug("Deleting code master: DivSeq {DivSeq}, CodeClassId {CodeClassId}, CodeId {CodeId}",
            request.DivSeq, request.CodeClassId, request.CodeId);

        try
        {
            var result = await _repository.DeleteCodeMasterAsync(
                request.DivSeq,
                request.CodeClassId,
                request.CodeId,
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
            _logger.LogError(ex, "Error deleting code master: DivSeq {DivSeq}, CodeId {CodeId}",
                request.DivSeq, request.CodeId);
            return Result<CodeMasterResultDto>.Failure("Failed to delete code master.");
        }
    }
}
