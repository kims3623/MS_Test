namespace Sphere.Application.DTOs.Alarm;

/// <summary>
/// Alarm action user DTO.
/// </summary>
public class AlarmActionUserDto
{
    public string AlmActionId { get; set; } = string.Empty;
    public string AlarmActionUserId { get; set; } = string.Empty;
}

/// <summary>
/// Alarm action user query DTO.
/// </summary>
public class AlarmActionUserQueryDto
{
    public string DivSeq { get; set; } = string.Empty;
    public string? AlmProcId { get; set; }
    public string? VendorId { get; set; }
    public string? MtrlClassId { get; set; }
}

/// <summary>
/// Alarm issue first action DTO.
/// </summary>
public class AlarmIssueFirstActionDto
{
    public string TemplateType { get; set; } = string.Empty;
    public string ChgTypeId { get; set; } = string.Empty;
    public string AlmSysId { get; set; } = string.Empty;
    public string AlmActionId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Contents { get; set; } = string.Empty;
    public string Reciver { get; set; } = string.Empty;
}

/// <summary>
/// Alarm action DTO for StopAlarmPopup.
/// </summary>
public class AlarmActionDto
{
    public string ActionId { get; set; } = string.Empty;
    public string ActionName { get; set; } = string.Empty;
    public string ActionNameK { get; set; } = string.Empty;
    public string? ActionNameE { get; set; }
    public int Seq { get; set; }
    public bool IsActive { get; set; } = true;
}

/// <summary>
/// Close/Stop alarm request DTO.
/// </summary>
public class CloseAlarmRequestDto
{
    public string AlarmSysId { get; set; } = string.Empty;
    public string ActionId { get; set; } = string.Empty;
    public string StopReason { get; set; } = string.Empty;
    public List<string>? CustomerIds { get; set; }
    public string UserId { get; set; } = string.Empty;
}

/// <summary>
/// Close/Stop alarm response DTO.
/// </summary>
public class CloseAlarmResponseDto
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public string AlarmSysId { get; set; } = string.Empty;
    public string NewStatus { get; set; } = string.Empty;
}
