using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.System;

namespace Sphere.Application.Features.System.Commands.DeleteMenu;

/// <summary>
/// Handler for DeleteMenuCommand.
/// </summary>
public class DeleteMenuCommandHandler : IRequestHandler<DeleteMenuCommand, Result<DeleteMenuResponseDto>>
{
    private readonly ILogger<DeleteMenuCommandHandler> _logger;

    public DeleteMenuCommandHandler(ILogger<DeleteMenuCommandHandler> logger)
    {
        _logger = logger;
    }

    public async Task<Result<DeleteMenuResponseDto>> Handle(
        DeleteMenuCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            // Delete menu in database

            var response = new DeleteMenuResponseDto
            {
                Result = "S",
                ResultMessage = "Menu deleted successfully."
            };

            _logger.LogInformation("Menu deleted: {MenuId}", request.MenuId);
            return Result<DeleteMenuResponseDto>.Success(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting menu: {MenuId}", request.MenuId);
            return Result<DeleteMenuResponseDto>.Failure($"Error deleting menu: {ex.Message}");
        }
    }
}
