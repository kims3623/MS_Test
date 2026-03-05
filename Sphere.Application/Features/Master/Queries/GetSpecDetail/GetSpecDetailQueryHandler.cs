using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Models;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Application.Features.Master.Queries.GetSpecDetail;

/// <summary>
/// Handler for GetSpecDetailQuery.
/// </summary>
public class GetSpecDetailQueryHandler : IRequestHandler<GetSpecDetailQuery, Result<SpecDetailListDto>>
{
    private readonly ISpecMasterRepository _repository;
    private readonly ILogger<GetSpecDetailQueryHandler> _logger;

    public GetSpecDetailQueryHandler(
        ISpecMasterRepository repository,
        ILogger<GetSpecDetailQueryHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<SpecDetailListDto>> Handle(GetSpecDetailQuery request, CancellationToken cancellationToken)
    {
        _logger.LogDebug("Getting spec detail: DivSeq {DivSeq}, SpecSysId {SpecSysId}",
            request.DivSeq, request.SpecSysId);

        try
        {
            var items = (await _repository.GetSpecDetailAsync(request.DivSeq, request.SpecSysId, cancellationToken)).ToList();

            return Result<SpecDetailListDto>.Success(new SpecDetailListDto
            {
                Items = items,
                TotalCount = items.Count
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting spec detail: DivSeq {DivSeq}, SpecSysId {SpecSysId}",
                request.DivSeq, request.SpecSysId);
            return Result<SpecDetailListDto>.Failure("Failed to retrieve spec detail.");
        }
    }
}
