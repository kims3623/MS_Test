namespace Sphere.Application.Common.Models;

/// <summary>
/// Standardized API response wrapper for consistent response structure
/// </summary>
/// <typeparam name="T">The type of data being returned</typeparam>
public class ApiResponse<T>
{
    /// <summary>
    /// Indicates whether the operation was successful
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// HTTP status code
    /// </summary>
    public int StatusCode { get; set; }

    /// <summary>
    /// Response message (success message or error description)
    /// </summary>
    public string? Message { get; set; }

    /// <summary>
    /// The response data (null on failure)
    /// </summary>
    public T? Data { get; set; }

    /// <summary>
    /// List of validation errors or error details
    /// </summary>
    public IDictionary<string, string[]>? Errors { get; set; }

    /// <summary>
    /// Timestamp of the response
    /// </summary>
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Trace ID for request tracking
    /// </summary>
    public string? TraceId { get; set; }

    /// <summary>
    /// Creates a successful response with data
    /// </summary>
    public static ApiResponse<T> Ok(T data, string? message = null)
    {
        return new ApiResponse<T>
        {
            Success = true,
            StatusCode = 200,
            Message = message ?? "Success",
            Data = data
        };
    }

    /// <summary>
    /// Creates a created response (201)
    /// </summary>
    public static ApiResponse<T> Created(T data, string? message = null)
    {
        return new ApiResponse<T>
        {
            Success = true,
            StatusCode = 201,
            Message = message ?? "Created successfully",
            Data = data
        };
    }

    /// <summary>
    /// Creates a no content response (204)
    /// </summary>
    public static ApiResponse<T> NoContent(string? message = null)
    {
        return new ApiResponse<T>
        {
            Success = true,
            StatusCode = 204,
            Message = message ?? "No content"
        };
    }

    /// <summary>
    /// Creates a bad request response (400)
    /// </summary>
    public static ApiResponse<T> BadRequest(string message, IDictionary<string, string[]>? errors = null)
    {
        return new ApiResponse<T>
        {
            Success = false,
            StatusCode = 400,
            Message = message,
            Errors = errors
        };
    }

    /// <summary>
    /// Creates an unauthorized response (401)
    /// </summary>
    public static ApiResponse<T> Unauthorized(string? message = null)
    {
        return new ApiResponse<T>
        {
            Success = false,
            StatusCode = 401,
            Message = message ?? "Unauthorized"
        };
    }

    /// <summary>
    /// Creates a forbidden response (403)
    /// </summary>
    public static ApiResponse<T> Forbidden(string? message = null)
    {
        return new ApiResponse<T>
        {
            Success = false,
            StatusCode = 403,
            Message = message ?? "Forbidden"
        };
    }

    /// <summary>
    /// Creates a not found response (404)
    /// </summary>
    public static ApiResponse<T> NotFound(string? message = null)
    {
        return new ApiResponse<T>
        {
            Success = false,
            StatusCode = 404,
            Message = message ?? "Not found"
        };
    }

    /// <summary>
    /// Creates an internal server error response (500)
    /// </summary>
    public static ApiResponse<T> InternalError(string? message = null, string? traceId = null)
    {
        return new ApiResponse<T>
        {
            Success = false,
            StatusCode = 500,
            Message = message ?? "An unexpected error occurred",
            TraceId = traceId
        };
    }
}

/// <summary>
/// Non-generic API response for operations without data
/// </summary>
public class ApiResponse
{
    /// <summary>
    /// Indicates whether the operation was successful
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// HTTP status code
    /// </summary>
    public int StatusCode { get; set; }

    /// <summary>
    /// Response message (success message or error description)
    /// </summary>
    public string? Message { get; set; }

    /// <summary>
    /// List of validation errors or error details
    /// </summary>
    public IDictionary<string, string[]>? Errors { get; set; }

    /// <summary>
    /// Timestamp of the response
    /// </summary>
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Trace ID for request tracking
    /// </summary>
    public string? TraceId { get; set; }

    /// <summary>
    /// Creates a successful response without data
    /// </summary>
    public static ApiResponse Ok(string? message = null)
    {
        return new ApiResponse
        {
            Success = true,
            StatusCode = 200,
            Message = message ?? "Success"
        };
    }

    /// <summary>
    /// Creates an error response
    /// </summary>
    public static ApiResponse Error(int statusCode, string message, IDictionary<string, string[]>? errors = null)
    {
        return new ApiResponse
        {
            Success = false,
            StatusCode = statusCode,
            Message = message,
            Errors = errors
        };
    }

    /// <summary>
    /// Creates a bad request response (400)
    /// </summary>
    public static ApiResponse BadRequest(string message, IDictionary<string, string[]>? errors = null)
    {
        return new ApiResponse
        {
            Success = false,
            StatusCode = 400,
            Message = message,
            Errors = errors
        };
    }

    /// <summary>
    /// Creates an unauthorized response (401)
    /// </summary>
    public static ApiResponse Unauthorized(string? message = null)
    {
        return new ApiResponse
        {
            Success = false,
            StatusCode = 401,
            Message = message ?? "Unauthorized"
        };
    }

    /// <summary>
    /// Creates a not found response (404)
    /// </summary>
    public static ApiResponse NotFound(string? message = null)
    {
        return new ApiResponse
        {
            Success = false,
            StatusCode = 404,
            Message = message ?? "Not found"
        };
    }

    /// <summary>
    /// Creates an internal server error response (500)
    /// </summary>
    public static ApiResponse InternalError(string? message = null, string? traceId = null)
    {
        return new ApiResponse
        {
            Success = false,
            StatusCode = 500,
            Message = message ?? "An unexpected error occurred",
            TraceId = traceId
        };
    }
}
