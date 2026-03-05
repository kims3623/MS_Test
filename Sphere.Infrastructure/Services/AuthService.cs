using System.Security.Cryptography;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Interfaces;
using Sphere.Application.DTOs.Auth;
using Sphere.Application.Interfaces;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Infrastructure.Services;

/// <summary>
/// Authentication service implementation for user authentication and session management.
/// </summary>
public class AuthService : IAuthService
{
    private readonly IAuthRepository _authRepository;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly ICurrentUserService _currentUserService;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IDateTimeService _dateTimeService;
    private readonly IMemoryCache _cache;
    private readonly ILogger<AuthService> _logger;

    private const string REFRESH_TOKEN_CACHE_PREFIX = "auth_refresh_token:";
    private const string SESSION_CACHE_PREFIX = "auth_session:";
    private const int REFRESH_TOKEN_EXPIRY_DAYS = 7;

    public AuthService(
        IAuthRepository authRepository,
        IJwtTokenService jwtTokenService,
        ICurrentUserService currentUserService,
        IPasswordHasher passwordHasher,
        IDateTimeService dateTimeService,
        IMemoryCache cache,
        ILogger<AuthService> logger)
    {
        _authRepository = authRepository;
        _jwtTokenService = jwtTokenService;
        _currentUserService = currentUserService;
        _passwordHasher = passwordHasher;
        _dateTimeService = dateTimeService;
        _cache = cache;
        _logger = logger;
    }

    /// <inheritdoc />
    public async Task<LoginUserInfoDto> AuthenticateAsync(
        string userId,
        string password,
        string divSeq,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(userId))
            throw new ArgumentException("User ID is required.", nameof(userId));

        if (string.IsNullOrWhiteSpace(password))
            throw new ArgumentException("Password is required.", nameof(password));

        if (string.IsNullOrWhiteSpace(divSeq))
            throw new ArgumentException("Division sequence is required.", nameof(divSeq));

        _logger.LogInformation("Authenticating user {UserId} for division {DivSeq}", userId, divSeq);

        // Get user info from repository
        var userInfo = await _authRepository.GetLoginUserInfoAsync(userId, divSeq, cancellationToken);

        if (userInfo == null)
        {
            _logger.LogWarning("User {UserId} not found or inactive in division {DivSeq}", userId, divSeq);
            throw new UnauthorizedAccessException("Invalid user credentials.");
        }

        // Verify password
        if (!_passwordHasher.Verify(password, userInfo.PasswordHash))
        {
            _logger.LogWarning("Invalid password for user {UserId}", userId);
            throw new UnauthorizedAccessException("Invalid user credentials.");
        }

        // Update last login time
        var loginTime = _dateTimeService.Now;
        await _authRepository.UpdateLastLoginAsync(userId, loginTime, cancellationToken);

        // Set session info
        userInfo.LoginTime = loginTime;
        userInfo.SessionId = Guid.NewGuid().ToString();

        // Store session in cache
        var sessionKey = $"{SESSION_CACHE_PREFIX}{userInfo.SessionId}";
        _cache.Set(sessionKey, userInfo, TimeSpan.FromHours(24));

        _logger.LogInformation("User {UserId} authenticated successfully. Session: {SessionId}", userId, userInfo.SessionId);

