using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.TPS;

/// <summary>
/// Alarm attachment entity for storing alarm-related files.
/// Maps to SPC_ALARM_ATTACHMENT table.
/// </summary>
public class AlarmAttachment : SphereEntity
{
    /// <summary>
    /// Attachment identifier (PK)
    /// </summary>
    public string AttachmentId { get; set; } = string.Empty;

    /// <summary>
    /// Alarm system identifier (FK)
    /// </summary>
    public string AlmSysId { get; set; } = string.Empty;

    /// <summary>
    /// File name
    /// </summary>
    public string FileName { get; set; } = string.Empty;

    /// <summary>
    /// Original file name
    /// </summary>
    public string OriginalFileName { get; set; } = string.Empty;

    /// <summary>
    /// File path/URL
    /// </summary>
    public string FilePath { get; set; } = string.Empty;

    /// <summary>
    /// File size in bytes
    /// </summary>
    public long FileSize { get; set; }

    /// <summary>
    /// File type/extension
    /// </summary>
    public string FileType { get; set; } = string.Empty;

    /// <summary>
    /// MIME type
    /// </summary>
    public string MimeType { get; set; } = string.Empty;

    /// <summary>
    /// Upload timestamp
    /// </summary>
    public DateTime? UploadDate { get; set; }

    /// <summary>
    /// Uploader user ID
    /// </summary>
    public string UploadUserId { get; set; } = string.Empty;

    /// <summary>
    /// Description
    /// </summary>
    public string Description { get; set; } = string.Empty;
}
