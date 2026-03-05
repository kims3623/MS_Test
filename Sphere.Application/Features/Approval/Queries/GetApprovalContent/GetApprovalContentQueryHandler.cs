using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Interfaces;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Approval;

namespace Sphere.Application.Features.Approval.Queries.GetApprovalContent;

/// <summary>
/// Handler for GetApprovalContentQuery.
/// </summary>
public class GetApprovalContentQueryHandler : IRequestHandler<GetApprovalContentQuery, Result<ApprovalContentDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly ILogger<GetApprovalContentQueryHandler> _logger;

    public GetApprovalContentQueryHandler(
        IApplicationDbContext context,
        ILogger<GetApprovalContentQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Result<ApprovalContentDto>> Handle(GetApprovalContentQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Fetching approval content for AprovId={AprovId}, DivSeq={DivSeq}, ChgTypeId={ChgTypeId}",
            request.AprovId, request.DivSeq, request.ChgTypeId);

        var approval = await _context.Approvals
            .Where(a => a.AprovId == request.AprovId && a.DivSeq == request.DivSeq)
            .FirstOrDefaultAsync(cancellationToken);

        if (approval is null)
        {
            _logger.LogWarning("Approval {AprovId} not found", request.AprovId);
            return Result<ApprovalContentDto>.Failure("Approval not found.");
        }

        // Get approval history
        var history = await _context.ApprovalHistories
            .Where(h => h.AprovId == request.AprovId && h.DivSeq == request.DivSeq)
            .OrderBy(h => h.HistSeq)
            .ToListAsync(cancellationToken);

        // Get requestor info
        var requestor = await _context.UserInfos
            .Where(u => u.UserId == approval.Writer && u.DivSeq == request.DivSeq)
            .FirstOrDefaultAsync(cancellationToken);

        // Get file attachments
        var attachments = await _context.ApprovalAttachments
            .Where(a => a.AprovId == request.AprovId)
            .OrderBy(a => a.UploadDate)
            .ToListAsync(cancellationToken);

        var result = new ApprovalContentDto
        {
            AprovId = approval.AprovId,
            DivSeq = approval.DivSeq,
            ChgTypeId = approval.ChgTypeId ?? string.Empty,
            ChgTypeName = approval.ChgTypeName ?? string.Empty,
            Title = approval.Title ?? string.Empty,
            Contents = approval.Contents ?? string.Empty,
            Requestor = approval.Writer ?? string.Empty,
            RequestorName = requestor?.UserName ?? approval.Writer ?? string.Empty,
            RequestDate = approval.CreateDate?.ToString("yyyy-MM-dd HH:mm:ss") ?? string.Empty,
            Status = approval.AprovState ?? string.Empty,
            StatusName = GetStatusName(approval.AprovState),
            ApprovalHistory = history.Select(h => new ApprovalHistoryItemDto
            {
                Seq = h.HistSeq,
                ActionDate = h.ActionDate?.ToString("yyyy-MM-dd HH:mm:ss") ?? string.Empty,
                UserId = h.ApproverId ?? string.Empty,
                UserName = h.ApproverName ?? h.ApproverId ?? string.Empty,
                Action = h.Action ?? string.Empty,
                ActionName = GetActionName(h.Action),
                Comment = h.Comment
            }).ToList(),
            Attachments = attachments.Count > 0
                ? attachments.Select(a => new AttachmentInfoDto
                {
                    FileId = a.AttachmentId,
                    FileName = a.OriginalFileName ?? a.FileName,
                    FileSize = a.FileSize,
                    FileUrl = a.FilePath
                }).ToList()
                : null
        };

        return Result<ApprovalContentDto>.Success(result);
    }

    private static string GetStatusName(string? status)
    {
        return status?.ToLower() switch
        {
            "approved" => "승인완료",
            "rejected" => "반려",
            "pending" => "진행중",
            "draft" => "임시저장",
            "cancelled" => "취소",
            _ => status ?? "알 수 없음"
        };
    }

    private static string GetActionName(string? action)
    {
        return action?.ToLower() switch
        {
            "request" or "submit" => "요청",
            "approve" or "approved" => "승인",
            "reject" or "rejected" => "반려",
            "cancel" or "cancelled" => "취소",
            "return" => "반송",
            _ => action ?? "알 수 없음"
        };
    }
}