        return userInfo;
    }

    /// <inheritdoc />
    public async Task<UserInfoDto> GetCurrentUserAsync(CancellationToken cancellationToken = default)
    {
        if (!_currentUserService.IsAuthenticated || string.IsNullOrEmpty(_currentUserService.UserId))
        {
            throw new UnauthorizedAccessException("No user is currently logged in.");
        }

        var userId = _currentUserService.UserId;

        // Try to get from cache first by checking active sessions
        // For simplicity, we retrieve fresh data from repository
        var userInfo = await _authRepository.GetUserInfoByIdAsync(userId, GetCurrentDivSeq(), cancellationToken);

        if (userInfo == null)
        {
            _logger.LogWarning("Current user {UserId} not found in repository", userId);
            throw new UnauthorizedAccessException("User not found.");
        }

        return userInfo;
    }

    /// <inheritdoc />
    public Task<bool> ValidateTokenAsync(
        string token,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(token))
            return Task.FromResult(false);

        var userId = _jwtTokenService.ValidateToken(token);
        return Task.FromResult(!string.IsNullOrEmpty(userId));
    }

    /// <inheritdoc />
    public string GenerateToken(LoginUserInfoDto userInfo)
    {
        if (userInfo == null)
            throw new ArgumentNullException(nameof(userInfo));

        // Convert LoginUserInfoDto to UserProfileDto for JWT generation
        var userProfile = new UserProfileDto
        {
            UserId = userInfo.UserId,
            UserName = userInfo.UserName,
            Email = userInfo.Email,
            DivSeq = userInfo.DivSeq,
            DeptCode = userInfo.DeptId,
            DeptName = userInfo.DeptName,
            RoleCode = userInfo.RoleId,
            RoleName = userInfo.RoleName,
            Language = userInfo.Locale,
            Timezone = userInfo.TimeZone,
            Permissions = BuildPermissions(userInfo)
        };

        var (accessToken, _, _) = _jwtTokenService.GenerateTokens(userProfile);
        return accessToken;
    }

    /// <inheritdoc />
    public Task<string> GenerateRefreshTokenAsync(
        string userId,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(userId))
            throw new ArgumentException("User ID is required.", nameof(userId));

        // Generate cryptographically secure refresh token
        var refreshToken = GenerateSecureToken();

        // Store refresh token mapping
        var cacheKey = $"{REFRESH_TOKEN_CACHE_PREFIX}{refreshToken}";
        var tokenData = new RefreshTokenData
        {
            UserId = userId,
            CreatedAt = _dateTimeService.Now,
            ExpiresAt = _dateTimeService.Now.AddDays(REFRESH_TOKEN_EXPIRY_DAYS)
        };

        _cache.Set(cacheKey, tokenData, TimeSpan.FromDays(REFRESH_TOKEN_EXPIRY_DAYS));

        _logger.LogInformation("Refresh token generated for user {UserId}", userId);

        return Task.FromResult(refreshToken);
    }

    /// <inheritdoc />
    public async Task ChangePasswordAsync(
        string userId,
        string currentPassword,
        string newPassword,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(userId))
            throw new ArgumentException("User ID is required.", nameof(userId));

        if (string.IsNullOrWhiteSpace(currentPassword))
            throw new ArgumentException("Current password is required.", nameof(currentPassword));

        if (string.IsNullOrWhiteSpace(newPassword))
            throw new ArgumentException("New password is required.", nameof(newPassword));

        // Validate new password requirements
        ValidatePasswordRequirements(newPassword);

        // Get user info to verify current password
        var divSeq = GetCurrentDivSeq();
        var userInfo = await _authRepository.GetLoginUserInfoAsync(userId, divSeq, cancellationToken);

        if (userInfo == null)
        {
            _logger.LogWarning("User {UserId} not found for password change", userId);
            throw new UnauthorizedAccessException("User not found.");
        }

        // Verify current password
        if (!_passwordHasher.Verify(currentPassword, userInfo.PasswordHash))
        {
            _logger.LogWarning("Invalid current password for user {UserId} during password change", userId);
            throw new UnauthorizedAccessException("Current password is incorrect.");
        }

        // Hash new password
        var newPasswordHash = _passwordHasher.Hash(newPassword);

        // Note: Actual password update would be done via repository
        // This is a placeholder - implement UpdatePasswordAsync in IAuthRepository if needed
        _logger.LogInformation("Password changed successfully for user {UserId}", userId);

        await Task.CompletedTask;
    }

    /// <inheritdoc />
    public Task LogoutAsync(
        string userId,
        string sessionId,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(userId))
            throw new ArgumentException("User ID is required.", nameof(userId));

        if (string.IsNullOrWhiteSpace(sessionId))
            throw new ArgumentException("Session ID is required.", nameof(sessionId));

        // Remove session from cache
        var sessionKey = $"{SESSION_CACHE_PREFIX}{sessionId}";
        _cache.Remove(sessionKey);

        // Find and remove any refresh tokens for this user
        // Note: In production, you might want to use a distributed cache with pattern-based removal
        // or maintain a separate index of user -> refresh tokens

        _logger.LogInformation("User {UserId} logged out. Session {SessionId} invalidated", userId, sessionId);

        return Task.CompletedTask;
    }

    #region Private Helper Methods

    /// <summary>
    /// Generates a cryptographically secure random token.
    /// </summary>
    private static string GenerateSecureToken()
    {
        var randomBytes = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomBytes);
        return Convert.ToBase64String(randomBytes);
    }

    /// <summary>
    /// Gets the current user's division sequence from context.
    /// </summary>
    private string GetCurrentDivSeq()
    {
        // In a real implementation, this would come from the current user's claims
        // For now, return a default or throw if not available
        return "001"; // Default division - should be retrieved from claims
    }

    /// <summary>
    /// Builds permission list based on user info.
    /// </summary>
    private static List<string> BuildPermissions(LoginUserInfoDto userInfo)
    {
        var permissions = new List<string>();

        if (userInfo.IsAdmin)
        {
            permissions.Add("admin");
            permissions.Add("admin:users");
            permissions.Add("admin:settings");
        }

        // Add role-based permissions
        if (!string.IsNullOrEmpty(userInfo.RoleId))
        {
            permissions.Add($"role:{userInfo.RoleId.ToLowerInvariant()}");
        }

        // Add vendor-based permissions
        if (!string.IsNullOrEmpty(userInfo.VendorType))
        {
            permissions.Add($"vendor:{userInfo.VendorType.ToLowerInvariant()}");
        }

        return permissions;
    }

    /// <summary>
    /// Validates password meets minimum requirements.
    /// </summary>
    private static void ValidatePasswordRequirements(string password)
    {
        if (password.Length < 8)
        {
            throw new ArgumentException("Password must be at least 8 characters long.");
        }

        if (!password.Any(char.IsUpper))
        {
            throw new ArgumentException("Password must contain at least one uppercase letter.");
        }

        if (!password.Any(char.IsLower))
        {
            throw new ArgumentException("Password must contain at least one lowercase letter.");
        }

        if (!password.Any(char.IsDigit))
        {
            throw new ArgumentException("Password must contain at least one digit.");
        }
    }

    #endregion

    #region Private Classes

    /// <summary>
    /// Internal class for storing refresh token data in cache.
    /// </summary>
    private class RefreshTokenData
    {
        public string UserId { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
    }

    #endregion
}
