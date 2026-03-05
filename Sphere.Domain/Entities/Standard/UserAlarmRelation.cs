using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.Standard;

/// <summary>
/// User alarm relation entity for mapping users to alarm notifications.
/// Maps to SPC_USER_ALARM_REL table.
/// </summary>
public class UserAlarmRelation : SphereEntity
{
    /// <summary>
    /// User identifier (PK)
    /// </summary>
    public string UserId { get; set; } = string.Empty;

    /// <summary>
    /// Alarm type identifier (PK)
    /// </summary>
    public string AlarmTypeId { get; set; } = string.Empty;

    /// <summary>
    /// Specification system identifier (PK)
    /// </summary>
    public string SpecSysId { get; set; } = string.Empty;

    /// <summary>
    /// User name
    /// </summary>
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// Alarm type name
    /// </summary>
    public string AlarmTypeName { get; set; } = string.Empty;

    /// <summary>
    /// Email notification flag
    /// </summary>
    public string EmailYn { get; set; } = "Y";

    /// <summary>
    /// SMS notification flag
    /// </summary>
    public string SmsYn { get; set; } = "N";

    /// <summary>
    /// Push notification flag
    /// </summary>
    public string PushYn { get; set; } = "Y";

    /// <summary>
    /// Priority level
    /// </summary>
    public int Priority { get; set; }
}
