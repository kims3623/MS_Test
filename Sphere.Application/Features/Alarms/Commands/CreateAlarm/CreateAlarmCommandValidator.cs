using FluentValidation;

namespace Sphere.Application.Features.Alarms.Commands.CreateAlarm;

/// <summary>
/// Validator for CreateAlarmCommand.
/// </summary>
public class CreateAlarmCommandValidator : AbstractValidator<CreateAlarmCommand>
{
    public CreateAlarmCommandValidator()
    {
        RuleFor(x => x.DivSeq)
            .NotEmpty().WithMessage("DivSeq is required.");

        RuleFor(x => x.AlmProcId)
            .NotEmpty().WithMessage("Alarm process ID is required.");

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(200).WithMessage("Title must not exceed 200 characters.");

        RuleFor(x => x.VendorId)
            .NotEmpty().WithMessage("Vendor ID is required.");

        RuleFor(x => x.MtrlClassId)
            .NotEmpty().WithMessage("Material class ID is required.");

        RuleFor(x => x.Severity)
            .Must(s => new[] { "1", "2", "3", "4", "5" }.Contains(s))
            .WithMessage("Severity must be between 1 and 5.");

        RuleFor(x => x.CreateUserId)
            .NotEmpty().WithMessage("Create user ID is required.");
    }
}
