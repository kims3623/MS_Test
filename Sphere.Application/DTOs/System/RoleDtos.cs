namespace Sphere.Application.DTOs.System;

#region Role List DTOs

/// <summary>
/// Role list filter DTO.
/// </summary>
public class RoleListFilterDto
{
    public string DivSeq { get; set; } = string.Empty;
    public string? RoleCode { get; set; }
    public string? RoleName { get; set; }
    public string? RoleType { get; set; }
    public string? IsActive { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}

/// <summary>
/// Role list item DTO.
/// </summary>
public class RoleListItemDto
{
    public string RoleCode { get; set; } = string.Empty;
    public string RoleName { get; set; } = string.Empty;
    public string RoleNameE { get; set; } = string.Empty;
    public string RoleType { get; set; } = string.Empty;
    public string RoleTypeName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Level { get; set; }
    public int UserCount { get; set; }
    public string IsActive { get; set; } = string.Empty;
    public string IsSystem { get; set; } = string.Empty;
    public string CreateDate { get; set; } = string.Empty;
    public string UpdateDate { get; set; } = string.Empty;
}

/// <summary>
/// Role list response with pagination.
/// </summary>
public class RoleListResponseDto
{
    public List<RoleListItemDto> Items { get; set; } = new();
    public int TotalCount { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}

#endregion

#region Role Detail DTOs

/// <summary>
/// Role detail DTO.
/// </summary>
public class RoleDetailDto
{
    public string RoleCode { get; set; } = string.Empty;
    public string RoleName { get; set; } = string.Empty;
    public string RoleNameE { get; set; } = string.Empty;
    public string RoleType { get; set; } = string.Empty;
    public string RoleTypeName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Level { get; set; }
    public int UserCount { get; set; }
    public string IsActive { get; set; } = string.Empty;
    public string IsSystem { get; set; } = string.Empty;
    public List<RolePermissionItemDto> Permissions { get; set; } = new();
    public List<RoleMenuItemDto> Menus { get; set; } = new();
    public string CreateDate { get; set; } = string.Empty;
    public string CreateUserId { get; set; } = string.Empty;
    public string UpdateDate { get; set; } = string.Empty;
    public string UpdateUserId { get; set; } = string.Empty;
}

/// <summary>
/// Role permission item DTO.
/// </summary>
public class RolePermissionItemDto
{
    public string PermissionCode { get; set; } = string.Empty;
    public string PermissionName { get; set; } = string.Empty;
    public string PermissionType { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string IsGranted { get; set; } = string.Empty;
}

/// <summary>
/// Role menu item DTO.
/// </summary>
public class RoleMenuItemDto
{
    public string MenuId { get; set; } = string.Empty;
    public string MenuName { get; set; } = string.Empty;
    public string ParentMenuId { get; set; } = string.Empty;
    public string MenuType { get; set; } = string.Empty;
    public string IsGranted { get; set; } = string.Empty;
    public string CanView { get; set; } = string.Empty;
    public string CanCreate { get; set; } = string.Empty;
    public string CanUpdate { get; set; } = string.Empty;
    public string CanDelete { get; set; } = string.Empty;
    public string CanExport { get; set; } = string.Empty;
}

#endregion

#region Role CRUD DTOs

/// <summary>
/// Create role request DTO.
/// </summary>
public class CreateRoleRequestDto
{
    public string DivSeq { get; set; } = string.Empty;
    public string RoleCode { get; set; } = string.Empty;
    public string RoleName { get; set; } = string.Empty;
    public string RoleNameE { get; set; } = string.Empty;
    public string RoleType { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int Level { get; set; }
    public List<string>? PermissionCodes { get; set; }
    public List<RoleMenuPermissionDto>? MenuPermissions { get; set; }
    public string CreateUserId { get; set; } = string.Empty;
}

/// <summary>
/// Role menu permission DTO for create/update.
/// </summary>
public class RoleMenuPermissionDto
{
    public string MenuId { get; set; } = string.Empty;
    public string CanView { get; set; } = "N";
    public string CanCreate { get; set; } = "N";
    public string CanUpdate { get; set; } = "N";
    public string CanDelete { get; set; } = "N";
    public string CanExport { get; set; } = "N";
}

/// <summary>
/// Create role response DTO.
/// </summary>
public class CreateRoleResponseDto
{
    public string Result { get; set; } = string.Empty;
    public string ResultMessage { get; set; } = string.Empty;
    public string RoleCode { get; set; } = string.Empty;
}

/// <summary>
/// Update role request DTO.
/// </summary>
public class UpdateRoleRequestDto
{
    public string DivSeq { get; set; } = string.Empty;
    public string RoleCode { get; set; } = string.Empty;
    public string? RoleName { get; set; }
    public string? RoleNameE { get; set; }
    public string? RoleType { get; set; }
    public string? Description { get; set; }
    public int? Level { get; set; }
    public string? IsActive { get; set; }
    public List<string>? PermissionCodes { get; set; }
    public List<RoleMenuPermissionDto>? MenuPermissions { get; set; }
    public string UpdateUserId { get; set; } = string.Empty;
}

/// <summary>
/// Update role response DTO.
/// </summary>
public class UpdateRoleResponseDto
{
    public string Result { get; set; } = string.Empty;
    public string ResultMessage { get; set; } = string.Empty;
    public string RoleCode { get; set; } = string.Empty;
}

/// <summary>
/// Delete role request DTO.
/// </summary>
public class DeleteRoleRequestDto
{
    public string DivSeq { get; set; } = string.Empty;
    public string RoleCode { get; set; } = string.Empty;
    public string DeleteUserId { get; set; } = string.Empty;
}

/// <summary>
/// Delete role response DTO.
/// </summary>
public class DeleteRoleResponseDto
{
    public string Result { get; set; } = string.Empty;
    public string ResultMessage { get; set; } = string.Empty;
}

#endregion

#region Permission DTOs

/// <summary>
/// Permission filter query DTO.
/// </summary>
public class PermissionFilterQueryDto
{
    public string DivSeq { get; set; } = string.Empty;
    public string? UserId { get; set; }
    public string? RoleCode { get; set; }
    public string? ScreenId { get; set; }
    public string? VendorId { get; set; }
    public string? MtrlClassId { get; set; }
}

/// <summary>
/// Permission filter result DTO.
/// </summary>
public class PermissionFilterResultDto
{
    public string ScreenId { get; set; } = string.Empty;
    public string ScreenName { get; set; } = string.Empty;
    public List<PermissionFilterItemDto> Filters { get; set; } = new();
}

/// <summary>
/// Permission filter item DTO.
/// </summary>
public class PermissionFilterItemDto
{
    public string FilterType { get; set; } = string.Empty;
    public string FilterValue { get; set; } = string.Empty;
    public string FilterName { get; set; } = string.Empty;
    public string IsInclude { get; set; } = string.Empty;
}

/// <summary>
/// All permissions list DTO.
/// </summary>
public class AllPermissionsDto
{
    public List<PermissionCategoryDto> Categories { get; set; } = new();
}

/// <summary>
/// Permission category DTO.
/// </summary>
public class PermissionCategoryDto
{
    public string CategoryCode { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public List<PermissionItemDto> Permissions { get; set; } = new();
}

/// <summary>
/// Permission item DTO.
/// </summary>
public class PermissionItemDto
{
    public string PermissionCode { get; set; } = string.Empty;
    public string PermissionName { get; set; } = string.Empty;
    public string PermissionNameE { get; set; } = string.Empty;
    public string PermissionType { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string IsSystem { get; set; } = string.Empty;
}

#endregion
