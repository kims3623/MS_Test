namespace Sphere.Application.DTOs.System;

#region Audit Log DTOs

/// <summary>
/// Audit log filter DTO.
/// </summary>
public class AuditLogFilterDto
{
    public string DivSeq { get; set; } = string.Empty;
    public string? StartDate { get; set; }
    public string? EndDate { get; set; }
    public string? UserId { get; set; }
    public string? ActionType { get; set; }
    public string? TargetType { get; set; }
    public string? TargetId { get; set; }
    public string? IpAddress { get; set; }
    public string? Keyword { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 50;
}

/// <summary>
/// Audit log item DTO.
/// </summary>
public class AuditLogItemDto
{
    public long LogId { get; set; }
    public string LogDate { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string ActionType { get; set; } = string.Empty;
    public string ActionTypeName { get; set; } = string.Empty;
    public string TargetType { get; set; } = string.Empty;
    public string TargetTypeName { get; set; } = string.Empty;
    public string TargetId { get; set; } = string.Empty;
    public string TargetName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string IpAddress { get; set; } = string.Empty;
    public string UserAgent { get; set; } = string.Empty;
    public string RequestUrl { get; set; } = string.Empty;
    public string RequestMethod { get; set; } = string.Empty;
    public string ResponseStatus { get; set; } = string.Empty;
    public int? ResponseTime { get; set; }
}

/// <summary>
/// Audit log response with pagination.
/// </summary>
public class AuditLogResponseDto
{
    public List<AuditLogItemDto> Items { get; set; } = new();
    public int TotalCount { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages => PageSize > 0 ? (int)Math.Ceiling((double)TotalCount / PageSize) : 0;
}

/// <summary>
/// Audit log detail DTO.
/// </summary>
public class AuditLogDetailDto
{
    public long LogId { get; set; }
    public string LogDate { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string ActionType { get; set; } = string.Empty;
    public string ActionTypeName { get; set; } = string.Empty;
    public string TargetType { get; set; } = string.Empty;
    public string TargetTypeName { get; set; } = string.Empty;
    public string TargetId { get; set; } = string.Empty;
    public string TargetName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string IpAddress { get; set; } = string.Empty;
    public string UserAgent { get; set; } = string.Empty;
    public string RequestUrl { get; set; } = string.Empty;
    public string RequestMethod { get; set; } = string.Empty;
    public string RequestBody { get; set; } = string.Empty;
    public string ResponseStatus { get; set; } = string.Empty;
    public string ResponseBody { get; set; } = string.Empty;
    public int? ResponseTime { get; set; }
    public string OldValue { get; set; } = string.Empty;
    public string NewValue { get; set; } = string.Empty;
}

/// <summary>
/// Export audit log request DTO.
/// </summary>
public class ExportAuditLogRequestDto
{
    public string DivSeq { get; set; } = string.Empty;
    public string? StartDate { get; set; }
    public string? EndDate { get; set; }
    public string? UserId { get; set; }
    public string? ActionType { get; set; }
    public string? TargetType { get; set; }
    public string ExportFormat { get; set; } = "xlsx";
    public string RequestUserId { get; set; } = string.Empty;
}

/// <summary>
/// Export audit log response DTO.
/// </summary>
public class ExportAuditLogResponseDto
{
    public string Result { get; set; } = string.Empty;
    public string ResultMessage { get; set; } = string.Empty;
    public string FileName { get; set; } = string.Empty;
    public string FileUrl { get; set; } = string.Empty;
    public int ExportedCount { get; set; }
}

#endregion

#region Login History DTOs

/// <summary>
/// Login history filter DTO.
/// </summary>
public class LoginHistoryFilterDto
{
    public string DivSeq { get; set; } = string.Empty;
    public string? StartDate { get; set; }
    public string? EndDate { get; set; }
    public string? UserId { get; set; }
    public string? LoginResult { get; set; }
    public string? IpAddress { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 50;
}

/// <summary>
/// Login history item DTO.
/// </summary>
public class LoginHistoryItemDto
{
    public long HistoryId { get; set; }
    public string LoginDate { get; set; } = string.Empty;
    public string LogoutDate { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string LoginResult { get; set; } = string.Empty;
    public string LoginResultName { get; set; } = string.Empty;
    public string FailReason { get; set; } = string.Empty;
    public string IpAddress { get; set; } = string.Empty;
    public string UserAgent { get; set; } = string.Empty;
    public string Browser { get; set; } = string.Empty;
    public string Os { get; set; } = string.Empty;
    public string Device { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public int? SessionDuration { get; set; }
}

/// <summary>
/// Login history response with pagination.
/// </summary>
public class LoginHistoryResponseDto
{
    public List<LoginHistoryItemDto> Items { get; set; } = new();
    public int TotalCount { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages => PageSize > 0 ? (int)Math.Ceiling((double)TotalCount / PageSize) : 0;
}

/// <summary>
/// Login statistics DTO.
/// </summary>
public class LoginStatisticsDto
{
    public int TotalLogins { get; set; }
    public int SuccessLogins { get; set; }
    public int FailedLogins { get; set; }
    public int UniqueUsers { get; set; }
    public double SuccessRate { get; set; }
    public List<LoginStatByDateDto> ByDate { get; set; } = new();
    public List<LoginStatByHourDto> ByHour { get; set; } = new();
}

/// <summary>
/// Login statistics by date DTO.
/// </summary>
public class LoginStatByDateDto
{
    public string Date { get; set; } = string.Empty;
    public int SuccessCount { get; set; }
    public int FailedCount { get; set; }
}

/// <summary>
/// Login statistics by hour DTO.
/// </summary>
public class LoginStatByHourDto
{
    public int Hour { get; set; }
    public int LoginCount { get; set; }
}

#endregion
