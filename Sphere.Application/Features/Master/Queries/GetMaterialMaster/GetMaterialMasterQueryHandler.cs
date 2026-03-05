using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Master;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Application.Features.Master.Queries.GetMaterialMaster;

public class GetMaterialMasterQueryHandler : IRequestHandler<GetMaterialMasterQuery, Result<MaterialMasterListDto>>
{
    private readonly IMaterialMasterRepository _repository;
    private readonly ILogger<GetMaterialMasterQueryHandler> _logger;

    public GetMaterialMasterQueryHandler(
        IMaterialMasterRepository repository,
        ILogger<GetMaterialMasterQueryHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<MaterialMasterListDto>> Handle(GetMaterialMasterQuery request, CancellationToken cancellationToken)
    {
        _logger.LogDebug("Getting material master list for DivSeq {DivSeq}", request.DivSeq);

        try
        {
            var filter = new MaterialMasterFilterDto
            {
                MtrlClassId = request.MtrlClassId,
                VendorId = request.VendorId,
                UseYn = request.UseYn
            };

            var items = await _repository.GetMaterialMasterListAsync(request.DivSeq, filter, cancellationToken);
            var itemList = items.ToList();

            return Result<MaterialMasterListDto>.Success(new MaterialMasterListDto
            {
                Items = itemList,
                TotalCount = itemList.Count
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting material master list for DivSeq {DivSeq}", request.DivSeq);
            return Result<MaterialMasterListDto>.Failure("Failed to retrieve material master list.");
        }
    }
}
