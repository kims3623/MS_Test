using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Interfaces;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Auth;

namespace Sphere.Application.Features.Users.Queries.GetUserById;

/// <summary>
/// Handler for GetUserByIdQuery.
/// </summary>
public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Result<UserProfileDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly ILogger<GetUserByIdQueryHandler> _logger;

    public GetUserByIdQueryHandler(
        IApplicationDbContext context,
        ILogger<GetUserByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Result<UserProfileDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Fetching user profile for {UserId}", request.UserId);

        var user = await _context.UserInfos
            .Where(u => u.DivSeq == request.DivSeq && u.UserId == request.UserId && u.UseYn == "Y")
            .FirstOrDefaultAsync(cancellationToken);

        if (user is null)
        {
            _logger.LogWarning("User {UserId} not found", request.UserId);
            return Result<UserProfileDto>.Failure("User not found.");
        }

        // Load permissions for the user's role
        var permissions = await _context.RolePermissions
            .Where(rp => rp.RoleCode == user.RoleId && rp.GrantedYn == "Y")
            .Select(rp => rp.PermissionCode)
            .Distinct()
            .ToListAsync(cancellationToken);

        _logger.LogInformation("Loaded {PermissionCount} permissions for user {UserId}", permissions.Count, request.UserId);

        var profile = new UserProfileDto
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
        };

        return Result<UserProfileDto>.Success(profile);
    }
}
