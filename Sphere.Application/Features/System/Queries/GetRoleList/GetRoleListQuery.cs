using MediatR;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.System;

namespace Sphere.Application.Features.System.Queries.GetRoleList;

/// <summary>
/// Query for getting role list with filter and pagination.
/// </summary>
public class GetRoleListQuery : IRequest<Result<RoleListResponseDto>>
{
    public string DivSeq { get; set; } = string.Empty;
    public string? RoleCode { get; set; }
    public string? RoleName { get; set; }
    public string? RoleType { get; set; }
    public string? IsActive { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}
