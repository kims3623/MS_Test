namespace Sphere.Api.Middleware;

/// <summary>
/// Middleware that adds security-related HTTP headers to all responses.
/// Implements OWASP recommended security headers.
/// </summary>
public class SecurityHeadersMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IWebHostEnvironment _env;

    public SecurityHeadersMiddleware(RequestDelegate next, IWebHostEnvironment env)
    {
        _next = next;
        _env = env;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Prevent MIME type sniffing
        // Browsers should trust the Content-Type header and not try to detect content type
        context.Response.Headers.Append("X-Content-Type-Options", "nosniff");

        // Prevent clickjacking attacks
        // Page cannot be displayed in a frame, regardless of the site attempting to do so
        context.Response.Headers.Append("X-Frame-Options", "DENY");

        // XSS Protection for legacy browsers
        // Modern browsers use CSP, but this provides fallback protection
        context.Response.Headers.Append("X-XSS-Protection", "1; mode=block");

        // Control referrer information sent with requests
        // Only send origin for cross-origin requests, full URL for same-origin
        context.Response.Headers.Append("Referrer-Policy", "strict-origin-when-cross-origin");

        // Content Security Policy
        // Controls which resources the browser is allowed to load
        context.Response.Headers.Append("Content-Security-Policy",
            "default-src 'self'; " +
            "script-src 'self' 'unsafe-inline' 'unsafe-eval'; " +
            "style-src 'self' 'unsafe-inline'; " +
            "img-src 'self' data: https: blob:; " +
            "font-src 'self' data:; " +
            "connect-src 'self' https:; " +
            "frame-ancestors 'none'; " +
            "form-action 'self'; " +
            "base-uri 'self'; " +
            "object-src 'none';");

        // HTTP Strict Transport Security (HSTS)
        // Forces browsers to use HTTPS for all future requests
        // Only applied in production to avoid issues with local development
        if (!_env.IsDevelopment())
        {
            // max-age: 1 year, includeSubDomains, preload
            context.Response.Headers.Append("Strict-Transport-Security",
                "max-age=31536000; includeSubDomains; preload");
        }

        // Permissions Policy (formerly Feature Policy)
        // Restricts which browser features can be used
        context.Response.Headers.Append("Permissions-Policy",
            "accelerometer=(), " +
            "camera=(), " +
            "geolocation=(), " +
            "gyroscope=(), " +
            "magnetometer=(), " +
            "microphone=(), " +
            "payment=(), " +
            "usb=(), " +
            "interest-cohort=()");

        // Cache control for sensitive data
        // Prevents caching of authenticated responses
        if (context.User.Identity?.IsAuthenticated == true)
        {
            context.Response.Headers.Append("Cache-Control", "no-store, no-cache, must-revalidate");
            context.Response.Headers.Append("Pragma", "no-cache");
        }

        await _next(context);
    }
}

/// <summary>
/// Extension methods for adding security headers middleware.
/// </summary>
public static class SecurityHeadersExtensions
{
    /// <summary>
    /// Adds security headers middleware to the application pipeline.
    /// </summary>
    /// <param name="app">The application builder.</param>
    /// <returns>The application builder for chaining.</returns>
    public static IApplicationBuilder UseSecurityHeaders(this IApplicationBuilder app)
    {
        return app.UseMiddleware<SecurityHeadersMiddleware>();
    }
}
