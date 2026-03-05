using MediatR;
using Sphere.Application.Common.Models;

namespace Sphere.Application.Features.Auth.Commands.ResendOtp;

/// <summary>
/// Response for OTP resend.
/// </summary>
public class ResendOtpResponse
{
    public bool Success { get; set; }
    public int RemainingSeconds { get; set; }
    public string Message { get; set; } = string.Empty;
}

/// <summary>
/// Command for resending OTP code.
/// </summary>
public record ResendOtpCommand : IRequest<Result<ResendOtpResponse>>
{
    /// <summary>
    /// OTP session identifier.
    /// </summary>
    public string OtpSessionId { get; init; } = string.Empty;
}
