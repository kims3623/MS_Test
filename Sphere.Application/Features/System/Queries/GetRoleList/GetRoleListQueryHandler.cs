using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.System;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Application.Features.System.Queries.GetRoleList;

/// <summary>
/// Handler for GetRoleListQuery.
/// </summary>
public class GetRoleListQueryHandler : IRequestHandler<GetRoleListQuery, Result<RoleListResponseDto>>
{
    private readonly ISystemRepository _systemRepository;
    private readonly ILogger<GetRoleListQueryHandler> _logger;

    public GetRoleListQueryHandler(
        ISystemRepository systemRepository,
        ILogger<GetRoleListQueryHandler> logger)
    {
        _systemRepository = systemRepository;
        _logger = logger;
    }

    public async Task<Result<RoleListResponseDto>> Handle(
        GetRoleListQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var filter = new RoleListFilterDto
            {
                DivSeq = request.DivSeq,
                RoleCode = request.RoleCode,
                RoleName = request.RoleName,
                RoleType = request.RoleType,
                IsActive = request.IsActive,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };

            var response = await _systemRepository.GetRoleListAsync(filter, cancellationToken);

            _logger.LogInformation("Role list retrieved: {Count} items", response.TotalCount);
            return Result<RoleListResponseDto>.Success(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving role list");
            return Result<RoleListResponseDto>.Failure($"Error retrieving role list: {ex.Message}");
        }
    }
}
