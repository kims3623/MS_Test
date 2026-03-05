using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.System;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Application.Features.System.Queries.GetSystemConfig;

/// <summary>
/// Handler for GetSystemConfigQuery.
/// </summary>
public class GetSystemConfigQueryHandler : IRequestHandler<GetSystemConfigQuery, Result<SystemConfigDto>>
{
    private readonly ISystemRepository _systemRepository;
    private readonly ILogger<GetSystemConfigQueryHandler> _logger;

    public GetSystemConfigQueryHandler(
        ISystemRepository systemRepository,
        ILogger<GetSystemConfigQueryHandler> logger)
    {
        _systemRepository = systemRepository;
        _logger = logger;
    }

    public async Task<Result<SystemConfigDto>> Handle(GetSystemConfigQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Fetching system config for DivSeq={DivSeq}", request.DivSeq);

        try
        {
            var result = await _systemRepository.GetSystemConfigAsync(request.DivSeq, cancellationToken);

            _logger.LogInformation("System config retrieved successfully");

            return Result<SystemConfigDto>.Success(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching system config");
            return Result<SystemConfigDto>.Failure($"시스템 설정 조회 중 오류가 발생했습니다: {ex.Message}");
        }
    }
}
