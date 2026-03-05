using MediatR;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.System;

namespace Sphere.Application.Features.System.Commands.DeleteRole;

/// <summary>
/// Command for deleting a role.
/// </summary>
public class DeleteRoleCommand : IRequest<Result<DeleteRoleResponseDto>>
{
    public string DivSeq { get; set; } = string.Empty;
    public string RoleCode { get; set; } = string.Empty;
    public string DeleteUserId { get; set; } = string.Empty;
}
