using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.System;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Application.Features.System.Queries.GetUserGroups;

/// <summary>
/// Handler for GetUserGroupsQuery.
/// </summary>
public class GetUserGroupsQueryHandler : IRequestHandler<GetUserGroupsQuery, Result<UserGroupListResponseDto>>
{
    private readonly ISystemRepository _systemRepository;
    private readonly ILogger<GetUserGroupsQueryHandler> _logger;

    public GetUserGroupsQueryHandler(
        ISystemRepository systemRepository,
        ILogger<GetUserGroupsQueryHandler> logger)
    {
        _systemRepository = systemRepository;
        _logger = logger;
    }

    public async Task<Result<UserGroupListResponseDto>> Handle(
        GetUserGroupsQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var filter = new UserGroupListFilterDto
            {
                DivSeq = request.DivSeq,
                GroupId = request.GroupId,
                GroupName = request.GroupName,
                GroupType = request.GroupType,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };

            var response = await _systemRepository.GetUserGroupListAsync(filter, cancellationToken);

            _logger.LogInformation("User groups retrieved: {Count} items", response.TotalCount);
            return Result<UserGroupListResponseDto>.Success(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving user groups");
            return Result<UserGroupListResponseDto>.Failure($"Error retrieving user groups: {ex.Message}");
        }
    }
}
