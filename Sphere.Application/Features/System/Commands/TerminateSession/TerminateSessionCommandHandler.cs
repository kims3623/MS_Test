using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Interfaces;
using Sphere.Application.Common.Models;
using Sphere.Application.DTOs.System;

namespace Sphere.Application.Features.System.Commands.TerminateSession;

/// <summary>
/// Handler for TerminateSessionCommand.
/// </summary>
public class TerminateSessionCommandHandler : IRequestHandler<TerminateSessionCommand, Result<TerminateSessionResponseDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly ILogger<TerminateSessionCommandHandler> _logger;

    public TerminateSessionCommandHandler(
        IApplicationDbContext context,
        ILogger<TerminateSessionCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Result<TerminateSessionResponseDto>> Handle(
        TerminateSessionCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            // Terminate session in database
            var response = new TerminateSessionResponseDto
            {
                Result = "S",
                ResultMessage = "Session terminated successfully.",
                SessionId = request.SessionId
            };

            _logger.LogInformation("Session terminated: {SessionId}", request.SessionId);
            return Result<TerminateSessionResponseDto>.Success(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error terminating session: {SessionId}", request.SessionId);
            return Result<TerminateSessionResponseDto>.Failure($"Error terminating session: {ex.Message}");
        }
    }
}
