namespace Sphere.Application.DTOs.System;

#region Menu List DTOs

/// <summary>
/// Menu list filter DTO.
/// </summary>
public class MenuListFilterDto
{
    public string DivSeq { get; set; } = string.Empty;
    public string? ParentMenuId { get; set; }
    public string? MenuType { get; set; }
    public string? IsActive { get; set; }
    public string Language { get; set; } = "ko-KR";
}

/// <summary>
/// Menu tree item DTO.
/// </summary>
public class MenuTreeItemDto
{
    public string MenuId { get; set; } = string.Empty;
    public string MenuName { get; set; } = string.Empty;
    public string MenuNameE { get; set; } = string.Empty;
    public string ParentMenuId { get; set; } = string.Empty;
    public string MenuType { get; set; } = string.Empty;
    public string MenuTypeName { get; set; } = string.Empty;
    public string IconClass { get; set; } = string.Empty;
    public string ScreenId { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public int SortOrder { get; set; }
    public int Level { get; set; }
    public string IsActive { get; set; } = string.Empty;
    public string IsVisible { get; set; } = string.Empty;
    public List<MenuTreeItemDto> Children { get; set; } = new();
}

/// <summary>
/// Menu list response (flat).
/// </summary>
public class MenuListResponseDto
{
    public List<MenuListItemDto> Items { get; set; } = new();
    public int TotalCount { get; set; }
}

/// <summary>
/// Menu list item DTO (flat).
/// </summary>
public class MenuListItemDto
{
    public string MenuId { get; set; } = string.Empty;
    public string MenuName { get; set; } = string.Empty;
    public string MenuNameE { get; set; } = string.Empty;
    public string ParentMenuId { get; set; } = string.Empty;
    public string ParentMenuName { get; set; } = string.Empty;
    public string MenuType { get; set; } = string.Empty;
    public string MenuTypeName { get; set; } = string.Empty;
    public string IconClass { get; set; } = string.Empty;
    public string ScreenId { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public int SortOrder { get; set; }
    public int Level { get; set; }
    public string IsActive { get; set; } = string.Empty;
    public string IsVisible { get; set; } = string.Empty;
    public string CreateDate { get; set; } = string.Empty;
    public string UpdateDate { get; set; } = string.Empty;
}

/// <summary>
/// Menu tree response.
/// </summary>
public class MenuTreeResponseDto
{
    public List<MenuTreeItemDto> Items { get; set; } = new();
}

#endregion

#region Menu Detail DTOs

/// <summary>
/// Menu detail DTO.
/// </summary>
public class MenuDetailDto
{
    public string MenuId { get; set; } = string.Empty;
    public string MenuName { get; set; } = string.Empty;
    public string MenuNameE { get; set; } = string.Empty;
    public string ParentMenuId { get; set; } = string.Empty;
    public string ParentMenuName { get; set; } = string.Empty;
    public string MenuType { get; set; } = string.Empty;
    public string MenuTypeName { get; set; } = string.Empty;
    public string IconClass { get; set; } = string.Empty;
    public string ScreenId { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int SortOrder { get; set; }
    public int Level { get; set; }
    public string IsActive { get; set; } = string.Empty;
    public string IsVisible { get; set; } = string.Empty;
    public string OpenNewWindow { get; set; } = string.Empty;
    public List<MenuRolePermissionDto> RolePermissions { get; set; } = new();
    public string CreateDate { get; set; } = string.Empty;
    public string CreateUserId { get; set; } = string.Empty;
    public string UpdateDate { get; set; } = string.Empty;
    public string UpdateUserId { get; set; } = string.Empty;
}

/// <summary>
/// Menu role permission DTO.
/// </summary>
public class MenuRolePermissionDto
{
    public string RoleCode { get; set; } = string.Empty;
    public string RoleName { get; set; } = string.Empty;
    public string CanView { get; set; } = string.Empty;
    public string CanCreate { get; set; } = string.Empty;
    public string CanUpdate { get; set; } = string.Empty;
    public string CanDelete { get; set; } = string.Empty;
    public string CanExport { get; set; } = string.Empty;
}

#endregion

#region Menu CRUD DTOs

/// <summary>
/// Create menu request DTO.
/// </summary>
public class CreateMenuRequestDto
{
    public string DivSeq { get; set; } = string.Empty;
    public string MenuId { get; set; } = string.Empty;
    public string MenuName { get; set; } = string.Empty;
    public string MenuNameE { get; set; } = string.Empty;
    public string? ParentMenuId { get; set; }
    public string MenuType { get; set; } = string.Empty;
    public string? IconClass { get; set; }
    public string? ScreenId { get; set; }
    public string? Url { get; set; }
    public string? Description { get; set; }
    public int SortOrder { get; set; }
    public string IsVisible { get; set; } = "Y";
    public string OpenNewWindow { get; set; } = "N";
    public string CreateUserId { get; set; } = string.Empty;
}

/// <summary>
/// Create menu response DTO.
/// </summary>
public class CreateMenuResponseDto
{
    public string Result { get; set; } = string.Empty;
    public string ResultMessage { get; set; } = string.Empty;
    public string MenuId { get; set; } = string.Empty;
}

/// <summary>
/// Update menu request DTO.
/// </summary>
public class UpdateMenuRequestDto
{
    public string DivSeq { get; set; } = string.Empty;
    public string MenuId { get; set; } = string.Empty;
    public string? MenuName { get; set; }
    public string? MenuNameE { get; set; }
    public string? ParentMenuId { get; set; }
    public string? MenuType { get; set; }
    public string? IconClass { get; set; }
    public string? ScreenId { get; set; }
    public string? Url { get; set; }
    public string? Description { get; set; }
    public int? SortOrder { get; set; }
    public string? IsActive { get; set; }
    public string? IsVisible { get; set; }
    public string? OpenNewWindow { get; set; }
    public string UpdateUserId { get; set; } = string.Empty;
}

/// <summary>
/// Update menu response DTO.
/// </summary>
public class UpdateMenuResponseDto
{
    public string Result { get; set; } = string.Empty;
    public string ResultMessage { get; set; } = string.Empty;
    public string MenuId { get; set; } = string.Empty;
}

/// <summary>
/// Delete menu request DTO.
/// </summary>
public class DeleteMenuRequestDto
{
    public string DivSeq { get; set; } = string.Empty;
    public string MenuId { get; set; } = string.Empty;
    public string DeleteUserId { get; set; } = string.Empty;
}

/// <summary>
/// Delete menu response DTO.
/// </summary>
public class DeleteMenuResponseDto
{
    public string Result { get; set; } = string.Empty;
    public string ResultMessage { get; set; } = string.Empty;
}

/// <summary>
/// Update menu sort order request DTO.
/// </summary>
public class UpdateMenuSortOrderRequestDto
{
    public string DivSeq { get; set; } = string.Empty;
    public List<MenuSortOrderItemDto> Items { get; set; } = new();
    public string UpdateUserId { get; set; } = string.Empty;
}

/// <summary>
/// Menu sort order item DTO.
/// </summary>
public class MenuSortOrderItemDto
{
    public string MenuId { get; set; } = string.Empty;
    public string? ParentMenuId { get; set; }
    public int SortOrder { get; set; }
}

#endregion

#region User Menu DTOs

/// <summary>
/// User accessible menu DTO.
/// </summary>
public class UserMenuDto
{
    public string MenuId { get; set; } = string.Empty;
    public string MenuName { get; set; } = string.Empty;
    public string ParentMenuId { get; set; } = string.Empty;
    public string MenuType { get; set; } = string.Empty;
    public string IconClass { get; set; } = string.Empty;
    public string ScreenId { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public int SortOrder { get; set; }
    public int Level { get; set; }
    public string CanView { get; set; } = string.Empty;
    public string CanCreate { get; set; } = string.Empty;
    public string CanUpdate { get; set; } = string.Empty;
    public string CanDelete { get; set; } = string.Empty;
    public string CanExport { get; set; } = string.Empty;
    public List<UserMenuDto> Children { get; set; } = new();
}

/// <summary>
/// User menu response DTO.
/// </summary>
public class UserMenuResponseDto
{
    public List<UserMenuDto> Menus { get; set; } = new();
    public List<string> FavoriteMenuIds { get; set; } = new();
}

/// <summary>
/// Update favorite menu request DTO.
/// </summary>
public class UpdateFavoriteMenuRequestDto
{
    public string DivSeq { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public List<string> MenuIds { get; set; } = new();
}

#endregion
