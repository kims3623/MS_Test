using MediatR;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.System;

namespace Sphere.Application.Features.System.Commands.CreateRole;

/// <summary>
/// Command for creating a new role.
/// </summary>
public class CreateRoleCommand : IRequest<Result<CreateRoleResponseDto>>
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
