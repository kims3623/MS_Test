using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Master;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Application.Features.Master.Queries.GetSpecMaster;

public class GetSpecMasterQueryHandler : IRequestHandler<GetSpecMasterQuery, Result<SpecMasterListDto>>
{
    private readonly ISpecMasterRepository _repository;
    private readonly ILogger<GetSpecMasterQueryHandler> _logger;

    public GetSpecMasterQueryHandler(ISpecMasterRepository repository, ILogger<GetSpecMasterQueryHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<SpecMasterListDto>> Handle(GetSpecMasterQuery request, CancellationToken cancellationToken)
    {
        _logger.LogDebug("Getting spec master list for DivSeq {DivSeq}", request.DivSeq);
        try
        {
            var filter = new SpecMasterFilterDto
            {
                VendorId = request.VendorId, MtrlClassId = request.MtrlClassId,
                Status = request.Status, UseYn = request.UseYn, SearchText = request.SearchText
            };
            var items = (await _repository.GetSpecMasterListAsync(request.DivSeq, filter, cancellationToken)).ToList();
            return Result<SpecMasterListDto>.Success(new SpecMasterListDto { Items = items, TotalCount = items.Count });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting spec master list");
            return Result<SpecMasterListDto>.Failure("Failed to retrieve spec master list.");
        }
    }
}
