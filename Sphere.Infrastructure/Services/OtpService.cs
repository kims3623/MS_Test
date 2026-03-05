using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Interfaces;

namespace Sphere.Infrastructure.Services;

/// <summary>
/// OTP service implementation using in-memory cache for development.
/// </summary>
public class OtpService : IOtpService
{
    private readonly IMemoryCache _cache;
    private readonly ILogger<OtpService> _logger;
    private const int OTP_EXPIRY_MINUTES = 5;
    private const int MAX_ATTEMPTS = 3;

    public OtpService(IMemoryCache cache, ILogger<OtpService> logger)
    {
        _cache = cache;
        _logger = logger;
    }

    public Task<string> GenerateOtpAsync(string userId, string divSeq, CancellationToken cancellationToken = default)
    {
        var sessionId = Guid.NewGuid().ToString();
        var otpCode = GenerateRandomOtp();

        var session = new OtpSession
        {
            SessionId = sessionId,
            UserId = userId,
            DivSeq = divSeq,
            OtpCode = otpCode,
            ExpiresAt = DateTime.UtcNow.AddMinutes(OTP_EXPIRY_MINUTES),
            RemainingAttempts = MAX_ATTEMPTS
        };

        _cache.Set(sessionId, session, TimeSpan.FromMinutes(OTP_EXPIRY_MINUTES));

        _logger.LogInformation("OTP generated for user {UserId}: {OtpCode} (Session: {SessionId})", userId, otpCode, sessionId);

        return Task.FromResult(sessionId);
    }

    public Task<OtpGenerationResult> GenerateOtpAsync(string userId, string divSeq, string email, CancellationToken cancellationToken = default)
    {
        var sessionId = Guid.NewGuid().ToString();
        var otpCode = GenerateRandomOtp();

        var session = new OtpSession
        {
            SessionId = sessionId,
            UserId = userId,
            DivSeq = divSeq,
            Email = email,
            OtpCode = otpCode,
            ExpiresAt = DateTime.UtcNow.AddMinutes(OTP_EXPIRY_MINUTES),
            RemainingAttempts = MAX_ATTEMPTS
        };

        _cache.Set(sessionId, session, TimeSpan.FromMinutes(OTP_EXPIRY_MINUTES));

        _logger.LogInformation("OTP generated for user {UserId} ({Email}): {OtpCode}", userId, email, otpCode);

        return Task.FromResult(OtpGenerationResult.Succeeded(sessionId, OTP_EXPIRY_MINUTES * 60));
    }

    public Task<OtpValidationResult> ValidateOtpAsync(string otpSessionId, string otpCode, CancellationToken cancellationToken = default)
    {
        if (!_cache.TryGetValue<OtpSession>(otpSessionId, out var session) || session == null)
        {
            return Task.FromResult(OtpValidationResult.Failure("OTP session not found or expired", 0));
        }

        if (session.ExpiresAt < DateTime.UtcNow)
        {
            _cache.Remove(otpSessionId);
            return Task.FromResult(OtpValidationResult.Failure("OTP has expired", 0));
        }

        if (session.RemainingAttempts <= 0)
        {
            _cache.Remove(otpSessionId);
            return Task.FromResult(OtpValidationResult.Failure("Maximum attempts exceeded", 0));
        }

        if (session.OtpCode != otpCode)
        {
            session.RemainingAttempts--;
            _cache.Set(otpSessionId, session, session.ExpiresAt - DateTime.UtcNow);
            return Task.FromResult(OtpValidationResult.Failure("Invalid OTP code", session.RemainingAttempts));
        }

        _cache.Remove(otpSessionId);
        _logger.LogInformation("OTP validated successfully for session {SessionId}", otpSessionId);

        return Task.FromResult(OtpValidationResult.Success());
    }

    public Task<bool> ResendOtpAsync(string otpSessionId, CancellationToken cancellationToken = default)
    {
        if (!_cache.TryGetValue<OtpSession>(otpSessionId, out var session) || session == null)
        {
            return Task.FromResult(false);
        }

        var newOtpCode = GenerateRandomOtp();
        session.OtpCode = newOtpCode;
        session.ExpiresAt = DateTime.UtcNow.AddMinutes(OTP_EXPIRY_MINUTES);
        session.RemainingAttempts = MAX_ATTEMPTS;

        _cache.Set(otpSessionId, session, TimeSpan.FromMinutes(OTP_EXPIRY_MINUTES));

        _logger.LogInformation("OTP resent for session {SessionId}: {OtpCode}", otpSessionId, newOtpCode);

        return Task.FromResult(true);
    }

    public Task ClearOtpSessionAsync(string otpSessionId, CancellationToken cancellationToken = default)
    {
        _cache.Remove(otpSessionId);
        return Task.CompletedTask;
    }

    public Task<int?> GetRemainingTimeAsync(string otpSessionId, CancellationToken cancellationToken = default)
    {
        if (!_cache.TryGetValue<OtpSession>(otpSessionId, out var session) || session == null)
        {
            return Task.FromResult<int?>(null);
        }

        var remaining = (int)(session.ExpiresAt - DateTime.UtcNow).TotalSeconds;
        return Task.FromResult<int?>(remaining > 0 ? remaining : 0);
    }

    private static string GenerateRandomOtp()
    {
        var random = new Random();
        return random.Next(100000, 999999).ToString();
    }

    private class OtpSession
    {
        public string SessionId { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string DivSeq { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string OtpCode { get; set; } = string.Empty;
        public DateTime ExpiresAt { get; set; }
        public int RemainingAttempts { get; set; }
    }
}
