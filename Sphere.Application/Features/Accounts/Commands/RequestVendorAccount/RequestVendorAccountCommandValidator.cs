using FluentValidation;

namespace Sphere.Application.Features.Accounts.Commands.RequestVendorAccount;

/// <summary>
/// Validator for RequestVendorAccountCommand.
/// </summary>
public class RequestVendorAccountCommandValidator : AbstractValidator<RequestVendorAccountCommand>
{
    public RequestVendorAccountCommandValidator()
    {
        RuleFor(x => x.VendorName)
            .NotEmpty().WithMessage("업체명은 필수입니다.")
            .MaximumLength(200).WithMessage("업체명은 200자를 초과할 수 없습니다.");

        RuleFor(x => x.ContactPerson)
            .NotEmpty().WithMessage("담당자명은 필수입니다.")
            .MaximumLength(100).WithMessage("담당자명은 100자를 초과할 수 없습니다.");

        RuleFor(x => x.ContactEmail)
            .NotEmpty().WithMessage("이메일은 필수입니다.")
            .EmailAddress().WithMessage("올바른 이메일 형식이 아닙니다.")
            .MaximumLength(200).WithMessage("이메일은 200자를 초과할 수 없습니다.");

        RuleFor(x => x.ContactPhone)
            .MaximumLength(50).WithMessage("연락처는 50자를 초과할 수 없습니다.")
            .When(x => !string.IsNullOrEmpty(x.ContactPhone));

        RuleFor(x => x.RequestReason)
            .NotEmpty().WithMessage("요청 사유는 필수입니다.")
            .MinimumLength(10).WithMessage("요청 사유는 최소 10자 이상 입력해주세요.")
            .MaximumLength(1000).WithMessage("요청 사유는 1000자를 초과할 수 없습니다.");

        RuleFor(x => x.AdditionalInfo)
            .MaximumLength(2000).WithMessage("추가 정보는 2000자를 초과할 수 없습니다.")
            .When(x => !string.IsNullOrEmpty(x.AdditionalInfo));
    }
}
