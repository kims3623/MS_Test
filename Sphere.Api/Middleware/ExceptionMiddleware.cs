using System.Net;
using System.Text.Json;
using Sphere.Application.Common.Interfaces;
using Sphere.Domain.Exceptions;

namespace Sphere.Api.Middleware;

/// <summary>
/// Global exception handling middleware
/// </summary>
public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        if (exception is DomainException)
            _logger.LogWarning("Domain exception: {ExceptionType} - {Message}", exception.GetType().Name, exception.Message);
        else
            _logger.LogError(exception, "An unhandled exception occurred: {Message}", exception.Message);

        var response = context.Response;
        response.ContentType = "application/json";

        var localizer = context.RequestServices.GetService<IMessageLocalizer>();

        var (statusCode, message, errors) = exception switch
        {
            ValidationException validationEx => (
                (int)HttpStatusCode.BadRequest,
                localizer?.Get("validation.failed") ?? "Validation failed",
                validationEx.Errors),
            NotFoundException => (
                (int)HttpStatusCode.NotFound,
                exception.Message,
                (IDictionary<string, string[]>?)null),
            DomainException => (
                (int)HttpStatusCode.BadRequest,
                exception.Message,
                (IDictionary<string, string[]>?)null),
            _ => (
                (int)HttpStatusCode.InternalServerError,
                localizer?.Get("error.general") ?? "An error occurred while processing your request.",
                (IDictionary<string, string[]>?)null)
        };

        response.StatusCode = statusCode;

        var result = JsonSerializer.Serialize(new
        {
            status = statusCode,
            message,
            errors
        });

        await response.WriteAsync(result);
    }
}
