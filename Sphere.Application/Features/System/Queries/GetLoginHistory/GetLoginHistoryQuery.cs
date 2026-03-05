using MediatR;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.System;

namespace Sphere.Application.Features.System.Queries.GetLoginHistory;

/// <summary>
/// Query for getting login history with filter and pagination.
/// </summary>
public class GetLoginHistoryQuery : IRequest<Result<LoginHistoryResponseDto>>
{
    public string DivSeq { get; set; } = string.Empty;
    public string? UserId { get; set; }
    public string? UserName { get; set; }
    public string? LoginResult { get; set; }
    public string? IpAddress { get; set; }
    public string? StartDate { get; set; }
    public string? EndDate { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 50;
}
