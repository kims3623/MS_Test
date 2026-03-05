namespace Sphere.Application.DTOs.Alarm;

/// <summary>
/// Alarm attachment item DTO.
/// </summary>
public class AlarmAttachmentDto
{
    public string AttachSeq { get; set; } = string.Empty;
    public string AlmSysId { get; set; } = string.Empty;
    public string FileName { get; set; } = string.Empty;
    public string OriginalFileName { get; set; } = string.Empty;
    public string FilePath { get; set; } = string.Empty;
    public long FileSize { get; set; }
    public string FileType { get; set; } = string.Empty;
    public string MimeType { get; set; } = string.Empty;
    public string CreateDate { get; set; } = string.Empty;
    public string CreateUserId { get; set; } = string.Empty;
    public string CreateUserName { get; set; } = string.Empty;
}

/// <summary>
/// Alarm attachment query parameters.
/// </summary>
public class AlarmAttachmentQueryDto
{
    public string DivSeq { get; set; } = string.Empty;
    public string AlmSysId { get; set; } = string.Empty;
}

/// <summary>
/// Alarm attachment list response.
/// </summary>
public class AlarmAttachmentListResponseDto
{
    public List<AlarmAttachmentDto> Items { get; set; } = new();
    public int TotalCount { get; set; }
    public long TotalSize { get; set; }
}

/// <summary>
/// Upload alarm attachment request DTO.
/// </summary>
public class UploadAlarmAttachmentRequestDto
{
    public string DivSeq { get; set; } = string.Empty;
    public string AlmSysId { get; set; } = string.Empty;
    public string FileName { get; set; } = string.Empty;
    public string OriginalFileName { get; set; } = string.Empty;
    public long FileSize { get; set; }
    public string FileType { get; set; } = string.Empty;
    public string MimeType { get; set; } = string.Empty;
    public byte[] FileContent { get; set; } = Array.Empty<byte>();
    public string CreateUserId { get; set; } = string.Empty;
}

/// <summary>
/// Upload alarm attachment response DTO.
/// </summary>
public class UploadAlarmAttachmentResponseDto
{
    public string Result { get; set; } = string.Empty;
    public string ResultMessage { get; set; } = string.Empty;
    public string AttachSeq { get; set; } = string.Empty;
    public string FilePath { get; set; } = string.Empty;
}

/// <summary>
/// Delete alarm attachment request DTO.
/// </summary>
public class DeleteAlarmAttachmentRequestDto
{
    public string DivSeq { get; set; } = string.Empty;
    public string AlmSysId { get; set; } = string.Empty;
    public string AttachSeq { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
}

/// <summary>
/// Delete alarm attachment response DTO.
/// </summary>
public class DeleteAlarmAttachmentResponseDto
{
    public string Result { get; set; } = string.Empty;
    public string ResultMessage { get; set; } = string.Empty;
}

/// <summary>
/// Download alarm attachment request DTO.
/// </summary>
public class DownloadAlarmAttachmentRequestDto
{
    public string DivSeq { get; set; } = string.Empty;
    public string AlmSysId { get; set; } = string.Empty;
    public string AttachSeq { get; set; } = string.Empty;
}

/// <summary>
/// Download alarm attachment response DTO.
/// </summary>
public class DownloadAlarmAttachmentResponseDto
{
    public string FileName { get; set; } = string.Empty;
    public string MimeType { get; set; } = string.Empty;
    public byte[] FileContent { get; set; } = Array.Empty<byte>();
}
