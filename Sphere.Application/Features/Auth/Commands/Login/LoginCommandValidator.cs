using FluentValidation;

namespace Sphere.Application.Features.Auth.Commands.Login;

/// <summary>
/// Validator for LoginCommand.
/// </summary>
public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User ID is required.")
            .MaximumLength(50).WithMessage("User ID must not exceed 50 characters.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(4).WithMessage("Password must be at least 4 characters.");

        RuleFor(x => x.DivSeq)
            .NotEmpty().WithMessage("Division is required.")
            .MaximumLength(10).WithMessage("Division must not exceed 10 characters.");
    }
}
