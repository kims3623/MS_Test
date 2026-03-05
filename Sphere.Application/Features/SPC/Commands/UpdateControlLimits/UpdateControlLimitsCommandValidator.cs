using FluentValidation;

namespace Sphere.Application.Features.SPC.Commands.UpdateControlLimits;

/// <summary>
/// Validator for UpdateControlLimitsCommand.
/// </summary>
public class UpdateControlLimitsCommandValidator : AbstractValidator<UpdateControlLimitsCommand>
{
    public UpdateControlLimitsCommandValidator()
    {
        RuleFor(x => x.SpecSysId)
            .NotEmpty().WithMessage("SpecSysId is required.");

        RuleFor(x => x.ChartType)
            .NotEmpty().WithMessage("ChartType is required.");

        RuleFor(x => x.Ucl)
            .GreaterThan(x => x.Cl)
            .WithMessage("UCL must be greater than CL.");

        RuleFor(x => x.Cl)
            .GreaterThan(x => x.Lcl)
            .WithMessage("CL must be greater than LCL.");

        RuleFor(x => x.Usl)
            .GreaterThan(x => x.Lsl)
            .When(x => x.Usl.HasValue && x.Lsl.HasValue)
            .WithMessage("USL must be greater than LSL.");

        RuleFor(x => x.Target)
            .GreaterThanOrEqualTo(x => x.Lsl)
            .LessThanOrEqualTo(x => x.Usl)
            .When(x => x.Target.HasValue && x.Lsl.HasValue && x.Usl.HasValue)
            .WithMessage("Target must be between LSL and USL.");
    }
}
