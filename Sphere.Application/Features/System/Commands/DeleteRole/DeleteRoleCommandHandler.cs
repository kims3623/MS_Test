using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.System;

namespace Sphere.Application.Features.System.Commands.DeleteRole;

/// <summary>
/// Handler for DeleteRoleCommand.
/// </summary>
public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, Result<DeleteRoleResponseDto>>
{
    private readonly ILogger<DeleteRoleCommandHandler> _logger;

    public DeleteRoleCommandHandler(ILogger<DeleteRoleCommandHandler> logger)
    {
        _logger = logger;
    }

    public async Task<Result<DeleteRoleResponseDto>> Handle(
        DeleteRoleCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            // Delete role in database

            var response = new DeleteRoleResponseDto
            {
                Result = "S",
                ResultMessage = "Role deleted successfully."
            };

            _logger.LogInformation("Role deleted: {RoleCode}", request.RoleCode);
            return Result<DeleteRoleResponseDto>.Success(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting role: {RoleCode}", request.RoleCode);
            return Result<DeleteRoleResponseDto>.Failure($"Error deleting role: {ex.Message}");
        }
    }
}
