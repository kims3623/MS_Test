using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Interfaces;
using Sphere.Application.Common.Models;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Application.Features.Auth.Commands.GetOtpCode;

/// <summary>
/// Handler for GetOtpCodeCommand.
/// Note: OTP is not supported in current DB schema.
/// </summary>
public class GetOtpCodeCommandHandler : IRequestHandler<GetOtpCodeCommand, Result<GetOtpCodeResponse>>
{
    private readonly IAuthRepository _authRepository;
    private readonly IOtpService _otpService;
    private readonly ILogger<GetOtpCodeCommandHandler> _logger;

    public GetOtpCodeCommandHandler(
        IAuthRepository authRepository,
        IOtpService otpService,
        ILogger<GetOtpCodeCommandHandler> logger)
    {
        _authRepository = authRepository;
        _otpService = otpService;
        _logger = logger;
    }

    public async Task<Result<GetOtpCodeResponse>> Handle(GetOtpCodeCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("OTP code generation request for user {UserId}", request.UserId);

        var user = await _authRepository.GetUserForAuthAsync(request.UserId, request.DivSeq, cancellationToken);

        if (user is null || user.UseYn != "Y")
        {
            _logger.LogWarning("OTP generation failed: User {UserId} not found", request.UserId);
            return Result<GetOtpCodeResponse>.Failure("User not found.");
        }

        // OTP is not supported in current DB schema
        _logger.LogWarning("OTP generation failed: OTP not enabled for user {UserId} (feature not supported)", request.UserId);
        return Result<GetOtpCodeResponse>.Failure("OTP is not enabled for this user.");
    }

    private static string MaskEmail(string email)
    {
        if (string.IsNullOrEmpty(email) || !email.Contains('@'))
            return "***";

        var parts = email.Split('@');
        var localPart = parts[0];
        var domain = parts[1];

        if (localPart.Length <= 2)
            return $"{localPart[0]}***@{domain}";

        return $"{localPart[0]}***{localPart[^1]}@{domain}";
    }
}
