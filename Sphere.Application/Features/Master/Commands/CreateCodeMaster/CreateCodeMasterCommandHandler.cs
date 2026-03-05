using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Master;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Application.Features.Master.Commands.CreateCodeMaster;

/// <summary>
/// Handler for CreateCodeMasterCommand.
/// </summary>
public class CreateCodeMasterCommandHandler : IRequestHandler<CreateCodeMasterCommand, Result<CodeMasterResultDto>>
{
    private readonly ICodeMasterRepository _repository;
    private readonly ILogger<CreateCodeMasterCommandHandler> _logger;

    public CreateCodeMasterCommandHandler(
        ICodeMasterRepository repository,
        ILogger<CreateCodeMasterCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<CodeMasterResultDto>> Handle(CreateCodeMasterCommand request, CancellationToken cancellationToken)
    {
        _logger.LogDebug("Creating code master: DivSeq {DivSeq}, CodeClassId {CodeClassId}, CodeId {CodeId}",
            request.DivSeq, request.CodeClassId, request.CodeId);

        try
        {
            var dto = new CreateCodeMasterDto
            {
                CodeId = request.CodeId,
                CodeClassId = request.CodeClassId,
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

            var result = await _repository.CreateCodeMasterAsync(request.DivSeq, dto, request.UserId, cancellationToken);

            if (!result.Success)
            {
                return Result<CodeMasterResultDto>.Failure(result.Message);
            }

            return Result<CodeMasterResultDto>.Success(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating code master: DivSeq {DivSeq}, CodeId {CodeId}",
                request.DivSeq, request.CodeId);
            return Result<CodeMasterResultDto>.Failure("Failed to create code master.");
        }
    }
}
