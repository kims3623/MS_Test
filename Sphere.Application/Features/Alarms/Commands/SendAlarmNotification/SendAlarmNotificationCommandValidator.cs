using FluentValidation;

namespace Sphere.Application.Features.Alarms.Commands.SendAlarmNotification;

/// <summary>
/// Validator for SendAlarmNotificationCommand.
/// </summary>
public class SendAlarmNotificationCommandValidator : AbstractValidator<SendAlarmNotificationCommand>
{
    public SendAlarmNotificationCommandValidator()
    {
        RuleFor(x => x.DivSeq)
            .NotEmpty().WithMessage("DivSeq is required.");

        RuleFor(x => x.AlmSysId)
            .NotEmpty().WithMessage("Alarm system ID is required.");

        RuleFor(x => x.RecipientUserIds)
            .NotEmpty().WithMessage("At least one recipient is required.")
            .Must(x => x.Count <= 100).WithMessage("Cannot send to more than 100 recipients at once.");

        RuleFor(x => x.SenderUserId)
            .NotEmpty().WithMessage("Sender user ID is required.");
    }
}
