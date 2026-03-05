using System.Threading.RateLimiting;
using Microsoft.AspNetCore.RateLimiting;

namespace Sphere.Api.Extensions;

/// <summary>
/// Rate limiting configuration for API endpoints.
/// Protects against brute-force attacks and API abuse.
/// </summary>
public static class RateLimitingExtensions
{
    /// <summary>
    /// Adds custom rate limiting policies to the service collection.
    /// </summary>
    public static IServiceCollection AddCustomRateLimiting(this IServiceCollection services)
    {
        services.AddRateLimiter(options =>
        {
            // Global rate limit: 100 requests per minute per IP
            options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(context =>
                RateLimitPartition.GetFixedWindowLimiter(
                    partitionKey: GetClientIpAddress(context),
                    factory: _ => new FixedWindowRateLimiterOptions
                    {
                        PermitLimit = 100,
                        Window = TimeSpan.FromMinutes(1),
                        QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                        QueueLimit = 0
                    }));

            // Login-specific policy: 50 attempts per 1 minute per IP (relaxed for development)
            // Protects against brute-force login attacks
            options.AddPolicy("login", context =>
                RateLimitPartition.GetFixedWindowLimiter(
                    partitionKey: GetClientIpAddress(context),
                    factory: _ => new FixedWindowRateLimiterOptions
                    {
                        PermitLimit = 50,
                        Window = TimeSpan.FromMinutes(1),
                        QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                        QueueLimit = 0
                    }));

            // OTP-specific policy: 3 attempts per 5 minutes per user/IP
            // Protects against OTP brute-force attacks
            options.AddPolicy("otp", context =>
                RateLimitPartition.GetSlidingWindowLimiter(
                    partitionKey: GetPartitionKey(context),
                    factory: _ => new SlidingWindowRateLimiterOptions
                    {
                        PermitLimit = 3,
                        Window = TimeSpan.FromMinutes(5),
                        SegmentsPerWindow = 5,
                        QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                        QueueLimit = 0
                    }));

            // Password reset policy: 3 attempts per hour per IP
            // Protects against password reset abuse
            options.AddPolicy("password-reset", context =>
                RateLimitPartition.GetFixedWindowLimiter(
                    partitionKey: GetClientIpAddress(context),
                    factory: _ => new FixedWindowRateLimiterOptions
                    {
                        PermitLimit = 3,
                        Window = TimeSpan.FromHours(1),
                        QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                        QueueLimit = 0
                    }));

            // Handle rate limit exceeded
            options.OnRejected = async (context, cancellationToken) =>
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                context.HttpContext.Response.ContentType = "application/json";

                var retryAfter = context.Lease.TryGetMetadata(MetadataName.RetryAfter, out var retryAfterValue)
                    ? (int)retryAfterValue.TotalSeconds
                    : 60;

                context.HttpContext.Response.Headers.RetryAfter = retryAfter.ToString();

                await context.HttpContext.Response.WriteAsJsonAsync(new
                {
                    error = "Too many requests. Please try again later.",
                    retryAfter = retryAfter,
                    message = GetRateLimitMessage(context.HttpContext.Request.Path)
                }, cancellationToken);
            };
        });

        return services;
    }

    /// <summary>
    /// Gets the client IP address, considering X-Forwarded-For header for proxies.
    /// </summary>
    private static string GetClientIpAddress(HttpContext context)
    {
        // Check for X-Forwarded-For header (when behind a proxy/load balancer)
        var forwardedFor = context.Request.Headers["X-Forwarded-For"].FirstOrDefault();
        if (!string.IsNullOrEmpty(forwardedFor))
        {
            // Take the first IP in the chain (original client)
            var ip = forwardedFor.Split(',').FirstOrDefault()?.Trim();
            if (!string.IsNullOrEmpty(ip))
            {
                return ip;
            }
        }

        return context.Connection.RemoteIpAddress?.ToString() ?? "unknown";
    }

    /// <summary>
    /// Gets partition key combining user identity and IP address.
    /// </summary>
    private static string GetPartitionKey(HttpContext context)
    {
        var userId = context.User.Identity?.Name ?? "anonymous";
        var ip = GetClientIpAddress(context);
        return $"{userId}:{ip}";
    }

    /// <summary>
    /// Gets a user-friendly rate limit message based on the endpoint.
    /// </summary>
    private static string GetRateLimitMessage(PathString path)
    {
        return path.Value?.ToLower() switch
        {
            var p when p?.Contains("/login") == true =>
                "Too many login attempts. Please wait 15 minutes before trying again.",
            var p when p?.Contains("/otp") == true =>
                "Too many OTP verification attempts. Please wait 5 minutes before trying again.",
            var p when p?.Contains("/password") == true =>
                "Too many password reset attempts. Please wait 1 hour before trying again.",
            _ => "Rate limit exceeded. Please try again later."
        };
    }
}
