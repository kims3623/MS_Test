using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.Master;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Application.Features.Master.Queries.GetProjectMaster;

public class GetProjectMasterQueryHandler : IRequestHandler<GetProjectMasterQuery, Result<ProjectMasterListDto>>
{
    private readonly IProjectMasterRepository _repository;
    private readonly ILogger<GetProjectMasterQueryHandler> _logger;

    public GetProjectMasterQueryHandler(IProjectMasterRepository repository, ILogger<GetProjectMasterQueryHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<ProjectMasterListDto>> Handle(GetProjectMasterQuery request, CancellationToken cancellationToken)
    {
        _logger.LogDebug("Getting project master list for DivSeq {DivSeq}", request.DivSeq);
        try
        {
            var filter = new ProjectMasterFilterDto
            {
                CustomerId = request.CustomerId, Status = request.Status,
                UseYn = request.UseYn, SearchText = request.SearchText
            };
            var items = (await _repository.GetProjectMasterListAsync(request.DivSeq, filter, cancellationToken)).ToList();
            return Result<ProjectMasterListDto>.Success(new ProjectMasterListDto { Items = items, TotalCount = items.Count });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting project master list");
            return Result<ProjectMasterListDto>.Failure("Failed to retrieve project master list.");
        }
    }
}
