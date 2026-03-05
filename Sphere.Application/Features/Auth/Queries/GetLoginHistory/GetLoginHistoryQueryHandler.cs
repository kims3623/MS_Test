using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Interfaces;
using Sphere.Application.Common.Models;

namespace Sphere.Application.Features.Auth.Queries.GetLoginHistory;

/// <summary>
/// Handler for GetLoginHistoryQuery.
/// </summary>
public class GetLoginHistoryQueryHandler : IRequestHandler<GetLoginHistoryQuery, Result<GetLoginHistoryResponse>>
{
    private readonly IApplicationDbContext _context;
    private readonly ILogger<GetLoginHistoryQueryHandler> _logger;

    public GetLoginHistoryQueryHandler(
        IApplicationDbContext context,
        ILogger<GetLoginHistoryQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Result<GetLoginHistoryResponse>> Handle(GetLoginHistoryQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting login history for user {UserId}", request.UserId);

        // 1. Verify user exists
        var user = await _context.UserInfos
            .Where(u => u.DivSeq == request.DivSeq && u.UserId == request.UserId)
            .FirstOrDefaultAsync(cancellationToken);

        if (user is null)
        {
            _logger.LogWarning("Login history query failed: User {UserId} not found", request.UserId);
            return Result<GetLoginHistoryResponse>.Failure("User not found.");
        }

        // 2. Return login history from UserInfo's last login date
        // Note: Current DB schema doesn't have a separate login history table
        var items = new List<LoginHistoryItem>();

        if (user.LastLoginDate.HasValue)
        {
            items.Add(new LoginHistoryItem
            {
                LoginTime = user.LastLoginDate.Value,
                IpAddress = "Unknown",
                UserAgent = "Unknown",
                LoginResult = "Success",
                FailureReason = string.Empty,
                LogoutTime = null
            });
        }

        return Result<GetLoginHistoryResponse>.Success(new GetLoginHistoryResponse
        {
            Items = items,
            TotalCount = items.Count,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize
        });
    }
}
