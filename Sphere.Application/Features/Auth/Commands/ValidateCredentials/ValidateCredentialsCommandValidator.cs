using FluentValidation;

namespace Sphere.Application.Features.Auth.Commands.ValidateCredentials;

/// <summary>
/// Validator for ValidateCredentialsCommand.
/// </summary>
public class ValidateCredentialsCommandValidator : AbstractValidator<ValidateCredentialsCommand>
{
    public ValidateCredentialsCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User ID is required.")
            .MaximumLength(50).WithMessage("User ID must not exceed 50 characters.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.");

        RuleFor(x => x.DivSeq)
            .NotEmpty().WithMessage("Division is required.");
    }
}
