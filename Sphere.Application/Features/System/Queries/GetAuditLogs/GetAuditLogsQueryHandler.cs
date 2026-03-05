using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.System;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Application.Features.System.Queries.GetAuditLogs;

/// <summary>
/// Handler for GetAuditLogsQuery.
/// </summary>
public class GetAuditLogsQueryHandler : IRequestHandler<GetAuditLogsQuery, Result<AuditLogResponseDto>>
{
    private readonly ISystemRepository _systemRepository;
    private readonly ILogger<GetAuditLogsQueryHandler> _logger;

    public GetAuditLogsQueryHandler(
        ISystemRepository systemRepository,
        ILogger<GetAuditLogsQueryHandler> logger)
    {
        _systemRepository = systemRepository;
        _logger = logger;
    }

    public async Task<Result<AuditLogResponseDto>> Handle(GetAuditLogsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Fetching audit logs for DivSeq={DivSeq}, Page={Page}",
            request.DivSeq, request.PageNumber);

        try
        {
            var filter = new AuditLogFilterDto
            {
                DivSeq = request.DivSeq,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                UserId = request.UserId,
                ActionType = request.ActionType,
                TargetType = request.TargetType,
                TargetId = request.TargetId,
                IpAddress = request.IpAddress,
                Keyword = request.Keyword,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };

            var result = await _systemRepository.GetAuditLogsAsync(filter, cancellationToken);

            _logger.LogInformation("Found {Count} audit logs", result.TotalCount);

            return Result<AuditLogResponseDto>.Success(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching audit logs");
            return Result<AuditLogResponseDto>.Failure($"감사 로그 조회 중 오류가 발생했습니다: {ex.Message}");
        }
    }
}
