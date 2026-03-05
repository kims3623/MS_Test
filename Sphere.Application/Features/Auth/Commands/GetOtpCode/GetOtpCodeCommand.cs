using MediatR;
using Sphere.Application.Common.Models;

namespace Sphere.Application.Features.Auth.Commands.GetOtpCode;

/// <summary>
/// Command to generate and send OTP code to user.
/// </summary>
public class GetOtpCodeCommand : IRequest<Result<GetOtpCodeResponse>>
{
    public string UserId { get; set; } = string.Empty;
    public string DivSeq { get; set; } = string.Empty;
}

/// <summary>
/// Response for OTP code generation.
/// </summary>
public class GetOtpCodeResponse
{
    public string OtpSessionId { get; set; } = string.Empty;
    public int ExpiresInSeconds { get; set; }
    public string DeliveryMethod { get; set; } = "email"; // email, sms
    public string MaskedDestination { get; set; } = string.Empty; // e.g., "j***@example.com"
}
