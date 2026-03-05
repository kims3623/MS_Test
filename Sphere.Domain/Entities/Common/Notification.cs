using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.Common;

/// <summary>
/// Notification entity for managing user notifications and alerts.
/// Maps to SPC_NOTIFICATION table.
/// </summary>
/// <remarks>
/// Composite PK: DivSeq + NotiId
/// Supports various notification types with reference to related entities.
/// Tracks read status and priority for notification management.
/// </remarks>
public class Notification : SphereEntity
{
    /// <summary>
    /// Notification identifier (PK)
    /// </summary>
    public string NotiId { get; set; } = string.Empty;

    /// <summary>
    /// Target user identifier
    /// </summary>
    public string UserId { get; set; } = string.Empty;

    /// <summary>
    /// Notification type (ALARM, INFO, WARNING, etc.)
    /// </summary>
    public string NotiType { get; set; } = string.Empty;

    /// <summary>
    /// Notification title
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Notification message content
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Related entity type for navigation
    /// </summary>
    public string RefType { get; set; } = string.Empty;

    /// <summary>
    /// Related entity identifier for navigation
    /// </summary>
    public string RefId { get; set; } = string.Empty;

    /// <summary>
    /// Read status flag (Y/N)
    /// </summary>
    public string ReadYn { get; set; } = "N";

    /// <summary>
    /// Timestamp when notification was read
    /// </summary>
    public DateTime? ReadDate { get; set; }

    /// <summary>
    /// Timestamp when notification was sent
    /// </summary>
    public DateTime? SentDate { get; set; }

    /// <summary>
    /// Priority level (1=High, 2=Medium, 3=Low)
    /// </summary>
    public int Priority { get; set; }
}
