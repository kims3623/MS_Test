using MediatR;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.System;

namespace Sphere.Application.Features.System.Commands.CreateMenu;

/// <summary>
/// Command for creating a new menu.
/// </summary>
public class CreateMenuCommand : IRequest<Result<CreateMenuResponseDto>>
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
