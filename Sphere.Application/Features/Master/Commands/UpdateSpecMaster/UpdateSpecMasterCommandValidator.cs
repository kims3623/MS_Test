using FluentValidation;

namespace Sphere.Application.Features.Master.Commands.UpdateSpecMaster;

/// <summary>
/// Validator for UpdateSpecMasterCommand.
/// </summary>
public class UpdateSpecMasterCommandValidator : AbstractValidator<UpdateSpecMasterCommand>
{
    public UpdateSpecMasterCommandValidator()
    {
        RuleFor(x => x.DivSeq)
            .NotEmpty()
            .WithMessage("Division sequence is required.");

        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("User ID is required.");

        RuleFor(x => x.SpecSysId)
            .NotEmpty()
            .WithMessage("Spec system ID is required.");

        RuleFor(x => x.SpecName)
            .NotEmpty()
            .MaximumLength(200)
            .WithMessage("Spec name is required and must be 200 characters or less.");

        RuleFor(x => x.SpecVersion)
            .NotEmpty()
            .MaximumLength(20)
            .WithMessage("Spec version is required and must be 20 characters or less.");

        RuleFor(x => x.UseYn)
            .Must(x => x == "Y" || x == "N")
            .WithMessage("Use flag must be 'Y' or 'N'.");
    }
}
