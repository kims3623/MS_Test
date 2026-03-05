using Microsoft.EntityFrameworkCore;
using Sphere.Domain.Entities.Approval;
using Sphere.Domain.Entities.Auth;

namespace Sphere.Application.Common.Interfaces;

/// <summary>
/// Application database context interface
/// </summary>
public interface IApplicationDbContext
{
    // Auth entities
    DbSet<UserInfo> UserInfos { get; }
    DbSet<UserSession> UserSessions { get; }
    DbSet<Authority> Authorities { get; }
    DbSet<AuthorityFilter> AuthorityFilters { get; }
    DbSet<RolePermission> RolePermissions { get; }
    DbSet<VendorAccountRequest> VendorAccountRequests { get; }

    // Approval entities
    DbSet<Approval> Approvals { get; }
    DbSet<ApprovalHistory> ApprovalHistories { get; }
    DbSet<ApprovalAttachment> ApprovalAttachments { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
