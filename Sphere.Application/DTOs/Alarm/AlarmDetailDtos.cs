namespace Sphere.Application.DTOs.Alarm;

/// <summary>
/// Alarm detail DTO with full information.
/// </summary>
public class AlarmDetailDto
{
    public string AlmSysId { get; set; } = string.Empty;
    public string AlmNo { get; set; } = string.Empty;
    public string DivSeq { get; set; } = string.Empty;
    public string AlmProcId { get; set; } = string.Empty;
    public string AlmProcName { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Contents { get; set; } = string.Empty;
    public string CurAlmActionId { get; set; } = string.Empty;
    public string CurAlmActionName { get; set; } = string.Empty;
    public string AlmProcYn { get; set; } = string.Empty;
    public string StopYn { get; set; } = string.Empty;
    public string VendorId { get; set; } = string.Empty;
    public string VendorName { get; set; } = string.Empty;
    public string MtrlClassId { get; set; } = string.Empty;
    public string MtrlClassName { get; set; } = string.Empty;
    public string SpecSysId { get; set; } = string.Empty;
    public string SpecName { get; set; } = string.Empty;
    public string Severity { get; set; } = string.Empty;
    public string SeverityName { get; set; } = string.Empty;
    public string StopReason { get; set; } = string.Empty;
    public string StopDate { get; set; } = string.Empty;
    public string StopUserId { get; set; } = string.Empty;
    public string StopUserName { get; set; } = string.Empty;
    public string CreateDate { get; set; } = string.Empty;
    public string CreateUserId { get; set; } = string.Empty;
    public string CreateUserName { get; set; } = string.Empty;
    public string UpdateDate { get; set; } = string.Empty;
    public string UpdateUserId { get; set; } = string.Empty;
    public string UpdateUserName { get; set; } = string.Empty;
    public string UseYn { get; set; } = "Y";

    /// <summary>
    /// Available actions for this alarm.
    /// </summary>
    public List<AlarmActionDto> Actions { get; set; } = new();

    /// <summary>
    /// Action history count.
    /// </summary>
    public int HistoryCount { get; set; }

    /// <summary>
    /// Attachment count.
    /// </summary>
    public int AttachmentCount { get; set; }
}

/// <summary>
/// Alarm detail query parameters.
/// </summary>
public class AlarmDetailQueryDto
{
    public string DivSeq { get; set; } = string.Empty;
    public string AlmSysId { get; set; } = string.Empty;
}

/// <summary>
/// Alarm merge request DTO for combining multiple alarms.
/// </summary>
public class MergeAlarmRequestDto
{
    public string DivSeq { get; set; } = string.Empty;
    public List<string> AlmSysIds { get; set; } = new();
    public string TargetAlmSysId { get; set; } = string.Empty;
    public string MergeReason { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
}

/// <summary>
/// Alarm merge response DTO.
/// </summary>
public class MergeAlarmResponseDto
{
    public string Result { get; set; } = string.Empty;
    public string ResultMessage { get; set; } = string.Empty;
    public string TargetAlmSysId { get; set; } = string.Empty;
    public int MergedCount { get; set; }
}

/// <summary>
/// Execute alarm action request DTO.
/// </summary>
public class ExecuteAlarmActionRequestDto
{
    public string DivSeq { get; set; } = string.Empty;
    public string AlmSysId { get; set; } = string.Empty;
    public string AlmActionId { get; set; } = string.Empty;
    public string? Comment { get; set; }
    public string UserId { get; set; } = string.Empty;
    public List<string>? NotifyUserIds { get; set; }
}

/// <summary>
/// Execute alarm action response DTO.
/// </summary>
public class ExecuteAlarmActionResponseDto
{
    public string Result { get; set; } = string.Empty;
    public string ResultMessage { get; set; } = string.Empty;
    public string AlmSysId { get; set; } = string.Empty;
    public string NewActionId { get; set; } = string.Empty;
    public string NewActionName { get; set; } = string.Empty;
}
