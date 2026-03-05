using FluentValidation;

namespace Sphere.Application.Features.Alarms.Commands.UpdateAlarm;

/// <summary>
/// Validator for UpdateAlarmCommand.
/// </summary>
public class UpdateAlarmCommandValidator : AbstractValidator<UpdateAlarmCommand>
{
    public UpdateAlarmCommandValidator()
    {
        RuleFor(x => x.AlmSysId)
            .NotEmpty().WithMessage("Alarm system ID is required.");

        RuleFor(x => x.DivSeq)
            .NotEmpty().WithMessage("DivSeq is required.");

        RuleFor(x => x.Title)
            .MaximumLength(200).WithMessage("Title must not exceed 200 characters.")
            .When(x => !string.IsNullOrEmpty(x.Title));

        RuleFor(x => x.Severity)
            .Must(s => s == null || new[] { "1", "2", "3", "4", "5" }.Contains(s))
            .WithMessage("Severity must be between 1 and 5.");

        RuleFor(x => x.UpdateUserId)
            .NotEmpty().WithMessage("Update user ID is required.");
    }
}
