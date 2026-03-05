using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Sphere.Application.Common.Interfaces;
using Sphere.Application.DTOs.Auth;

namespace Sphere.Infrastructure.Identity;

/// <summary>
/// JWT token service implementation.
/// </summary>
public class JwtTokenService : IJwtTokenService
{
    private readonly JwtSettings _settings;
    private readonly IMemoryCache _cache;
    private readonly IDateTimeService _dateTimeService;

    public JwtTokenService(
        IOptions<JwtSettings> settings,
        IMemoryCache cache,
        IDateTimeService dateTimeService)
    {
        _settings = settings.Value;
        _cache = cache;
        _dateTimeService = dateTimeService;
    }

    public (string AccessToken, string RefreshToken, DateTime ExpiresAt) GenerateTokens(UserProfileDto user)
    {
        var expiresAt = _dateTimeService.Now.AddMinutes(_settings.ExpirationMinutes);

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.UserId),
            new(ClaimTypes.Name, user.UserName),
            new(ClaimTypes.Email, user.Email),
            new("div_seq", user.DivSeq),
            new("dept_code", user.DeptCode),
            new("role_code", user.RoleCode),
            new(ClaimTypes.Role, user.RoleName)
        };

        // Add permissions as claims
        foreach (var permission in user.Permissions)
        {
            claims.Add(new Claim("permission", permission));
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Secret));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _settings.Issuer,
            audience: _settings.Audience,
            claims: claims,
            expires: expiresAt,
            signingCredentials: credentials);

        var accessToken = new JwtSecurityTokenHandler().WriteToken(token);
        var refreshToken = GenerateRefreshToken();

        // Store refresh token mapping
        var cacheKey = $"refresh_token:{refreshToken}";
        _cache.Set(cacheKey, new RefreshTokenData
        {
            UserId = user.UserId,
            DivSeq = user.DivSeq,
            ExpiresAt = _dateTimeService.Now.AddDays(_settings.RefreshExpirationDays)
        }, TimeSpan.FromDays(_settings.RefreshExpirationDays));

        return (accessToken, refreshToken, expiresAt);
    }

    public async Task<(string AccessToken, string RefreshToken, DateTime ExpiresAt)?> RefreshTokensAsync(string refreshToken)
    {
        var cacheKey = $"refresh_token:{refreshToken}";

        if (!_cache.TryGetValue<RefreshTokenData>(cacheKey, out var tokenData) || tokenData is null)
        {
            return null;
        }

        if (tokenData.ExpiresAt < _dateTimeService.Now)
        {
            _cache.Remove(cacheKey);
            return null;
        }

        // Remove old refresh token
        _cache.Remove(cacheKey);

        // Generate new tokens with minimal user profile (will be refreshed on next API call)
        var userProfile = new UserProfileDto
        {
            UserId = tokenData.UserId,
            DivSeq = tokenData.DivSeq
        };

        return await Task.FromResult(GenerateTokens(userProfile));
    }

    public string? ValidateToken(string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_settings.Secret);

            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidIssuer = _settings.Issuer,
                ValidateAudience = true,
                ValidAudience = _settings.Audience,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            }, out var validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            return jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        }
        catch
        {
            return null;
        }
    }

    public Task RevokeTokenAsync(string refreshToken)
    {
        var cacheKey = $"refresh_token:{refreshToken}";
        _cache.Remove(cacheKey);
        return Task.CompletedTask;
    }

    private static string GenerateRefreshToken()
    {
        var randomBytes = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomBytes);
        return Convert.ToBase64String(randomBytes);
    }

    private class RefreshTokenData
    {
        public string UserId { get; set; } = string.Empty;
        public string DivSeq { get; set; } = string.Empty;
        public DateTime ExpiresAt { get; set; }
    }
}
