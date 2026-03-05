namespace Sphere.Application.Common.Interfaces;

/// <summary>
/// OTP validation result.
/// </summary>
public class OtpValidationResult
{
    public bool IsValid { get; set; }
    public string? FailureReason { get; set; }
    public int RemainingAttempts { get; set; }
    public DateTime? ExpiresAt { get; set; }

    public static OtpValidationResult Success() => new() { IsValid = true };

    public static OtpValidationResult Failure(string reason, int remainingAttempts = 0) =>
        new() { IsValid = false, FailureReason = reason, RemainingAttempts = remainingAttempts };
}

/// <summary>
/// OTP generation result.
/// </summary>
public class OtpGenerationResult
{
    public bool Success { get; set; }
    public string SessionId { get; set; } = string.Empty;
    public int ExpiresInSeconds { get; set; }
    public string? FailureReason { get; set; }

    public static OtpGenerationResult Succeeded(string sessionId, int expiresInSeconds) =>
        new() { Success = true, SessionId = sessionId, ExpiresInSeconds = expiresInSeconds };

    public static OtpGenerationResult Failed(string reason) =>
        new() { Success = false, FailureReason = reason };
}

/// <summary>
/// Service interface for OTP operations.
/// </summary>
public interface IOtpService
{
    /// <summary>
    /// Generates and sends an OTP code for the specified user.
    /// </summary>
    /// <param name="userId">User identifier.</param>
    /// <param name="divSeq">Division sequence.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>OTP session ID for verification.</returns>
    Task<string> GenerateOtpAsync(string userId, string divSeq, CancellationToken cancellationToken = default);

    /// <summary>
    /// Generates and sends an OTP code for the specified user with email.
    /// </summary>
    /// <param name="userId">User identifier.</param>
    /// <param name="divSeq">Division sequence.</param>
    /// <param name="email">User email for sending OTP.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>OTP generation result.</returns>
    Task<OtpGenerationResult> GenerateOtpAsync(string userId, string divSeq, string email, CancellationToken cancellationToken = default);

    /// <summary>
    /// Validates an OTP code for the given session.
    /// </summary>
    /// <param name="otpSessionId">OTP session identifier.</param>
    /// <param name="otpCode">OTP code to validate.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Validation result.</returns>
    Task<OtpValidationResult> ValidateOtpAsync(string otpSessionId, string otpCode, CancellationToken cancellationToken = default);

    /// <summary>
    /// Resends OTP code for the specified session.
    /// </summary>
    /// <param name="otpSessionId">OTP session identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True if resend was successful.</returns>
    Task<bool> ResendOtpAsync(string otpSessionId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Clears an OTP session after successful verification.
    /// </summary>
    /// <param name="otpSessionId">OTP session identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task ClearOtpSessionAsync(string otpSessionId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets remaining time for OTP session.
    /// </summary>
    /// <param name="otpSessionId">OTP session identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Remaining seconds, or null if session not found.</returns>
    Task<int?> GetRemainingTimeAsync(string otpSessionId, CancellationToken cancellationToken = default);
}
