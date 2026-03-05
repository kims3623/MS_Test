using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Data;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Application.Features.Data.Queries.GetRawDataByGroup;

/// <summary>
/// Handler for GetRawDataByGroupQuery.
/// </summary>
public class GetRawDataByGroupQueryHandler : IRequestHandler<GetRawDataByGroupQuery, Result<RawDataGroupDto>>
{
    private readonly IRawDataRepository _repository;
    private readonly ILogger<GetRawDataByGroupQueryHandler> _logger;

    public GetRawDataByGroupQueryHandler(
        IRawDataRepository repository,
        ILogger<GetRawDataByGroupQueryHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<RawDataGroupDto>> Handle(GetRawDataByGroupQuery request, CancellationToken cancellationToken)
    {
        _logger.LogDebug("Getting raw data by group for DivSeq {DivSeq}, GroupSpecSysId {GroupSpecSysId}",
            request.DivSeq, request.GroupSpecSysId);

        try
        {
            var result = await _repository.GetRawDataByGroupAsync(request.DivSeq, request.GroupSpecSysId, cancellationToken);
            return Result<RawDataGroupDto>.Success(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting raw data by group for DivSeq {DivSeq}", request.DivSeq);
            return Result<RawDataGroupDto>.Failure("Failed to retrieve raw data by group.");
        }
    }
}
