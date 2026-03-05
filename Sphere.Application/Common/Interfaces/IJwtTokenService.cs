using Sphere.Application.DTOs.Auth;

namespace Sphere.Application.Common.Interfaces;

/// <summary>
/// JWT token service interface for authentication.
/// </summary>
public interface IJwtTokenService
{
    /// <summary>
    /// Generates access and refresh tokens for the user.
    /// </summary>
    /// <param name="user">User profile.</param>
    /// <returns>Tuple of access token, refresh token, and expiration time.</returns>
    (string AccessToken, string RefreshToken, DateTime ExpiresAt) GenerateTokens(UserProfileDto user);

    /// <summary>
    /// Validates and refreshes tokens.
    /// </summary>
    /// <param name="refreshToken">Refresh token.</param>
    /// <returns>New tokens if valid, null otherwise.</returns>
    Task<(string AccessToken, string RefreshToken, DateTime ExpiresAt)?> RefreshTokensAsync(string refreshToken);

    /// <summary>
    /// Validates an access token and returns the user ID.
    /// </summary>
    /// <param name="token">Access token.</param>
    /// <returns>User ID if valid, null otherwise.</returns>
    string? ValidateToken(string token);

    /// <summary>
    /// Revokes a refresh token.
    /// </summary>
    /// <param name="refreshToken">Refresh token to revoke.</param>
    Task RevokeTokenAsync(string refreshToken);
}
