using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.System;

/// <summary>
/// Error log entity for application error tracking.
/// </summary>
public class ErrorLog : SphereEntity
{
    public string ErrorId { get; set; } = string.Empty;
    public string ErrorType { get; set; } = string.Empty;
    public string ErrorMessage { get; set; } = string.Empty;
    public string StackTrace { get; set; } = string.Empty;
    public string Source { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public string IpAddress { get; set; } = string.Empty;
    public DateTime? ErrorDate { get; set; }
    public string Severity { get; set; } = "ERROR";
    public string ResolvedYn { get; set; } = "N";
}
