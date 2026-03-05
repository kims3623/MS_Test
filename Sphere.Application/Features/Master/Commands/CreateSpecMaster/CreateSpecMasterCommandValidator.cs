using FluentValidation;

namespace Sphere.Application.Features.Master.Commands.CreateSpecMaster;

/// <summary>
/// Validator for CreateSpecMasterCommand.
/// </summary>
public class CreateSpecMasterCommandValidator : AbstractValidator<CreateSpecMasterCommand>
{
    public CreateSpecMasterCommandValidator()
    {
        RuleFor(x => x.DivSeq)
            .NotEmpty()
            .WithMessage("Division sequence is required.");

        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("User ID is required.");

        RuleFor(x => x.SpecId)
            .NotEmpty()
            .MaximumLength(50)
            .WithMessage("Spec ID is required and must be 50 characters or less.");

        RuleFor(x => x.SpecName)
            .NotEmpty()
            .MaximumLength(200)
            .WithMessage("Spec name is required and must be 200 characters or less.");

        RuleFor(x => x.SpecVersion)
            .NotEmpty()
            .MaximumLength(20)
            .WithMessage("Spec version is required and must be 20 characters or less.");

        RuleFor(x => x.VendorId)
            .NotEmpty()
            .WithMessage("Vendor ID is required.");

        RuleFor(x => x.MtrlClassId)
            .NotEmpty()
            .WithMessage("Material class ID is required.");

        RuleFor(x => x.UseYn)
            .Must(x => x == "Y" || x == "N")
            .WithMessage("Use flag must be 'Y' or 'N'.");
    }
}
