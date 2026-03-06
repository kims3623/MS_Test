using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Approval;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Application.Features.Approval.Queries.GetApprovalContent;

/// <summary>
/// Handler for GetApprovalContentQuery.
/// </summary>
public class GetApprovalContentQueryHandler : IRequestHandler<GetApprovalContentQuery, Result<ApprovalContentDto>>
{
    private readonly IApprovalRepository _approvalRepository;
    private readonly IAuthRepository _authRepository;
    private readonly ILogger<GetApprovalContentQueryHandler> _logger;

    public GetApprovalContentQueryHandler(
        IApprovalRepository approvalRepository,
        IAuthRepository authRepository,
        ILogger<GetApprovalContentQueryHandler> logger)
    {
        _approvalRepository = approvalRepository;
        _authRepository = authRepository;
        _logger = logger;
    }

    public async Task<Result<ApprovalContentDto>> Handle(GetApprovalContentQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Fetching approval content for AprovId={AprovId}, DivSeq={DivSeq}, ChgTypeId={ChgTypeId}",
            request.AprovId, request.DivSeq, request.ChgTypeId);

        var approval = await _approvalRepository.GetDetailAsync(request.DivSeq, request.AprovId);

        if (approval is null)
        {
            _logger.LogWarning("Approval {AprovId} not found", request.AprovId);
            return Result<ApprovalContentDto>.Failure("Approval not found.");
        }

        var historyTask = _approvalRepository.GetApprovalHistoriesAsync(request.DivSeq, request.AprovId, cancellationToken);
        var attachmentsTask = _approvalRepository.GetApprovalAttachmentsAsync(request.AprovId, cancellationToken);
        var requestorTask = string.IsNullOrEmpty(approval.Writer)
            ? Task.FromResult<Sphere.Application.DTOs.Auth.UserAuthDto?>(null)
            : _authRepository.GetUserForAuthAsync(approval.Writer, request.DivSeq, cancellationToken);

        await Task.WhenAll(historyTask, attachmentsTask, requestorTask);

        var history = (await historyTask).ToList();
        var attachments = (await attachmentsTask).ToList();
        var requestor = await requestorTask;

        var result = new ApprovalContentDto
        {
            AprovId = approval.AprovId,
            DivSeq = approval.DivSeq,
            ChgTypeId = approval.ChgTypeId,
            ChgTypeName = approval.ChgTypeName,
            Title = approval.Title,
            Contents = approval.Contents,
            Requestor = approval.Writer,
            RequestorName = requestor?.UserName ?? approval.Writer,
            RequestDate = approval.CreateDate,
            Status = approval.AprovState,
            StatusName = GetStatusName(approval.AprovState),
            ApprovalHistory = history.Select(h => new ApprovalHistoryItemDto
            {
                Seq = h.Seq,
                ActionDate = h.ActionDate,
                UserId = h.UserId,
                UserName = h.UserName,
                Action = h.Action,
                ActionName = GetActionName(h.Action),
                Comment = h.Comment
            }).ToList(),
            Attachments = attachments.Count > 0 ? attachments.ToList() : null
        };

        return Result<ApprovalContentDto>.Success(result);
    }

    private static string GetStatusName(string? status)
    {
        return status?.ToLower() switch
        {
            "approved" => "승인완료",
            "rejected" => "반려",
            "pending"  => "진행중",
            "draft"    => "임시저장",
            "cancelled" => "취소",
            _ => status ?? "알 수 없음"
        };
    }

    private static string GetActionName(string? action)
    {
        return action?.ToLower() switch
        {
            "request" or "submit"     => "요청",
            "approve" or "approved"   => "승인",
            "reject"  or "rejected"   => "반려",
            "cancel"  or "cancelled"  => "취소",
            "return"                  => "반송",
            _ => action ?? "알 수 없음"
        };
    }
}
