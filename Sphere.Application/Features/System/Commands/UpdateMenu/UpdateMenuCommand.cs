using MediatR;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.System;

namespace Sphere.Application.Features.System.Commands.UpdateMenu;

/// <summary>
/// Command for updating an existing menu.
/// </summary>
public class UpdateMenuCommand : IRequest<Result<UpdateMenuResponseDto>>
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
