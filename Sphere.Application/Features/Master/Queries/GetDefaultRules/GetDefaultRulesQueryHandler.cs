using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Master;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Application.Features.Master.Queries.GetDefaultRules;

public class GetDefaultRulesQueryHandler : IRequestHandler<GetDefaultRulesQuery, Result<DefaultRuleListDto>>
{
    private readonly IDefaultRuleRepository _repository;
    private readonly ILogger<GetDefaultRulesQueryHandler> _logger;

    public GetDefaultRulesQueryHandler(IDefaultRuleRepository repository, ILogger<GetDefaultRulesQueryHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<DefaultRuleListDto>> Handle(GetDefaultRulesQuery request, CancellationToken cancellationToken)
    {
        _logger.LogDebug("Getting default rules for DivSeq {DivSeq}", request.DivSeq);
        try
        {
            var filter = new DefaultRuleFilterDto
            {
                RuleType = request.RuleType, TargetType = request.TargetType,
                UseYn = request.UseYn, SearchText = request.SearchText
            };
            var items = (await _repository.GetDefaultRuleListAsync(request.DivSeq, filter, cancellationToken)).ToList();
            return Result<DefaultRuleListDto>.Success(new DefaultRuleListDto { Items = items, TotalCount = items.Count });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting default rules");
            return Result<DefaultRuleListDto>.Failure("Failed to retrieve default rules.");
        }
    }
}
