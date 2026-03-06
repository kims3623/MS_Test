using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Models;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Application.Features.Auth.Queries.GetLoginHistory;

/// <summary>
/// Handler for GetLoginHistoryQuery.
/// </summary>
public class GetLoginHistoryQueryHandler : IRequestHandler<GetLoginHistoryQuery, Result<GetLoginHistoryResponse>>
{
    private readonly IAuthRepository _authRepository;
    private readonly ILogger<GetLoginHistoryQueryHandler> _logger;

    public GetLoginHistoryQueryHandler(
        IAuthRepository authRepository,
        ILogger<GetLoginHistoryQueryHandler> logger)
    {
        _authRepository = authRepository;
        _logger = logger;
    }

    public async Task<Result<GetLoginHistoryResponse>> Handle(GetLoginHistoryQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting login history for user {UserId}", request.UserId);

        var user = await _authRepository.GetUserForAuthAsync(request.UserId, request.DivSeq, cancellationToken);

        if (user is null)
        {
            _logger.LogWarning("Login history query failed: User {UserId} not found", request.UserId);
            return Result<GetLoginHistoryResponse>.Failure("User not found.");
        }

        // Current DB schema doesn't have a separate login history table - return last login from UserInfo
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
