using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Interfaces;
using Sphere.Application.Common.Models;

namespace Sphere.Application.Features.Auth.Commands.ResendOtp;

/// <summary>
/// Handler for ResendOtpCommand.
/// </summary>
public class ResendOtpCommandHandler : IRequestHandler<ResendOtpCommand, Result<ResendOtpResponse>>
{
    private readonly IOtpService _otpService;
    private readonly ILogger<ResendOtpCommandHandler> _logger;

    public ResendOtpCommandHandler(
        IOtpService otpService,
        ILogger<ResendOtpCommandHandler> logger)
    {
        _otpService = otpService;
        _logger = logger;
    }

    public async Task<Result<ResendOtpResponse>> Handle(ResendOtpCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("OTP resend request for session {SessionId}", request.OtpSessionId);

        var success = await _otpService.ResendOtpAsync(request.OtpSessionId, cancellationToken);

        if (!success)
        {
            _logger.LogWarning("OTP resend failed for session {SessionId}", request.OtpSessionId);
            return Result<ResendOtpResponse>.Failure("Failed to resend OTP. Please try again.");
        }

        var remainingSeconds = await _otpService.GetRemainingTimeAsync(request.OtpSessionId, cancellationToken);

        _logger.LogInformation("OTP resent successfully for session {SessionId}", request.OtpSessionId);

        return Result<ResendOtpResponse>.Success(new ResendOtpResponse
        {
            Success = true,
            RemainingSeconds = remainingSeconds ?? 180,
            Message = "OTP has been resent to your registered device."
        });
    }
}
