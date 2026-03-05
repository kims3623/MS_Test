namespace Sphere.Application.DTOs.System;

#region User List DTOs

/// <summary>
/// User list filter DTO for search operations.
/// </summary>
public class UserListFilterDto
{
    public string DivSeq { get; set; } = string.Empty;
    public string? UserId { get; set; }
    public string? UserName { get; set; }
    public string? DeptCode { get; set; }
    public string? RoleCode { get; set; }
    public string? UserType { get; set; }
    public string? IsActive { get; set; }
    public string? VendorId { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}

/// <summary>
/// User list item DTO for grid display.
/// </summary>
public class UserListItemDto
{
    public string UserId { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string UserNameE { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string DeptCode { get; set; } = string.Empty;
    public string DeptName { get; set; } = string.Empty;
    public string RoleCode { get; set; } = string.Empty;
    public string RoleName { get; set; } = string.Empty;
    public string UserType { get; set; } = string.Empty;
    public string UserTypeName { get; set; } = string.Empty;
    public string VendorId { get; set; } = string.Empty;
    public string VendorName { get; set; } = string.Empty;
    public string IsActive { get; set; } = string.Empty;
    public string LastLoginDate { get; set; } = string.Empty;
    public string CreateDate { get; set; } = string.Empty;
    public string CreateUserId { get; set; } = string.Empty;
    public string UpdateDate { get; set; } = string.Empty;
    public string UpdateUserId { get; set; } = string.Empty;
}

/// <summary>
/// User list response with pagination.
/// </summary>
public class UserListResponseDto
{
    public List<UserListItemDto> Items { get; set; } = new();
    public int TotalCount { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages => PageSize > 0 ? (int)Math.Ceiling((double)TotalCount / PageSize) : 0;
}

#endregion

#region User Detail DTOs

/// <summary>
/// User detail DTO.
/// </summary>
public class UserDetailDto
{
    public string UserId { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string UserNameE { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Mobile { get; set; } = string.Empty;
    public string DeptCode { get; set; } = string.Empty;
    public string DeptName { get; set; } = string.Empty;
    public string PositionCode { get; set; } = string.Empty;
    public string PositionName { get; set; } = string.Empty;
    public string RoleCode { get; set; } = string.Empty;
    public string RoleName { get; set; } = string.Empty;
    public string UserType { get; set; } = string.Empty;
    public string UserTypeName { get; set; } = string.Empty;
    public string VendorId { get; set; } = string.Empty;
    public string VendorName { get; set; } = string.Empty;
    public string Language { get; set; } = "ko-KR";
    public string Timezone { get; set; } = "Asia/Seoul";
    public string IsActive { get; set; } = string.Empty;
    public string IsLocked { get; set; } = string.Empty;
    public int FailedLoginCount { get; set; }
    public string LastLoginDate { get; set; } = string.Empty;
    public string LastPasswordChangeDate { get; set; } = string.Empty;
    public string PasswordExpireDate { get; set; } = string.Empty;
    public string CreateDate { get; set; } = string.Empty;
    public string CreateUserId { get; set; } = string.Empty;
    public string UpdateDate { get; set; } = string.Empty;
    public string UpdateUserId { get; set; } = string.Empty;
    public List<UserGroupItemDto> Groups { get; set; } = new();
    public List<string> Permissions { get; set; } = new();
}

/// <summary>
/// User group item DTO.
/// </summary>
public class UserGroupItemDto
{
    public string GroupId { get; set; } = string.Empty;
    public string GroupName { get; set; } = string.Empty;
    public string GroupType { get; set; } = string.Empty;
    public string IsDefault { get; set; } = string.Empty;
}

#endregion

#region User CRUD DTOs

/// <summary>
/// Create user request DTO.
/// </summary>
public class CreateUserRequestDto
{
    public string DivSeq { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string UserNameE { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Mobile { get; set; } = string.Empty;
    public string DeptCode { get; set; } = string.Empty;
    public string PositionCode { get; set; } = string.Empty;
    public string RoleCode { get; set; } = string.Empty;
    public string UserType { get; set; } = string.Empty;
    public string? VendorId { get; set; }
    public string Language { get; set; } = "ko-KR";
    public string Timezone { get; set; } = "Asia/Seoul";
    public string InitialPassword { get; set; } = string.Empty;
    public List<string>? GroupIds { get; set; }
    public string CreateUserId { get; set; } = string.Empty;
}

/// <summary>
/// Create user response DTO.
/// </summary>
public class CreateUserResponseDto
{
    public string Result { get; set; } = string.Empty;
    public string ResultMessage { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
}

/// <summary>
/// Update user request DTO.
/// </summary>
public class UpdateUserRequestDto
{
    public string DivSeq { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string? UserName { get; set; }
    public string? UserNameE { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Mobile { get; set; }
    public string? DeptCode { get; set; }
    public string? PositionCode { get; set; }
    public string? RoleCode { get; set; }
    public string? UserType { get; set; }
    public string? VendorId { get; set; }
    public string? Language { get; set; }
    public string? Timezone { get; set; }
    public string? IsActive { get; set; }
    public List<string>? GroupIds { get; set; }
    public string UpdateUserId { get; set; } = string.Empty;
}

/// <summary>
/// Update user response DTO.
/// </summary>
public class UpdateUserResponseDto
{
    public string Result { get; set; } = string.Empty;
    public string ResultMessage { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
}

/// <summary>
/// Delete user request DTO.
/// </summary>
public class DeleteUserRequestDto
{
    public string DivSeq { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string DeleteUserId { get; set; } = string.Empty;
}

/// <summary>
/// Delete user response DTO.
/// </summary>
public class DeleteUserResponseDto
{
    public string Result { get; set; } = string.Empty;
    public string ResultMessage { get; set; } = string.Empty;
}

/// <summary>
/// Reset password request DTO.
/// </summary>
public class ResetUserPasswordRequestDto
{
    public string DivSeq { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string NewPassword { get; set; } = string.Empty;
    public string UpdateUserId { get; set; } = string.Empty;
}

/// <summary>
/// Unlock user request DTO.
/// </summary>
public class UnlockUserRequestDto
{
    public string DivSeq { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string UpdateUserId { get; set; } = string.Empty;
}

#endregion

#region User Group DTOs

/// <summary>
/// User group list filter DTO.
/// </summary>
public class UserGroupListFilterDto
{
    public string DivSeq { get; set; } = string.Empty;
    public string? GroupId { get; set; }
    public string? GroupName { get; set; }
    public string? GroupType { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}

/// <summary>
/// User group list item DTO.
/// </summary>
public class UserGroupListItemDto
{
    public string GroupId { get; set; } = string.Empty;
    public string GroupName { get; set; } = string.Empty;
    public string GroupNameE { get; set; } = string.Empty;
    public string GroupType { get; set; } = string.Empty;
    public string GroupTypeName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int MemberCount { get; set; }
    public string IsActive { get; set; } = string.Empty;
    public string CreateDate { get; set; } = string.Empty;
    public string UpdateDate { get; set; } = string.Empty;
}

/// <summary>
/// User group list response with pagination.
/// </summary>
public class UserGroupListResponseDto
{
    public List<UserGroupListItemDto> Items { get; set; } = new();
    public int TotalCount { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}

/// <summary>
/// User group detail DTO.
/// </summary>
public class UserGroupDetailDto
{
    public string GroupId { get; set; } = string.Empty;
    public string GroupName { get; set; } = string.Empty;
    public string GroupNameE { get; set; } = string.Empty;
    public string GroupType { get; set; } = string.Empty;
    public string GroupTypeName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string IsActive { get; set; } = string.Empty;
    public List<UserListItemDto> Members { get; set; } = new();
    public string CreateDate { get; set; } = string.Empty;
    public string CreateUserId { get; set; } = string.Empty;
    public string UpdateDate { get; set; } = string.Empty;
    public string UpdateUserId { get; set; } = string.Empty;
}

/// <summary>
/// Update user group members request DTO.
/// </summary>
public class UpdateUserGroupMembersRequestDto
{
    public string DivSeq { get; set; } = string.Empty;
    public string GroupId { get; set; } = string.Empty;
    public List<string> UserIds { get; set; } = new();
    public string UpdateUserId { get; set; } = string.Empty;
}

#endregion
