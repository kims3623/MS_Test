using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Master;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Application.Features.Master.Queries.GetCopyableSpecs;

/// <summary>
/// Handler for GetCopyableSpecsQuery.
/// Retrieves specifications that can be copied, filtered by vendor and material class.
/// </summary>
public class GetCopyableSpecsQueryHandler : IRequestHandler<GetCopyableSpecsQuery, Result<SpecMasterListDto>>
{
    private readonly ISpecMasterRepository _repository;
    private readonly ILogger<GetCopyableSpecsQueryHandler> _logger;

    public GetCopyableSpecsQueryHandler(
        ISpecMasterRepository repository,
        ILogger<GetCopyableSpecsQueryHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<SpecMasterListDto>> Handle(
        GetCopyableSpecsQuery request,
        CancellationToken cancellationToken)
    {
        _logger.LogDebug(
            "Getting copyable specs for DivSeq {DivSeq}, VendorId {VendorId}",
            request.DivSeq,
            request.VendorId);

        try
        {
            // Build filter for copyable specs
            var filter = new SpecMasterFilterDto
            {
                VendorId = request.VendorId,
                MtrlClassId = request.MtrlClassId,
                UseYn = request.ActiveOnly ? "Y" : null,
                SearchText = request.SearchText
            };

            var items = (await _repository.GetSpecMasterListAsync(
                request.DivSeq,
                filter,
                cancellationToken)).ToList();

            _logger.LogInformation(
                "Found {Count} copyable specs for DivSeq {DivSeq}",
                items.Count,
                request.DivSeq);

            return Result<SpecMasterListDto>.Success(new SpecMasterListDto
            {
                Items = items,
                TotalCount = items.Count
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting copyable specs");
            return Result<SpecMasterListDto>.Failure("Failed to retrieve copyable specifications.");
        }
    }
}
