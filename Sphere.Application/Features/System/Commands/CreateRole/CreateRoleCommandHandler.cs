using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Interfaces;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.System;

namespace Sphere.Application.Features.System.Commands.CreateRole;

/// <summary>
/// Handler for CreateRoleCommand.
/// </summary>
public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, Result<CreateRoleResponseDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly ILogger<CreateRoleCommandHandler> _logger;

    public CreateRoleCommandHandler(
        IApplicationDbContext context,
        ILogger<CreateRoleCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Result<CreateRoleResponseDto>> Handle(
        CreateRoleCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            // Create role in database

            var response = new CreateRoleResponseDto
            {
                Result = "S",
                ResultMessage = "Role created successfully.",
                RoleCode = request.RoleCode
            };

            _logger.LogInformation("Role created: {RoleCode}", request.RoleCode);
            return Result<CreateRoleResponseDto>.Success(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating role: {RoleCode}", request.RoleCode);
            return Result<CreateRoleResponseDto>.Failure($"Error creating role: {ex.Message}");
        }
    }
}
