using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Interfaces;
using Sphere.Application.Common.Models;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Application.Features.Auth.Commands.Logout;

/// <summary>
/// Handler for LogoutCommand.
/// </summary>
public class LogoutCommandHandler : IRequestHandler<LogoutCommand, Result>
{
    private readonly IAuthRepository _authRepository;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly ILogger<LogoutCommandHandler> _logger;

    public LogoutCommandHandler(
        IAuthRepository authRepository,
        IJwtTokenService jwtTokenService,
        ILogger<LogoutCommandHandler> logger)
    {
        _authRepository = authRepository;
        _jwtTokenService = jwtTokenService;
        _logger = logger;
    }

    public async Task<Result> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Logout request for user {UserId}", request.UserId);

        if (!string.IsNullOrEmpty(request.RefreshToken))
        {
            await _jwtTokenService.RevokeTokenAsync(request.RefreshToken);
        }

        var session = await _authRepository.GetActiveSessionAsync(request.UserId, request.DivSeq, cancellationToken);
        if (session is not null)
        {
            await _authRepository.UpdateSessionActiveAsync(request.UserId, request.DivSeq, "N", cancellationToken);
        }

        _logger.LogInformation("Logout successful for user {UserId}", request.UserId);
        return Result.Success();
    }
}
