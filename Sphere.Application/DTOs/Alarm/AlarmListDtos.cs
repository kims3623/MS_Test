namespace Sphere.Application.DTOs.Alarm;

/// <summary>
/// Alarm list filter DTO for search operations.
/// </summary>
public class AlarmListFilterDto
{
    public string DivSeq { get; set; } = string.Empty;
    public string? StartDate { get; set; }
    public string? EndDate { get; set; }
    public string? VendorId { get; set; }
    public string? MtrlClassId { get; set; }
    public string? AlmActionId { get; set; }
    public string? AlmProcYn { get; set; }
    public string? StopYn { get; set; }
    public string? UserId { get; set; }
    public int? PageNumber { get; set; }
    public int? PageSize { get; set; }
}

/// <summary>
/// Alarm list item DTO for grid display.
/// </summary>
public class AlarmListItemDto
{
    public string AlmSysId { get; set; } = string.Empty;
    public string AlmNo { get; set; } = string.Empty;
    public string AlmProcId { get; set; } = string.Empty;
    public string AlmProcName { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
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
    public string CreateDate { get; set; } = string.Empty;
    public string CreateUserId { get; set; } = string.Empty;
    public string UpdateDate { get; set; } = string.Empty;
}

/// <summary>
/// Alarm list response with pagination.
/// </summary>
public class AlarmListResponseDto
{
    public List<AlarmListItemDto> Items { get; set; } = new();
    public int TotalCount { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages => PageSize > 0 ? (int)Math.Ceiling((double)TotalCount / PageSize) : 0;
}

/// <summary>
/// Create alarm request DTO.
/// </summary>
public class CreateAlarmRequestDto
{
    public string DivSeq { get; set; } = string.Empty;
    public string AlmProcId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Contents { get; set; } = string.Empty;
    public string VendorId { get; set; } = string.Empty;
    public string MtrlClassId { get; set; } = string.Empty;
    public string? SpecSysId { get; set; }
    public string Severity { get; set; } = "3";
    public string CreateUserId { get; set; } = string.Empty;
}

/// <summary>
/// Create alarm response DTO.
/// </summary>
public class CreateAlarmResponseDto
{
    public string Result { get; set; } = string.Empty;
    public string ResultMessage { get; set; } = string.Empty;
    public string AlmSysId { get; set; } = string.Empty;
    public string AlmNo { get; set; } = string.Empty;
}

/// <summary>
/// Update alarm request DTO.
/// </summary>
public class UpdateAlarmRequestDto
{
    public string AlmSysId { get; set; } = string.Empty;
    public string DivSeq { get; set; } = string.Empty;
    public string? Title { get; set; }
    public string? Contents { get; set; }
    public string? AlmActionId { get; set; }
    public string? Severity { get; set; }
    public string UpdateUserId { get; set; } = string.Empty;
}

/// <summary>
/// Update alarm response DTO.
/// </summary>
public class UpdateAlarmResponseDto
{
    public string Result { get; set; } = string.Empty;
    public string ResultMessage { get; set; } = string.Empty;
    public string AlmSysId { get; set; } = string.Empty;
}
