using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.System;

namespace Sphere.Application.Features.System.Queries.GetActiveSessions;

/// <summary>
/// Handler for GetActiveSessionsQuery.
/// </summary>
public class GetActiveSessionsQueryHandler : IRequestHandler<GetActiveSessionsQuery, Result<ActiveSessionResponseDto>>
{
    private readonly ILogger<GetActiveSessionsQueryHandler> _logger;

    public GetActiveSessionsQueryHandler(ILogger<GetActiveSessionsQueryHandler> logger)
    {
        _logger = logger;
    }

    public async Task<Result<ActiveSessionResponseDto>> Handle(
        GetActiveSessionsQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var response = new ActiveSessionResponseDto
            {
                Items = new List<ActiveSessionItemDto>(),
                TotalCount = 0,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                ActiveCount = 0,
                IdleCount = 0
            };

            _logger.LogInformation("Active sessions retrieved: {Count} items", response.TotalCount);
            return Result<ActiveSessionResponseDto>.Success(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving active sessions");
            return Result<ActiveSessionResponseDto>.Failure($"Error retrieving active sessions: {ex.Message}");
        }
    }
}
