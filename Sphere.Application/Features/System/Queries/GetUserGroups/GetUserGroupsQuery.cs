using MediatR;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.System;

namespace Sphere.Application.Features.System.Queries.GetUserGroups;

/// <summary>
/// Query for getting user groups with filter and pagination.
/// </summary>
public class GetUserGroupsQuery : IRequest<Result<UserGroupListResponseDto>>
{
    public string DivSeq { get; set; } = string.Empty;
    public string? GroupId { get; set; }
    public string? GroupName { get; set; }
    public string? GroupType { get; set; }
    public string? IsActive { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}
