using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.System;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Application.Features.System.Queries.GetSecurityPolicy;

/// <summary>
/// Handler for GetSecurityPolicyQuery.
/// </summary>
public class GetSecurityPolicyQueryHandler : IRequestHandler<GetSecurityPolicyQuery, Result<SecurityPolicyDto>>
{
    private readonly ISystemRepository _systemRepository;
    private readonly ILogger<GetSecurityPolicyQueryHandler> _logger;

    public GetSecurityPolicyQueryHandler(
        ISystemRepository systemRepository,
        ILogger<GetSecurityPolicyQueryHandler> logger)
    {
        _systemRepository = systemRepository;
        _logger = logger;
    }

    public async Task<Result<SecurityPolicyDto>> Handle(GetSecurityPolicyQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Fetching security policy for DivSeq={DivSeq}", request.DivSeq);

        try
        {
            var result = await _systemRepository.GetSecurityPolicyAsync(request.DivSeq, cancellationToken);

            _logger.LogInformation("Security policy retrieved successfully");

            return Result<SecurityPolicyDto>.Success(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching security policy");
            return Result<SecurityPolicyDto>.Failure($"보안 정책 조회 중 오류가 발생했습니다: {ex.Message}");
        }
    }
}
