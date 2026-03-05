using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Data;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Application.Features.Data.Queries.GetRawDataList;

/// <summary>
/// Handler for GetRawDataListQuery.
/// </summary>
public class GetRawDataListQueryHandler : IRequestHandler<GetRawDataListQuery, Result<RawDataListDto>>
{
    private readonly IRawDataRepository _repository;
    private readonly ILogger<GetRawDataListQueryHandler> _logger;

    public GetRawDataListQueryHandler(
        IRawDataRepository repository,
        ILogger<GetRawDataListQueryHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<RawDataListDto>> Handle(GetRawDataListQuery request, CancellationToken cancellationToken)
    {
        _logger.LogDebug("Getting raw data list for DivSeq {DivSeq}, SpecSysId {SpecSysId}",
            request.DivSeq, request.SpecSysId);

        try
        {
            var filter = new RawDataFilterDto
            {
                VendorId = request.VendorId,
                MtrlClassId = request.MtrlClassId,
                ProjectId = request.ProjectId,
                ActProdId = request.ActProdId,
                StepId = request.StepId,
                ItemId = request.ItemId,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Shift = request.Shift,
                SpecSysId = request.SpecSysId,
                ApprovalYn = request.ApprovalYn,
                SearchText = request.SearchText,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };

            var result = await _repository.GetRawDataAsync(request.DivSeq, filter, cancellationToken);
            return Result<RawDataListDto>.Success(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting raw data list for DivSeq {DivSeq}", request.DivSeq);
            return Result<RawDataListDto>.Failure("Failed to retrieve raw data list.");
        }
    }
}
