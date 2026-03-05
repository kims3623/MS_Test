namespace Sphere.Application.DTOs.System;

#region System Config DTOs

/// <summary>
/// System configuration DTO.
/// </summary>
public class SystemConfigDto
{
    public string DivSeq { get; set; } = string.Empty;

    // General Settings
    public string SystemName { get; set; } = string.Empty;
    public string SystemNameE { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    public string CompanyNameE { get; set; } = string.Empty;
    public string DefaultLanguage { get; set; } = "ko-KR";
    public string DefaultTimezone { get; set; } = "Asia/Seoul";
    public string DateFormat { get; set; } = "yyyy-MM-dd";
    public string TimeFormat { get; set; } = "HH:mm:ss";
    public string NumberFormat { get; set; } = "#,##0.##";

    // Email Settings
    public string SmtpServer { get; set; } = string.Empty;
    public int SmtpPort { get; set; } = 587;
    public string SmtpUseSsl { get; set; } = "Y";
    public string SmtpUsername { get; set; } = string.Empty;
    public string SmtpFromEmail { get; set; } = string.Empty;
    public string SmtpFromName { get; set; } = string.Empty;

    // File Upload Settings
    public int MaxFileSize { get; set; } = 10485760; // 10MB
    public string AllowedFileTypes { get; set; } = "pdf,xlsx,xls,doc,docx,ppt,pptx,jpg,jpeg,png,gif";
    public string FileStoragePath { get; set; } = string.Empty;

    // Notification Settings
    public string EnableEmailNotification { get; set; } = "Y";
    public string EnableSmsNotification { get; set; } = "N";
    public string EnablePushNotification { get; set; } = "N";

    // Data Settings
    public int DataRetentionDays { get; set; } = 365;
    public string EnableDataArchive { get; set; } = "Y";
    public int ArchiveAfterDays { get; set; } = 180;

    // API Settings
    public int ApiRateLimitPerMinute { get; set; } = 100;
    public int ApiTimeoutSeconds { get; set; } = 30;

    // Metadata
    public string UpdateDate { get; set; } = string.Empty;
    public string UpdateUserId { get; set; } = string.Empty;
}

/// <summary>
/// System config item DTO for key-value storage.
/// </summary>
public class SystemConfigItemDto
{
    public string ConfigKey { get; set; } = string.Empty;
    public string ConfigValue { get; set; } = string.Empty;
    public string ConfigType { get; set; } = string.Empty;
    public string ConfigGroup { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string IsEncrypted { get; set; } = string.Empty;
    public string IsEditable { get; set; } = string.Empty;
    public string UpdateDate { get; set; } = string.Empty;
    public string UpdateUserId { get; set; } = string.Empty;
}

/// <summary>
/// System config list response DTO.
/// </summary>
public class SystemConfigListResponseDto
{
    public List<SystemConfigItemDto> Items { get; set; } = new();
    public int TotalCount { get; set; }
}

/// <summary>
/// Update system config request DTO.
/// </summary>
public class UpdateSystemConfigRequestDto
{
    public string DivSeq { get; set; } = string.Empty;
    public List<SystemConfigUpdateItemDto> Items { get; set; } = new();
    public string UpdateUserId { get; set; } = string.Empty;
}

/// <summary>
/// System config update item DTO.
/// </summary>
public class SystemConfigUpdateItemDto
{
    public string ConfigKey { get; set; } = string.Empty;
    public string ConfigValue { get; set; } = string.Empty;
}

/// <summary>
/// Update system config response DTO.
/// </summary>
public class UpdateSystemConfigResponseDto
{
    public string Result { get; set; } = string.Empty;
    public string ResultMessage { get; set; } = string.Empty;
    public int UpdatedCount { get; set; }
}

#endregion

#region System Health DTOs

/// <summary>
/// System health DTO.
/// </summary>
public class SystemHealthDto
{
    public string Status { get; set; } = string.Empty;
    public string CheckDate { get; set; } = string.Empty;
    public SystemHealthDatabaseDto Database { get; set; } = new();
    public SystemHealthCacheDto Cache { get; set; } = new();
    public SystemHealthStorageDto Storage { get; set; } = new();
    public List<SystemHealthServiceDto> Services { get; set; } = new();
}

/// <summary>
/// System health database DTO.
/// </summary>
public class SystemHealthDatabaseDto
{
    public string Status { get; set; } = string.Empty;
    public int ConnectionCount { get; set; }
    public int MaxConnectionCount { get; set; }
    public double AvgResponseTimeMs { get; set; }
    public string LastCheckDate { get; set; } = string.Empty;
}

/// <summary>
/// System health cache DTO.
/// </summary>
public class SystemHealthCacheDto
{
    public string Status { get; set; } = string.Empty;
    public long MemoryUsed { get; set; }
    public long MemoryTotal { get; set; }
    public double HitRate { get; set; }
    public int ItemCount { get; set; }
}

/// <summary>
/// System health storage DTO.
/// </summary>
public class SystemHealthStorageDto
{
    public string Status { get; set; } = string.Empty;
    public long DiskUsed { get; set; }
    public long DiskTotal { get; set; }
    public double UsagePercent { get; set; }
}

/// <summary>
/// System health service DTO.
/// </summary>
public class SystemHealthServiceDto
{
    public string ServiceName { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string LastCheckDate { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}

/// <summary>
/// Clear cache request DTO.
/// </summary>
public class ClearCacheRequestDto
{
    public string DivSeq { get; set; } = string.Empty;
    public string CacheType { get; set; } = "all";
    public string? CacheKey { get; set; }
    public string RequestUserId { get; set; } = string.Empty;
}

/// <summary>
/// Clear cache response DTO.
/// </summary>
public class ClearCacheResponseDto
{
    public string Result { get; set; } = string.Empty;
    public string ResultMessage { get; set; } = string.Empty;
    public int ClearedCount { get; set; }
}

#endregion
