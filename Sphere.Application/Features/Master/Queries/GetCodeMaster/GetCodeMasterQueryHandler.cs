using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Master;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Application.Features.Master.Queries.GetCodeMaster;

/// <summary>
/// Handler for GetCodeMasterQuery.
/// </summary>
public class GetCodeMasterQueryHandler : IRequestHandler<GetCodeMasterQuery, Result<CodeMasterListDto>>
{
    private readonly ICodeMasterRepository _repository;
    private readonly ILogger<GetCodeMasterQueryHandler> _logger;

    public GetCodeMasterQueryHandler(
        ICodeMasterRepository repository,
        ILogger<GetCodeMasterQueryHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<CodeMasterListDto>> Handle(GetCodeMasterQuery request, CancellationToken cancellationToken)
    {
        _logger.LogDebug("Getting code master list for DivSeq {DivSeq}, CodeClassId {CodeClassId}",
            request.DivSeq, request.CodeClassId);

        try
        {
            var filter = new CodeMasterFilterDto
            {
                CodeClassId = request.CodeClassId,
                UseYn = request.UseYn,
                SearchText = request.SearchText
            };

            var items = await _repository.GetCodeMasterListAsync(request.DivSeq, filter, cancellationToken);
            var itemList = items.ToList();

            var result = new CodeMasterListDto
            {
                Items = itemList,
                TotalCount = itemList.Count
            };

            return Result<CodeMasterListDto>.Success(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting code master list for DivSeq {DivSeq}", request.DivSeq);
            return Result<CodeMasterListDto>.Failure("Failed to retrieve code master list.");
        }
    }
}
