using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.Common;

/// <summary>
/// File information entity for managing uploaded file metadata.
/// Maps to SPC_FILE_INFO table.
/// </summary>
/// <remarks>
/// Composite PK: DivSeq + FileId
/// Named SphereFileInfo to avoid conflict with System.IO.FileInfo.
/// Stores file metadata; actual file content stored in file system or blob storage.
/// </remarks>
public class SphereFileInfo : SphereEntity
{
    /// <summary>
    /// File identifier (PK)
    /// </summary>
    public string FileId { get; set; } = string.Empty;

    /// <summary>
    /// Original file name as uploaded
    /// </summary>
    public string OriginalFileName { get; set; } = string.Empty;

    /// <summary>
    /// Stored file name (sanitized/renamed)
    /// </summary>
    public string StoredFileName { get; set; } = string.Empty;

    /// <summary>
    /// Storage path or URL
    /// </summary>
    public string FilePath { get; set; } = string.Empty;

    /// <summary>
    /// File size in bytes
    /// </summary>
    public long FileSize { get; set; }

    /// <summary>
    /// File extension (e.g., .pdf, .xlsx)
    /// </summary>
    public string FileExtension { get; set; } = string.Empty;

    /// <summary>
    /// MIME type (e.g., application/pdf)
    /// </summary>
    public string MimeType { get; set; } = string.Empty;

    /// <summary>
    /// Related entity type for file association
    /// </summary>
    public string RefType { get; set; } = string.Empty;

    /// <summary>
    /// Related entity identifier for file association
    /// </summary>
    public string RefId { get; set; } = string.Empty;

    /// <summary>
    /// Upload timestamp
    /// </summary>
    public DateTime? UploadDate { get; set; }

    /// <summary>
    /// Uploader user identifier
    /// </summary>
    public string UploadUserId { get; set; } = string.Empty;
}
