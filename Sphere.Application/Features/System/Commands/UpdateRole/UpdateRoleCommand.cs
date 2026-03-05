using MediatR;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.System;

namespace Sphere.Application.Features.System.Commands.UpdateRole;

/// <summary>
/// Command for updating an existing role.
/// </summary>
public class UpdateRoleCommand : IRequest<Result<UpdateRoleResponseDto>>
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
