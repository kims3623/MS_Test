using MediatR;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.System;

namespace Sphere.Application.Features.System.Commands.CreateUser;

/// <summary>
/// Command to create a new user.
/// </summary>
public record CreateUserCommand : IRequest<Result<CreateUserResponseDto>>
{
    public string DivSeq { get; init; } = string.Empty;
    public string UserId { get; init; } = string.Empty;
    public string UserName { get; init; } = string.Empty;
    public string UserNameE { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string Phone { get; init; } = string.Empty;
    public string Mobile { get; init; } = string.Empty;
    public string DeptCode { get; init; } = string.Empty;
    public string PositionCode { get; init; } = string.Empty;
    public string RoleCode { get; init; } = string.Empty;
    public string UserType { get; init; } = string.Empty;
    public string? VendorId { get; init; }
    public string Language { get; init; } = "ko-KR";
    public string Timezone { get; init; } = "Asia/Seoul";
    public string InitialPassword { get; init; } = string.Empty;
    public List<string>? GroupIds { get; init; }
    public string CreateUserId { get; init; } = string.Empty;
}
