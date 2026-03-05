namespace Sphere.Application.Common.Interfaces;

/// <summary>
/// Service to get current authenticated user information
/// </summary>
public interface ICurrentUserService
{
    string? UserId { get; }
    string? UserName { get; }
    bool IsAuthenticated { get; }
}
