using MediatR;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.System;

namespace Sphere.Application.Features.System.Queries.GetAuditLogs;

/// <summary>
/// Query to get audit logs with filter and pagination.
/// </summary>
public record GetAuditLogsQuery : IRequest<Result<AuditLogResponseDto>>
{
    public string DivSeq { get; init; } = string.Empty;
    public string? StartDate { get; init; }
    public string? EndDate { get; init; }
    public string? UserId { get; init; }
    public string? ActionType { get; init; }
    public string? TargetType { get; init; }
    public string? TargetId { get; init; }
    public string? IpAddress { get; init; }
    public string? Keyword { get; init; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 50;
}
