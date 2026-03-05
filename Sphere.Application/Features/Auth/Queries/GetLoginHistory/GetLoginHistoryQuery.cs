using MediatR;
using Sphere.Application.Common.Models;

namespace Sphere.Application.Features.Auth.Queries.GetLoginHistory;

/// <summary>
/// Query to get user login history.
/// </summary>
public class GetLoginHistoryQuery : IRequest<Result<GetLoginHistoryResponse>>
{
    public string UserId { get; set; } = string.Empty;
    public string DivSeq { get; set; } = string.Empty;
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}

/// <summary>
/// Response for login history.
/// </summary>
public class GetLoginHistoryResponse
{
    public List<LoginHistoryItem> Items { get; set; } = new();
    public int TotalCount { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}

/// <summary>
/// Login history item.
/// </summary>
public class LoginHistoryItem
{
    public DateTime LoginTime { get; set; }
    public string IpAddress { get; set; } = string.Empty;
    public string UserAgent { get; set; } = string.Empty;
    public string LoginResult { get; set; } = string.Empty; // Success, Failed, Locked
    public string FailureReason { get; set; } = string.Empty;
    public DateTime? LogoutTime { get; set; }
}
