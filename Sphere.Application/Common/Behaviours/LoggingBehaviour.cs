using MediatR;
using Microsoft.Extensions.Logging;
using Sphere.Application.Common.Interfaces;

namespace Sphere.Application.Common.Behaviours;

/// <summary>
/// MediatR pipeline behaviour for request logging
/// </summary>
public class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly ILogger<LoggingBehaviour<TRequest, TResponse>> _logger;
    private readonly ICurrentUserService _currentUserService;

    public LoggingBehaviour(
        ILogger<LoggingBehaviour<TRequest, TResponse>> logger,
        ICurrentUserService currentUserService)
    {
        _logger = logger;
        _currentUserService = currentUserService;
    }

    /// <summary>
    /// Command names that contain sensitive data and should not be logged in full.
    /// </summary>
    private static readonly HashSet<string> SensitiveCommands = new(StringComparer.OrdinalIgnoreCase)
    {
        "LoginCommand", "ChangePasswordCommand", "ResetPasswordCommand",
        "ValidateCredentialsCommand", "VerifyOtpCommand"
    };

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        var userId = _currentUserService.UserId ?? "Anonymous";
        var userName = _currentUserService.UserName ?? "Unknown";

        if (SensitiveCommands.Contains(requestName))
        {
            _logger.LogInformation(
                "Sphere Request: {Name} {@UserId} {@UserName} [sensitive payload masked]",
                requestName, userId, userName);
        }
        else
        {
            _logger.LogInformation(
                "Sphere Request: {Name} {@UserId} {@UserName} {@Request}",
                requestName, userId, userName, request);
        }

        var response = await next();

        _logger.LogInformation(
            "Sphere Response: {Name} {@UserId} completed",
            requestName, userId);

        return response;
    }
}
