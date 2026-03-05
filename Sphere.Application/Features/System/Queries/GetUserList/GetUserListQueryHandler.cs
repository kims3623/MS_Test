using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.System;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Application.Features.System.Queries.GetUserList;

/// <summary>
/// Handler for GetUserListQuery.
/// </summary>
public class GetUserListQueryHandler : IRequestHandler<GetUserListQuery, Result<UserListResponseDto>>
{
    private readonly ISystemRepository _systemRepository;
    private readonly ILogger<GetUserListQueryHandler> _logger;

    public GetUserListQueryHandler(
        ISystemRepository systemRepository,
        ILogger<GetUserListQueryHandler> logger)
    {
        _systemRepository = systemRepository;
        _logger = logger;
    }

    public async Task<Result<UserListResponseDto>> Handle(GetUserListQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Fetching user list for DivSeq={DivSeq}, Page={Page}",
            request.DivSeq, request.PageNumber);

        try
        {
            var filter = new UserListFilterDto
            {
                DivSeq = request.DivSeq,
                UserId = request.UserId,
                UserName = request.UserName,
                DeptCode = request.DeptCode,
                RoleCode = request.RoleCode,
                UserType = request.UserType,
                IsActive = request.IsActive,
                VendorId = request.VendorId,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };

            var result = await _systemRepository.GetUserListAsync(filter, cancellationToken);

            _logger.LogInformation("Found {Count} users", result.TotalCount);

            return Result<UserListResponseDto>.Success(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching user list");
            return Result<UserListResponseDto>.Failure($"사용자 목록 조회 중 오류가 발생했습니다: {ex.Message}");
        }
    }
}
