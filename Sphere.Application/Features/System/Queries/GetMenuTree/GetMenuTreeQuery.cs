using MediatR;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.System;

namespace Sphere.Application.Features.System.Queries.GetMenuTree;

/// <summary>
/// Query for getting menu tree structure.
/// </summary>
public class GetMenuTreeQuery : IRequest<Result<MenuTreeResponseDto>>
{
    public string DivSeq { get; set; } = string.Empty;
    public string? ParentMenuId { get; set; }
    public string? MenuType { get; set; }
    public string? IsActive { get; set; }
    public string Language { get; set; } = "ko-KR";
}
