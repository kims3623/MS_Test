using FluentValidation;

namespace Sphere.Application.Features.Auth.Commands.VerifyOtp;

/// <summary>
/// Validator for VerifyOtpCommand.
/// </summary>
public class VerifyOtpCommandValidator : AbstractValidator<VerifyOtpCommand>
{
    public VerifyOtpCommandValidator()
    {
        RuleFor(x => x.OtpSessionId)
            .NotEmpty().WithMessage("OTP session is required.");

        RuleFor(x => x.OtpCode)
            .NotEmpty().WithMessage("OTP code is required.")
            .Length(6).WithMessage("OTP code must be 6 digits.")
            .Matches("^[0-9]+$").WithMessage("OTP code must contain only digits.");

        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User ID is required.");

        RuleFor(x => x.DivSeq)
            .NotEmpty().WithMessage("Division is required.");
    }
}
