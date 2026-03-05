using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.SPC;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Application.Features.SPC.Queries.GetRunRules;

/// <summary>
/// Handler for GetRunRulesQuery.
/// </summary>
public class GetRunRulesQueryHandler : IRequestHandler<GetRunRulesQuery, Result<RunRulesConfigDto>>
{
    private readonly ISPCRepository _repository;
    private readonly ILogger<GetRunRulesQueryHandler> _logger;

    public GetRunRulesQueryHandler(
        ISPCRepository repository,
        ILogger<GetRunRulesQueryHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<RunRulesConfigDto>> Handle(
        GetRunRulesQuery request,
        CancellationToken cancellationToken)
    {
        _logger.LogDebug("Getting run rules for SpecSysId {SpecSysId}", request.SpecSysId);

        try
        {
            var data = await _repository.GetRunRulesAsync(request.SpecSysId, cancellationToken);
            return Result<RunRulesConfigDto>.Success(data);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting run rules for SpecSysId {SpecSysId}", request.SpecSysId);
            return Result<RunRulesConfigDto>.Failure("Failed to retrieve run rules configuration.");
        }
    }
}
