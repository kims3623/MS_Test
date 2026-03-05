using MediatR;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.System;

namespace Sphere.Application.Features.System.Queries.GetActiveSessions;

/// <summary>
/// Query for getting active sessions with filter and pagination.
/// </summary>
public class GetActiveSessionsQuery : IRequest<Result<ActiveSessionResponseDto>>
{
    public string DivSeq { get; set; } = string.Empty;
    public string? UserId { get; set; }
    public string? IpAddress { get; set; }
    public string? SessionStatus { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 50;
}
