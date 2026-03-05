using FluentValidation;

namespace Sphere.Application.Features.Auth.Commands.GetOtpCode;

/// <summary>
/// Validator for GetOtpCodeCommand.
/// </summary>
public class GetOtpCodeCommandValidator : AbstractValidator<GetOtpCodeCommand>
{
    public GetOtpCodeCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User ID is required.")
            .MaximumLength(50).WithMessage("User ID must not exceed 50 characters.");

        RuleFor(x => x.DivSeq)
            .NotEmpty().WithMessage("Division is required.");
    }
}
