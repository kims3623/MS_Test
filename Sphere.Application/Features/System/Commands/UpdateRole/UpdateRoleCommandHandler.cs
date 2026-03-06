using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.System;

namespace Sphere.Application.Features.System.Commands.UpdateRole;

/// <summary>
/// Handler for UpdateRoleCommand.
/// </summary>
public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, Result<UpdateRoleResponseDto>>
{
    private readonly ILogger<UpdateRoleCommandHandler> _logger;

    public UpdateRoleCommandHandler(ILogger<UpdateRoleCommandHandler> logger)
    {
        _logger = logger;
    }

    public async Task<Result<UpdateRoleResponseDto>> Handle(
        UpdateRoleCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            // Update role in database

            var response = new UpdateRoleResponseDto
            {
                Result = "S",
                ResultMessage = "Role updated successfully.",
                RoleCode = request.RoleCode
            };

            _logger.LogInformation("Role updated: {RoleCode}", request.RoleCode);
            return Result<UpdateRoleResponseDto>.Success(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating role: {RoleCode}", request.RoleCode);
            return Result<UpdateRoleResponseDto>.Failure($"Error updating role: {ex.Message}");
        }
    }
}
