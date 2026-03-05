using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Interfaces;
using Sphere.Application.Common.Models;

namespace Sphere.Application.Features.Auth.Commands.Logout;

/// <summary>
/// Handler for LogoutCommand.
/// </summary>
public class LogoutCommandHandler : IRequestHandler<LogoutCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly ILogger<LogoutCommandHandler> _logger;

    public LogoutCommandHandler(
        IApplicationDbContext context,
        IJwtTokenService jwtTokenService,
        ILogger<LogoutCommandHandler> logger)
    {
        _context = context;
        _jwtTokenService = jwtTokenService;
        _logger = logger;
    }

    public async Task<Result> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Logout request for user {UserId}", request.UserId);

        // Revoke refresh token if provided
        if (!string.IsNullOrEmpty(request.RefreshToken))
        {
            await _jwtTokenService.RevokeTokenAsync(request.RefreshToken);
        }

        // Invalidate user session
        var session = await _context.UserSessions
            .Where(s => s.DivSeq == request.DivSeq && s.UserId == request.UserId && s.IsActive == "Y")
            .FirstOrDefaultAsync(cancellationToken);

        if (session is not null)
        {
            session.IsActive = "N";
            await _context.SaveChangesAsync(cancellationToken);
        }

        _logger.LogInformation("Logout successful for user {UserId}", request.UserId);
        return Result.Success();
    }
}
