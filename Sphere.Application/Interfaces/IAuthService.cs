using Sphere.Application.DTOs.Auth;

namespace Sphere.Application.Interfaces;

/// <summary>
/// Authentication service interface for user authentication and session management.
/// </summary>
public interface IAuthService
{
    /// <summary>
    /// Authenticates a user with credentials and returns login user information.
    /// </summary>
    /// <param name="userId">User ID</param>
    /// <param name="password">Plain text password</param>
    /// <param name="divSeq">Division sequence</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Login user info if authentication succeeds</returns>
    /// <exception cref="UnauthorizedAccessException">Thrown when authentication fails</exception>
    Task<LoginUserInfoDto> AuthenticateAsync(
        string userId,
        string password,
        string divSeq,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the currently logged-in user information from the current context.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Current user info</returns>
    /// <exception cref="UnauthorizedAccessException">Thrown when no user is logged in</exception>
    Task<UserInfoDto> GetCurrentUserAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Validates a JWT token and checks if it is still valid.
    /// </summary>
    /// <param name="token">JWT access token</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if token is valid, false otherwise</returns>
    Task<bool> ValidateTokenAsync(
        string token,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Generates a JWT access token for the authenticated user.
    /// </summary>
    /// <param name="userInfo">Login user information</param>
    /// <returns>JWT access token string</returns>
    string GenerateToken(LoginUserInfoDto userInfo);

    /// <summary>
    /// Generates a refresh token for the user and stores it for later validation.
    /// </summary>
    /// <param name="userId">User ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Refresh token string</returns>
    Task<string> GenerateRefreshTokenAsync(
        string userId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Changes the user's password after validating the current password.
    /// </summary>
    /// <param name="userId">User ID</param>
    /// <param name="currentPassword">Current plain text password</param>
    /// <param name="newPassword">New plain text password</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <exception cref="UnauthorizedAccessException">Thrown when current password is invalid</exception>
    /// <exception cref="ArgumentException">Thrown when new password doesn't meet requirements</exception>
    Task ChangePasswordAsync(
        string userId,
        string currentPassword,
        string newPassword,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Logs out a user by invalidating their session and revoking tokens.
    /// </summary>
    /// <param name="userId">User ID</param>
    /// <param name="sessionId">Session ID to invalidate</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task LogoutAsync(
        string userId,
        string sessionId,
        CancellationToken cancellationToken = default);
}
