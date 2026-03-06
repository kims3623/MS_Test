using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Auth;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Application.Features.Users.Queries.GetUserById;

/// <summary>
/// Handler for GetUserByIdQuery.
/// </summary>
public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Result<UserProfileDto>>
{
    private readonly IAuthRepository _authRepository;
    private readonly ILogger<GetUserByIdQueryHandler> _logger;

    public GetUserByIdQueryHandler(
        IAuthRepository authRepository,
        ILogger<GetUserByIdQueryHandler> logger)
    {
        _authRepository = authRepository;
        _logger = logger;
    }

    public async Task<Result<UserProfileDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Fetching user profile for {UserId}", request.UserId);

        var user = await _authRepository.GetUserForAuthAsync(request.UserId, request.DivSeq, cancellationToken);

        if (user is null || user.UseYn != "Y")
        {
            _logger.LogWarning("User {UserId} not found", request.UserId);
            return Result<UserProfileDto>.Failure("User not found.");
        }

        var permissions = (await _authRepository.GetRolePermissionsAsync(
            request.DivSeq, user.RoleId ?? string.Empty, cancellationToken)).ToList();

        _logger.LogInformation("Loaded {PermissionCount} permissions for user {UserId}", permissions.Count, request.UserId);

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
