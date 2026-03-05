using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Interfaces;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Auth;

namespace Sphere.Application.Features.Auth.Queries.GetCurrentUser;

/// <summary>
/// Handler for GetCurrentUserQuery.
/// </summary>
public class GetCurrentUserQueryHandler : IRequestHandler<GetCurrentUserQuery, Result<UserProfileDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly ILogger<GetCurrentUserQueryHandler> _logger;

    public GetCurrentUserQueryHandler(
        IApplicationDbContext context,
        ILogger<GetCurrentUserQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Result<UserProfileDto>> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
    {
        _logger.LogDebug("Getting current user profile for {UserId}", request.UserId);

        var user = await _context.UserInfos
            .Where(u => u.DivSeq == request.DivSeq && u.UserId == request.UserId && u.UseYn == "Y")
            .FirstOrDefaultAsync(cancellationToken);

        if (user is null)
        {
            _logger.LogWarning("User {UserId} not found", request.UserId);
            return Result<UserProfileDto>.Failure("User not found.");
        }

        // Load permissions (fallback to empty list if SPC_ROLE_PERMISSION table doesn't exist)
        var permissions = new List<string>();
        try
        {
            permissions = await _context.RolePermissions
                .Where(rp => rp.DivSeq == request.DivSeq && rp.RoleCode == user.RoleId && rp.UseYn == "Y")
                .Select(rp => rp.PermissionCode)
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "RolePermissions table not available, returning empty permissions");
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
