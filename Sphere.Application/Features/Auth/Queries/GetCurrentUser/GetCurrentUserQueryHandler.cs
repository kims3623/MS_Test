using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Auth;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Application.Features.Auth.Queries.GetCurrentUser;

/// <summary>
/// Handler for GetCurrentUserQuery.
/// </summary>
public class GetCurrentUserQueryHandler : IRequestHandler<GetCurrentUserQuery, Result<UserProfileDto>>
{
    private readonly IAuthRepository _authRepository;
    private readonly ILogger<GetCurrentUserQueryHandler> _logger;

    public GetCurrentUserQueryHandler(
        IAuthRepository authRepository,
        ILogger<GetCurrentUserQueryHandler> logger)
    {
        _authRepository = authRepository;
        _logger = logger;
    }

    public async Task<Result<UserProfileDto>> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
    {
        _logger.LogDebug("Getting current user profile for {UserId}", request.UserId);

        var user = await _authRepository.GetUserForAuthAsync(request.UserId, request.DivSeq, cancellationToken);

        if (user is null || user.UseYn != "Y")
        {
            _logger.LogWarning("User {UserId} not found", request.UserId);
            return Result<UserProfileDto>.Failure("User not found.");
        }

        var permissions = new List<string>();
        try
        {
            permissions = (await _authRepository.GetRolePermissionsAsync(
                request.DivSeq, user.RoleId ?? string.Empty, cancellationToken)).ToList();
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "RolePermissions not available, returning empty permissions");
        }

        return Result<UserProfileDto>.Success(new UserProfileDto
        {
            UserId = user.UserId,
            UserName = user.UserName,
            Email = user.Email,
            DivSeq = user.DivSeq,
            DeptCode = user.DeptId,
            DeptName = user.DeptName,
            RoleCode = user.RoleId,
            RoleName = user.RoleName,
            Language = user.Locale,
            Timezone = user.Timezone,
            Permissions = permissions
        });
    }
}
