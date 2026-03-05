using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.System;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Application.Features.System.Queries.GetMenuTree;

/// <summary>
/// Handler for GetMenuTreeQuery.
/// </summary>
public class GetMenuTreeQueryHandler : IRequestHandler<GetMenuTreeQuery, Result<MenuTreeResponseDto>>
{
    private readonly ISystemRepository _systemRepository;
    private readonly ILogger<GetMenuTreeQueryHandler> _logger;

    public GetMenuTreeQueryHandler(
        ISystemRepository systemRepository,
        ILogger<GetMenuTreeQueryHandler> logger)
    {
        _systemRepository = systemRepository;
        _logger = logger;
    }

    public async Task<Result<MenuTreeResponseDto>> Handle(
        GetMenuTreeQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var filter = new MenuListFilterDto
            {
                DivSeq = request.DivSeq,
                ParentMenuId = request.ParentMenuId,
                MenuType = request.MenuType,
                IsActive = request.IsActive,
                Language = request.Language
            };

            var response = await _systemRepository.GetMenuTreeAsync(filter, cancellationToken);

            _logger.LogInformation("Menu tree retrieved: {Count} items", response.Items.Count);
            return Result<MenuTreeResponseDto>.Success(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving menu tree");
            return Result<MenuTreeResponseDto>.Failure($"Error retrieving menu tree: {ex.Message}");
        }
    }
}
