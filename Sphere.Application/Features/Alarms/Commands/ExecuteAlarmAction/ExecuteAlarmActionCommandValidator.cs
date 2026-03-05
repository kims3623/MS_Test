using FluentValidation;

namespace Sphere.Application.Features.Alarms.Commands.ExecuteAlarmAction;

/// <summary>
/// Validator for ExecuteAlarmActionCommand.
/// </summary>
public class ExecuteAlarmActionCommandValidator : AbstractValidator<ExecuteAlarmActionCommand>
{
    public ExecuteAlarmActionCommandValidator()
    {
        RuleFor(x => x.DivSeq)
            .NotEmpty().WithMessage("DivSeq is required.");

        RuleFor(x => x.AlmSysId)
            .NotEmpty().WithMessage("Alarm system ID is required.");

        RuleFor(x => x.AlmActionId)
            .NotEmpty().WithMessage("Alarm action ID is required.");

        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User ID is required.");
    }
}
