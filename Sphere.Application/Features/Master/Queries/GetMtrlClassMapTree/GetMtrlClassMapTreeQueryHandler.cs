using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Master;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Application.Features.Master.Queries.GetMtrlClassMapTree;

public class GetMtrlClassMapTreeQueryHandler : IRequestHandler<GetMtrlClassMapTreeQuery, Result<List<MtrlClassMapTreeDto>>>
{
    private readonly IMtrlClassMapRepository _repository;
    private readonly ILogger<GetMtrlClassMapTreeQueryHandler> _logger;

    public GetMtrlClassMapTreeQueryHandler(
        IMtrlClassMapRepository repository,
        ILogger<GetMtrlClassMapTreeQueryHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<List<MtrlClassMapTreeDto>>> Handle(
        GetMtrlClassMapTreeQuery request,
        CancellationToken cancellationToken)
    {
        _logger.LogDebug("Getting MtrlClassMap tree for DivSeq {DivSeq}", request.DivSeq);

        try
        {
            var items = await _repository.GetTreeAsync(request.DivSeq, cancellationToken);
            return Result<List<MtrlClassMapTreeDto>>.Success(items.ToList());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting MtrlClassMap tree for DivSeq {DivSeq}", request.DivSeq);
            return Result<List<MtrlClassMapTreeDto>>.Failure("Failed to retrieve MtrlClassMap tree.");
        }
    }
}
