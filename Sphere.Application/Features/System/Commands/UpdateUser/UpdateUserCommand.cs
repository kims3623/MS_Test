using MediatR;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.System;

namespace Sphere.Application.Features.System.Commands.UpdateUser;

/// <summary>
/// Command to update an existing user.
/// </summary>
public record UpdateUserCommand : IRequest<Result<UpdateUserResponseDto>>
{
    public string DivSeq { get; init; } = string.Empty;
    public string UserId { get; init; } = string.Empty;
    public string? UserName { get; init; }
    public string? UserNameE { get; init; }
    public string? Email { get; init; }
    public string? Phone { get; init; }
    public string? Mobile { get; init; }
    public string? DeptCode { get; init; }
    public string? PositionCode { get; init; }
    public string? RoleCode { get; init; }
    public string? UserType { get; init; }
    public string? VendorId { get; init; }
    public string? Language { get; init; }
    public string? Timezone { get; init; }
    public string? IsActive { get; init; }
    public List<string>? GroupIds { get; init; }
    public string UpdateUserId { get; init; } = string.Empty;
}
