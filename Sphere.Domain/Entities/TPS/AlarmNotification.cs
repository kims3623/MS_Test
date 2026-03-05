using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.TPS;

/// <summary>
/// Alarm notification entity for tracking alarm notifications.
/// </summary>
public class AlarmNotification : SphereEntity
{
    public string NotiId { get; set; } = string.Empty;
    public string AlmSysId { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string NotiType { get; set; } = "EMAIL";
    public string Status { get; set; } = "PENDING";
    public DateTime? SentDate { get; set; }
    public string ErrorMessage { get; set; } = string.Empty;
    public int RetryCount { get; set; }
}
