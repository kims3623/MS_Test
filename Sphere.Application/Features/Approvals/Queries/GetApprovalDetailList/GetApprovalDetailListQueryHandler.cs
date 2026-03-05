using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Interfaces;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Approval;

namespace Sphere.Application.Features.Approvals.Queries.GetApprovalDetailList;

public class GetApprovalDetailListQueryHandler : IRequestHandler<GetApprovalDetailListQuery, Result<ApprovalDetailListResponseDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly ILogger<GetApprovalDetailListQueryHandler> _logger;

    public GetApprovalDetailListQueryHandler(
        IApplicationDbContext context,
        ILogger<GetApprovalDetailListQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Result<ApprovalDetailListResponseDto>> Handle(GetApprovalDetailListQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting approval detail list for {AprovId}", request.AprovId);

        var approvals = await _context.Approvals
            .Where(a => a.DivSeq == request.DivSeq && a.AprovId == request.AprovId)
            .ToListAsync(cancellationToken);

        var histories = await _context.ApprovalHistories
            .Where(h => h.DivSeq == request.DivSeq && h.AprovId == request.AprovId)
            .OrderBy(h => h.ActionDate)
            .ToListAsync(cancellationToken);

        var items = histories.Select(h => new ApprovalDetailListItemDto
        {
            AprovId = h.AprovId,
            Action = h.Action,
            ApproverId = h.ApproverId,
            ActionDate = h.ActionDate?.ToString("yyyy-MM-dd HH:mm:ss") ?? string.Empty,
            Comment = h.Comment
        }).ToList();

        var response = new ApprovalDetailListResponseDto
        {
            AprovId = request.AprovId,
            Items = items,
            TotalCount = items.Count
        };

        return Result<ApprovalDetailListResponseDto>.Success(response);
    }
}
