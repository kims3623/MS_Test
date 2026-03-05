using Sphere.Application.DTOs.System;

namespace Sphere.Application.Interfaces.Repositories;

/// <summary>
/// Repository interface for system management operations.
/// </summary>
public interface ISystemRepository
{
    #region User Operations

    /// <summary>
    /// Gets user list with filter and pagination.
    /// </summary>
    Task<UserListResponseDto> GetUserListAsync(UserListFilterDto filter, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets user detail by ID.
    /// </summary>
    Task<UserDetailDto?> GetUserByIdAsync(string divSeq, string userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new user.
    /// </summary>
    Task<CreateUserResponseDto> CreateUserAsync(CreateUserRequestDto request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing user.
    /// </summary>
    Task<UpdateUserResponseDto> UpdateUserAsync(UpdateUserRequestDto request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a user.
    /// </summary>
    Task<DeleteUserResponseDto> DeleteUserAsync(DeleteUserRequestDto request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Resets user password.
    /// </summary>
    Task<UpdateUserResponseDto> ResetUserPasswordAsync(ResetUserPasswordRequestDto request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Unlocks a locked user.
    /// </summary>
    Task<UpdateUserResponseDto> UnlockUserAsync(UnlockUserRequestDto request, CancellationToken cancellationToken = default);

    #endregion

    #region User Group Operations

    /// <summary>
    /// Gets user group list.
    /// </summary>
    Task<UserGroupListResponseDto> GetUserGroupListAsync(UserGroupListFilterDto filter, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets user group detail.
    /// </summary>
    Task<UserGroupDetailDto?> GetUserGroupByIdAsync(string divSeq, string groupId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates user group members.
    /// </summary>
    Task<UpdateUserResponseDto> UpdateUserGroupMembersAsync(UpdateUserGroupMembersRequestDto request, CancellationToken cancellationToken = default);

    #endregion

    #region Role Operations

    /// <summary>
    /// Gets role list with filter and pagination.
    /// </summary>
    Task<RoleListResponseDto> GetRoleListAsync(RoleListFilterDto filter, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets role detail.
    /// </summary>
    Task<RoleDetailDto?> GetRoleByIdAsync(string divSeq, string roleCode, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new role.
    /// </summary>
    Task<CreateRoleResponseDto> CreateRoleAsync(CreateRoleRequestDto request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing role.
    /// </summary>
    Task<UpdateRoleResponseDto> UpdateRoleAsync(UpdateRoleRequestDto request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a role.
    /// </summary>
    Task<DeleteRoleResponseDto> DeleteRoleAsync(DeleteRoleRequestDto request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all permissions.
    /// </summary>
    Task<AllPermissionsDto> GetAllPermissionsAsync(string divSeq, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets permission filter result.
    /// </summary>
    Task<PermissionFilterResultDto> GetPermissionFilterAsync(PermissionFilterQueryDto query, CancellationToken cancellationToken = default);

    #endregion

    #region Menu Operations

    /// <summary>
    /// Gets menu list (flat).
    /// </summary>
    Task<MenuListResponseDto> GetMenuListAsync(MenuListFilterDto filter, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets menu tree.
    /// </summary>
    Task<MenuTreeResponseDto> GetMenuTreeAsync(MenuListFilterDto filter, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets menu detail.
    /// </summary>
    Task<MenuDetailDto?> GetMenuByIdAsync(string divSeq, string menuId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets user menus with permissions.
    /// </summary>
    Task<UserMenuResponseDto> GetUserMenusAsync(string divSeq, string userId, string language, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new menu.
    /// </summary>
    Task<CreateMenuResponseDto> CreateMenuAsync(CreateMenuRequestDto request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing menu.
    /// </summary>
    Task<UpdateMenuResponseDto> UpdateMenuAsync(UpdateMenuRequestDto request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a menu.
    /// </summary>
    Task<DeleteMenuResponseDto> DeleteMenuAsync(DeleteMenuRequestDto request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates menu sort order.
    /// </summary>
    Task<UpdateMenuResponseDto> UpdateMenuSortOrderAsync(UpdateMenuSortOrderRequestDto request, CancellationToken cancellationToken = default);

    #endregion

    #region Security Policy Operations

    /// <summary>
    /// Gets security policy.
    /// </summary>
    Task<SecurityPolicyDto> GetSecurityPolicyAsync(string divSeq, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates security policy.
    /// </summary>
    Task<UpdateSecurityPolicyResponseDto> UpdateSecurityPolicyAsync(UpdateSecurityPolicyRequestDto request, CancellationToken cancellationToken = default);

    #endregion

    #region Audit Log Operations

    /// <summary>
    /// Gets audit log list.
    /// </summary>
    Task<AuditLogResponseDto> GetAuditLogsAsync(AuditLogFilterDto filter, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets audit log detail.
    /// </summary>
    Task<AuditLogDetailDto?> GetAuditLogByIdAsync(string divSeq, long logId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Exports audit logs.
    /// </summary>
    Task<ExportAuditLogResponseDto> ExportAuditLogsAsync(ExportAuditLogRequestDto request, CancellationToken cancellationToken = default);

    #endregion

    #region Login History Operations

    /// <summary>
    /// Gets login history.
    /// </summary>
    Task<LoginHistoryResponseDto> GetLoginHistoryAsync(LoginHistoryFilterDto filter, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets login statistics.
    /// </summary>
    Task<LoginStatisticsDto> GetLoginStatisticsAsync(string divSeq, string? startDate, string? endDate, CancellationToken cancellationToken = default);

    #endregion

    #region Session Operations

    /// <summary>
    /// Gets active sessions.
    /// </summary>
    Task<ActiveSessionResponseDto> GetActiveSessionsAsync(ActiveSessionFilterDto filter, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets session statistics.
    /// </summary>
    Task<SessionStatisticsDto> GetSessionStatisticsAsync(string divSeq, CancellationToken cancellationToken = default);

    /// <summary>
    /// Terminates a session.
    /// </summary>
    Task<TerminateSessionResponseDto> TerminateSessionAsync(TerminateSessionRequestDto request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Terminates all sessions for a user or all users.
    /// </summary>
    Task<TerminateAllSessionsResponseDto> TerminateAllSessionsAsync(TerminateAllSessionsRequestDto request, CancellationToken cancellationToken = default);

    #endregion

    #region System Config Operations

    /// <summary>
    /// Gets system configuration.
    /// </summary>
    Task<SystemConfigDto> GetSystemConfigAsync(string divSeq, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets system config list (key-value).
    /// </summary>
    Task<SystemConfigListResponseDto> GetSystemConfigListAsync(string divSeq, string? configGroup, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates system configuration.
    /// </summary>
    Task<UpdateSystemConfigResponseDto> UpdateSystemConfigAsync(UpdateSystemConfigRequestDto request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets system health status.
    /// </summary>
    Task<SystemHealthDto> GetSystemHealthAsync(string divSeq, CancellationToken cancellationToken = default);

    /// <summary>
    /// Clears cache.
    /// </summary>
    Task<ClearCacheResponseDto> ClearCacheAsync(ClearCacheRequestDto request, CancellationToken cancellationToken = default);

    #endregion

    #region Division Operations

    /// <summary>
    /// Gets division list.
    /// </summary>
    Task<DivisionListResponseDto> GetDivisionListAsync(DivisionListFilterDto filter, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets division tree.
    /// </summary>
    Task<DivisionTreeResponseDto> GetDivisionTreeAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets division detail.
    /// </summary>
    Task<DivisionDetailDto?> GetDivisionByIdAsync(string divSeq, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new division.
    /// </summary>
    Task<CreateDivisionResponseDto> CreateDivisionAsync(CreateDivisionRequestDto request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing division.
    /// </summary>
    Task<UpdateDivisionResponseDto> UpdateDivisionAsync(UpdateDivisionRequestDto request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a division.
    /// </summary>
    Task<DeleteDivisionResponseDto> DeleteDivisionAsync(DeleteDivisionRequestDto request, CancellationToken cancellationToken = default);

    #endregion

    #region OTP Settings Operations

    /// <summary>
    /// Gets OTP settings list.
    /// </summary>
    Task<OTPSettingsResponseDto> GetOTPSettingsAsync(OTPSettingsFilterDto filter, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets OTP history.
    /// </summary>
    Task<OTPHistoryResponseDto> GetOTPHistoryAsync(OTPHistoryFilterDto filter, CancellationToken cancellationToken = default);

    #endregion

    #region Approval Config Operations

    /// <summary>
    /// Gets approval config list.
    /// </summary>
    Task<ApprovalConfigResponseDto> GetApprovalConfigListAsync(ApprovalConfigFilterDto filter, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets approval config detail.
    /// </summary>
    Task<ApprovalConfigDetailDto?> GetApprovalConfigByIdAsync(string divSeq, string configId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates approval config.
    /// </summary>
    Task<UpdateApprovalConfigResponseDto> UpdateApprovalConfigAsync(UpdateApprovalConfigRequestDto request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets approval user relations.
    /// </summary>
    Task<ApprovalUserRelationResponseDto> GetApprovalUserRelationsAsync(ApprovalUserRelationFilterDto filter, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets user approvers.
    /// </summary>
    Task<UserApproversDto> GetUserApproversAsync(string divSeq, string userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates approval user relation.
    /// </summary>
    Task<CreateApprovalUserRelationResponseDto> CreateApprovalUserRelationAsync(CreateApprovalUserRelationRequestDto request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates approval user relation.
    /// </summary>
    Task<UpdateApprovalUserRelationResponseDto> UpdateApprovalUserRelationAsync(UpdateApprovalUserRelationRequestDto request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes approval user relation.
    /// </summary>
    Task<DeleteApprovalUserRelationResponseDto> DeleteApprovalUserRelationAsync(DeleteApprovalUserRelationRequestDto request, CancellationToken cancellationToken = default);

    #endregion

    #region Approval Management Operations

    /// <summary>
    /// Gets pending approvals.
    /// </summary>
    Task<PendingApprovalResponseDto> GetPendingApprovalsAsync(PendingApprovalFilterDto filter, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets approval management detail.
    /// </summary>
    Task<ApprovalManagementDetailDto?> GetApprovalManagementDetailAsync(string divSeq, string approvalId, CancellationToken cancellationToken = default);

    #endregion
}
