using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Interfaces;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Account;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Application.Features.Accounts.Commands.RequestVendorAccount;

/// <summary>
/// Handler for RequestVendorAccountCommand.
/// Creates a vendor account request and initiates approval workflow if configured.
/// </summary>
public class RequestVendorAccountCommandHandler
    : IRequestHandler<RequestVendorAccountCommand, Result<VendorAccountRequestResultDto>>
{
    private readonly IAccountRepository _accountRepository;
    private readonly IDateTimeService _dateTimeService;
    private readonly ILogger<RequestVendorAccountCommandHandler> _logger;

    public RequestVendorAccountCommandHandler(
        IAccountRepository accountRepository,
        IDateTimeService dateTimeService,
        ILogger<RequestVendorAccountCommandHandler> logger)
    {
        _accountRepository = accountRepository;
        _dateTimeService = dateTimeService;
        _logger = logger;
    }

    public async Task<Result<VendorAccountRequestResultDto>> Handle(
        RequestVendorAccountCommand request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Processing vendor account request for {VendorName}, Email: {Email}",
            request.VendorName,
            request.ContactEmail);

        try
        {
            var hasPending = await _accountRepository.HasPendingRequestByEmailAsync(request.ContactEmail, cancellationToken);
            if (hasPending)
            {
                return Result<VendorAccountRequestResultDto>.Failure(
                    "이미 동일한 이메일로 대기 중인 계정 요청이 있습니다.");
            }

            var requestId = $"VAR{_dateTimeService.Now:yyyyMMddHHmmss}{Guid.NewGuid().ToString("N")[..6].ToUpper()}";
            var divSeq = string.IsNullOrEmpty(request.DivSeq) ? "01" : request.DivSeq;

            await _accountRepository.InsertVendorAccountRequestAsync(
                divSeq,
                requestId,
                request.VendorName,
                request.VendorId,
                request.ContactPerson,
                request.ContactEmail,
                request.ContactPhone,
                request.RequestReason,
                _dateTimeService.Now,
                cancellationToken);

            _logger.LogInformation(
                "Vendor account request {RequestId} created successfully for {VendorName}",
                requestId,
                request.VendorName);

            return Result<VendorAccountRequestResultDto>.Success(new VendorAccountRequestResultDto
            {
                Success = true,
                Message = "계정 요청이 성공적으로 접수되었습니다. 승인 후 이메일로 안내드리겠습니다.",
                RequestId = requestId
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing vendor account request");
            return Result<VendorAccountRequestResultDto>.Failure("계정 요청 처리 중 오류가 발생했습니다.");
        }
    }
}
