using FluentValidation;

namespace Sphere.Application.Features.Alarms.Commands.UploadAlarmAttachment;

/// <summary>
/// Validator for UploadAlarmAttachmentCommand.
/// </summary>
public class UploadAlarmAttachmentCommandValidator : AbstractValidator<UploadAlarmAttachmentCommand>
{
    private const long MaxFileSize = 10 * 1024 * 1024; // 10MB

    public UploadAlarmAttachmentCommandValidator()
    {
        RuleFor(x => x.DivSeq)
            .NotEmpty().WithMessage("DivSeq is required.");

        RuleFor(x => x.AlmSysId)
            .NotEmpty().WithMessage("Alarm system ID is required.");

        RuleFor(x => x.OriginalFileName)
            .NotEmpty().WithMessage("File name is required.")
            .MaximumLength(255).WithMessage("File name must not exceed 255 characters.");

        RuleFor(x => x.FileSize)
            .GreaterThan(0).WithMessage("File size must be greater than 0.")
            .LessThanOrEqualTo(MaxFileSize).WithMessage($"File size must not exceed {MaxFileSize / 1024 / 1024}MB.");

        RuleFor(x => x.FileContent)
            .NotEmpty().WithMessage("File content is required.");

        RuleFor(x => x.CreateUserId)
            .NotEmpty().WithMessage("Create user ID is required.");
    }
}
