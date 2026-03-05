using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.System;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Application.Features.System.Queries.GetDivisionList;

/// <summary>
/// Handler for GetDivisionListQuery.
/// </summary>
public class GetDivisionListQueryHandler : IRequestHandler<GetDivisionListQuery, Result<DivisionListResponseDto>>
{
    private readonly ISystemRepository _systemRepository;
    private readonly ILogger<GetDivisionListQueryHandler> _logger;

    public GetDivisionListQueryHandler(
        ISystemRepository systemRepository,
        ILogger<GetDivisionListQueryHandler> logger)
    {
        _systemRepository = systemRepository;
        _logger = logger;
    }

    public async Task<Result<DivisionListResponseDto>> Handle(GetDivisionListQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Fetching division list, Page={Page}",
            request.PageNumber);

        try
        {
            var filter = new DivisionListFilterDto
            {
                DivSeq = request.DivSeq,
                DivName = request.DivName,
                IsActive = request.IsActive,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };

            var result = await _systemRepository.GetDivisionListAsync(filter, cancellationToken);

            _logger.LogInformation("Found {Count} divisions", result.TotalCount);

            return Result<DivisionListResponseDto>.Success(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching division list");
            return Result<DivisionListResponseDto>.Failure($"사업부 목록 조회 중 오류가 발생했습니다: {ex.Message}");
        }
    }
}
