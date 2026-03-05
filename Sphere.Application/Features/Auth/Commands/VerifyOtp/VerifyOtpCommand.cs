using MediatR;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Auth;

namespace Sphere.Application.Features.Auth.Commands.VerifyOtp;

/// <summary>
/// Command for OTP verification.
/// </summary>
public record VerifyOtpCommand : IRequest<Result<LoginResponseDto>>
{
    /// <summary>
    /// OTP session identifier from login.
    /// </summary>
    public string OtpSessionId { get; init; } = string.Empty;

    /// <summary>
    /// OTP code entered by user.
    /// </summary>
    public string OtpCode { get; init; } = string.Empty;

    /// <summary>
    /// User identifier (for session lookup).
    /// </summary>
    public string UserId { get; init; } = string.Empty;

    /// <summary>
    /// Division sequence.
    /// </summary>
    public string DivSeq { get; init; } = string.Empty;
}
