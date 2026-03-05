using MediatR;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.System;

namespace Sphere.Application.Features.System.Queries.GetUserList;

/// <summary>
/// Query to get user list with filter and pagination.
/// </summary>
public record GetUserListQuery : IRequest<Result<UserListResponseDto>>
{
    public string DivSeq { get; init; } = string.Empty;
    public string? UserId { get; init; }
    public string? UserName { get; init; }
    public string? DeptCode { get; init; }
    public string? RoleCode { get; init; }
    public string? UserType { get; init; }
    public string? IsActive { get; init; }
    public string? VendorId { get; init; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 20;
}
