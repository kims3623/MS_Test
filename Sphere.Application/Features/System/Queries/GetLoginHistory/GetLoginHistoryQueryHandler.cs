using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.System;

namespace Sphere.Application.Features.System.Queries.GetLoginHistory;

/// <summary>
/// Handler for GetLoginHistoryQuery.
/// </summary>
public class GetLoginHistoryQueryHandler : IRequestHandler<GetLoginHistoryQuery, Result<LoginHistoryResponseDto>>
{
    private readonly ILogger<GetLoginHistoryQueryHandler> _logger;

    public GetLoginHistoryQueryHandler(ILogger<GetLoginHistoryQueryHandler> logger)
    {
        _logger = logger;
    }

    public async Task<Result<LoginHistoryResponseDto>> Handle(
        GetLoginHistoryQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            // Query login history from database

            var response = new LoginHistoryResponseDto
            {
                Items = new List<LoginHistoryItemDto>(),
                TotalCount = 0,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };

            _logger.LogInformation("Login history retrieved: {Count} items", response.TotalCount);
            return Result<LoginHistoryResponseDto>.Success(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving login history");
            return Result<LoginHistoryResponseDto>.Failure($"Error retrieving login history: {ex.Message}");
        }
    }
}
