namespace Sphere.Application.DTOs.System;

#region Session DTOs

/// <summary>
/// Active session filter DTO.
/// </summary>
public class ActiveSessionFilterDto
{
    public string DivSeq { get; set; } = string.Empty;
    public string? UserId { get; set; }
    public string? IpAddress { get; set; }
    public string? SessionStatus { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 50;
}

/// <summary>
/// Active session item DTO.
/// </summary>
public class ActiveSessionItemDto
{
    public string SessionId { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string DeptName { get; set; } = string.Empty;
    public string RoleName { get; set; } = string.Empty;
    public string LoginDate { get; set; } = string.Empty;
    public string LastActivityDate { get; set; } = string.Empty;
    public string ExpirationDate { get; set; } = string.Empty;
    public string IpAddress { get; set; } = string.Empty;
    public string UserAgent { get; set; } = string.Empty;
    public string Browser { get; set; } = string.Empty;
    public string Os { get; set; } = string.Empty;
    public string Device { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string SessionStatus { get; set; } = string.Empty;
    public string SessionStatusName { get; set; } = string.Empty;
    public int IdleMinutes { get; set; }
}

/// <summary>
/// Active session response DTO.
/// </summary>
public class ActiveSessionResponseDto
{
    public List<ActiveSessionItemDto> Items { get; set; } = new();
    public int TotalCount { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int ActiveCount { get; set; }
    public int IdleCount { get; set; }
}

/// <summary>
/// Session statistics DTO.
/// </summary>
public class SessionStatisticsDto
{
    public int TotalActiveSessions { get; set; }
    public int UniqueUsers { get; set; }
    public int IdleSessions { get; set; }
    public int ExpiringSoonSessions { get; set; }
    public List<SessionByDeviceDto> ByDevice { get; set; } = new();
    public List<SessionByLocationDto> ByLocation { get; set; } = new();
}

/// <summary>
/// Session by device DTO.
/// </summary>
public class SessionByDeviceDto
{
    public string Device { get; set; } = string.Empty;
    public int Count { get; set; }
    public double Percentage { get; set; }
}

/// <summary>
/// Session by location DTO.
/// </summary>
public class SessionByLocationDto
{
    public string Location { get; set; } = string.Empty;
    public int Count { get; set; }
}

/// <summary>
/// Terminate session request DTO.
/// </summary>
public class TerminateSessionRequestDto
{
    public string DivSeq { get; set; } = string.Empty;
    public string SessionId { get; set; } = string.Empty;
    public string Reason { get; set; } = string.Empty;
    public string TerminateUserId { get; set; } = string.Empty;
}

/// <summary>
/// Terminate session response DTO.
/// </summary>
public class TerminateSessionResponseDto
{
    public string Result { get; set; } = string.Empty;
    public string ResultMessage { get; set; } = string.Empty;
    public string SessionId { get; set; } = string.Empty;
}

/// <summary>
/// Terminate all sessions request DTO.
/// </summary>
public class TerminateAllSessionsRequestDto
{
    public string DivSeq { get; set; } = string.Empty;
    public string? UserId { get; set; }
    public string ExcludeCurrentSession { get; set; } = "Y";
    public string Reason { get; set; } = string.Empty;
    public string TerminateUserId { get; set; } = string.Empty;
}

/// <summary>
/// Terminate all sessions response DTO.
/// </summary>
public class TerminateAllSessionsResponseDto
{
    public string Result { get; set; } = string.Empty;
    public string ResultMessage { get; set; } = string.Empty;
    public int TerminatedCount { get; set; }
}

#endregion
