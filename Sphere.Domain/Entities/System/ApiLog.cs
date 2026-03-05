using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.System;

/// <summary>
/// API log entity for tracking API calls.
/// </summary>
public class ApiLog : SphereEntity
{
    public string LogId { get; set; } = string.Empty;
    public string Endpoint { get; set; } = string.Empty;
    public string Method { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string RequestBody { get; set; } = string.Empty;
    public string ResponseBody { get; set; } = string.Empty;
    public int StatusCode { get; set; }
    public long DurationMs { get; set; }
    public string IpAddress { get; set; } = string.Empty;
    public DateTime? RequestDate { get; set; }
    public string ErrorMessage { get; set; } = string.Empty;
}
