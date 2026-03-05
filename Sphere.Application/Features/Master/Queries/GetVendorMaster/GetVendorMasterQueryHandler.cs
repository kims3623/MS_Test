using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Master;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Application.Features.Master.Queries.GetVendorMaster;

public class GetVendorMasterQueryHandler : IRequestHandler<GetVendorMasterQuery, Result<VendorMasterListDto>>
{
    private readonly IVendorMasterRepository _repository;
    private readonly ILogger<GetVendorMasterQueryHandler> _logger;

    public GetVendorMasterQueryHandler(
        IVendorMasterRepository repository,
        ILogger<GetVendorMasterQueryHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<VendorMasterListDto>> Handle(GetVendorMasterQuery request, CancellationToken cancellationToken)
    {
        _logger.LogDebug("Getting vendor master list for DivSeq {DivSeq}", request.DivSeq);

        try
        {
            var filter = new VendorMasterFilterDto
            {
                VendorType = request.VendorType,
                UseYn = request.UseYn,
                ApprovalStatus = request.ApprovalStatus,
                SearchText = request.SearchText
            };

            var items = await _repository.GetVendorMasterListAsync(request.DivSeq, filter, cancellationToken);
            var itemList = items.ToList();

            return Result<VendorMasterListDto>.Success(new VendorMasterListDto
            {
                Items = itemList,
                TotalCount = itemList.Count
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting vendor master list for DivSeq {DivSeq}", request.DivSeq);
            return Result<VendorMasterListDto>.Failure("Failed to retrieve vendor master list.");
        }
    }
}
