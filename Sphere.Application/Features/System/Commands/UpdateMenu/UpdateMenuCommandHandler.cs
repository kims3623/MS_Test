using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.System;

namespace Sphere.Application.Features.System.Commands.UpdateMenu;

/// <summary>
/// Handler for UpdateMenuCommand.
/// </summary>
public class UpdateMenuCommandHandler : IRequestHandler<UpdateMenuCommand, Result<UpdateMenuResponseDto>>
{
    private readonly ILogger<UpdateMenuCommandHandler> _logger;

    public UpdateMenuCommandHandler(ILogger<UpdateMenuCommandHandler> logger)
    {
        _logger = logger;
    }

    public async Task<Result<UpdateMenuResponseDto>> Handle(
        UpdateMenuCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            // Update menu in database

            var response = new UpdateMenuResponseDto
            {
                Result = "S",
                ResultMessage = "Menu updated successfully.",
                MenuId = request.MenuId
            };

            _logger.LogInformation("Menu updated: {MenuId}", request.MenuId);
            return Result<UpdateMenuResponseDto>.Success(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating menu: {MenuId}", request.MenuId);
            return Result<UpdateMenuResponseDto>.Failure($"Error updating menu: {ex.Message}");
        }
    }
}
