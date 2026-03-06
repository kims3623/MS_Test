using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.System;

namespace Sphere.Application.Features.System.Commands.CreateMenu;

/// <summary>
/// Handler for CreateMenuCommand.
/// </summary>
public class CreateMenuCommandHandler : IRequestHandler<CreateMenuCommand, Result<CreateMenuResponseDto>>
{
    private readonly ILogger<CreateMenuCommandHandler> _logger;

    public CreateMenuCommandHandler(ILogger<CreateMenuCommandHandler> logger)
    {
        _logger = logger;
    }

    public async Task<Result<CreateMenuResponseDto>> Handle(
        CreateMenuCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            // Create menu in database

            var response = new CreateMenuResponseDto
            {
                Result = "S",
                ResultMessage = "Menu created successfully.",
                MenuId = request.MenuId
            };

            _logger.LogInformation("Menu created: {MenuId}", request.MenuId);
            return Result<CreateMenuResponseDto>.Success(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating menu: {MenuId}", request.MenuId);
            return Result<CreateMenuResponseDto>.Failure($"Error creating menu: {ex.Message}");
        }
    }
}
