namespace Sphere.Application.DTOs.System;

#region Division DTOs

/// <summary>
/// Division list filter DTO.
/// </summary>
public class DivisionListFilterDto
{
    public string? DivSeq { get; set; }
    public string? DivName { get; set; }
    public string? IsActive { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}

/// <summary>
/// Division list item DTO.
/// </summary>
public class DivisionListItemDto
{
    public string DivSeq { get; set; } = string.Empty;
    public string DivCode { get; set; } = string.Empty;
    public string DivName { get; set; } = string.Empty;
    public string DivNameE { get; set; } = string.Empty;
    public string DivType { get; set; } = string.Empty;
    public string DivTypeName { get; set; } = string.Empty;
    public string ParentDivSeq { get; set; } = string.Empty;
    public string ParentDivName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int UserCount { get; set; }
    public string IsActive { get; set; } = string.Empty;
    public int SortOrder { get; set; }
    public string CreateDate { get; set; } = string.Empty;
    public string UpdateDate { get; set; } = string.Empty;
}

/// <summary>
/// Division list response DTO.
/// </summary>
public class DivisionListResponseDto
{
    public List<DivisionListItemDto> Items { get; set; } = new();
    public int TotalCount { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}

/// <summary>
/// Division tree item DTO.
/// </summary>
public class DivisionTreeItemDto
{
    public string DivSeq { get; set; } = string.Empty;
    public string DivCode { get; set; } = string.Empty;
    public string DivName { get; set; } = string.Empty;
    public string DivNameE { get; set; } = string.Empty;
    public string DivType { get; set; } = string.Empty;
    public string ParentDivSeq { get; set; } = string.Empty;
    public int Level { get; set; }
    public int SortOrder { get; set; }
    public string IsActive { get; set; } = string.Empty;
    public List<DivisionTreeItemDto> Children { get; set; } = new();
}

/// <summary>
/// Division tree response DTO.
/// </summary>
public class DivisionTreeResponseDto
{
    public List<DivisionTreeItemDto> Items { get; set; } = new();
}

/// <summary>
/// Division detail DTO.
/// </summary>
public class DivisionDetailDto
{
    public string DivSeq { get; set; } = string.Empty;
    public string DivCode { get; set; } = string.Empty;
    public string DivName { get; set; } = string.Empty;
    public string DivNameE { get; set; } = string.Empty;
    public string DivType { get; set; } = string.Empty;
    public string DivTypeName { get; set; } = string.Empty;
    public string ParentDivSeq { get; set; } = string.Empty;
    public string ParentDivName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Fax { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int UserCount { get; set; }
    public int SortOrder { get; set; }
    public string IsActive { get; set; } = string.Empty;
    public List<DivisionUserDto> Users { get; set; } = new();
    public string CreateDate { get; set; } = string.Empty;
    public string CreateUserId { get; set; } = string.Empty;
    public string UpdateDate { get; set; } = string.Empty;
    public string UpdateUserId { get; set; } = string.Empty;
}

/// <summary>
/// Division user DTO.
/// </summary>
public class DivisionUserDto
{
    public string UserId { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string DeptName { get; set; } = string.Empty;
    public string PositionName { get; set; } = string.Empty;
    public string RoleName { get; set; } = string.Empty;
}

/// <summary>
/// Create division request DTO.
/// </summary>
public class CreateDivisionRequestDto
{
    public string DivCode { get; set; } = string.Empty;
    public string DivName { get; set; } = string.Empty;
    public string DivNameE { get; set; } = string.Empty;
    public string DivType { get; set; } = string.Empty;
    public string? ParentDivSeq { get; set; }
    public string? Description { get; set; }
    public string? Address { get; set; }
    public string? Phone { get; set; }
    public string? Fax { get; set; }
    public string? Email { get; set; }
    public int SortOrder { get; set; }
    public string CreateUserId { get; set; } = string.Empty;
}

/// <summary>
/// Create division response DTO.
/// </summary>
public class CreateDivisionResponseDto
{
    public string Result { get; set; } = string.Empty;
    public string ResultMessage { get; set; } = string.Empty;
    public string DivSeq { get; set; } = string.Empty;
}

/// <summary>
/// Update division request DTO.
/// </summary>
public class UpdateDivisionRequestDto
{
    public string DivSeq { get; set; } = string.Empty;
    public string? DivCode { get; set; }
    public string? DivName { get; set; }
    public string? DivNameE { get; set; }
    public string? DivType { get; set; }
    public string? ParentDivSeq { get; set; }
    public string? Description { get; set; }
    public string? Address { get; set; }
    public string? Phone { get; set; }
    public string? Fax { get; set; }
    public string? Email { get; set; }
    public int? SortOrder { get; set; }
    public string? IsActive { get; set; }
    public string UpdateUserId { get; set; } = string.Empty;
}

/// <summary>
/// Update division response DTO.
/// </summary>
public class UpdateDivisionResponseDto
{
    public string Result { get; set; } = string.Empty;
    public string ResultMessage { get; set; } = string.Empty;
    public string DivSeq { get; set; } = string.Empty;
}

/// <summary>
/// Delete division request DTO.
/// </summary>
public class DeleteDivisionRequestDto
{
    public string DivSeq { get; set; } = string.Empty;
    public string DeleteUserId { get; set; } = string.Empty;
}

/// <summary>
/// Delete division response DTO.
/// </summary>
public class DeleteDivisionResponseDto
{
    public string Result { get; set; } = string.Empty;
    public string ResultMessage { get; set; } = string.Empty;
}

#endregion
