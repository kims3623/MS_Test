using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.Approval;

/// <summary>
/// Approval attachment entity for storing approval-related files.
/// Maps to SPC_APROV_ATTACHMENT table.
/// </summary>
public class ApprovalAttachment : SphereEntity
{
    /// <summary>
    /// Attachment identifier (PK)
    /// </summary>
    public string AttachmentId { get; set; } = string.Empty;

    /// <summary>
    /// Approval identifier (FK)
    /// </summary>
    public string AprovId { get; set; } = string.Empty;

    /// <summary>
    /// File name (stored name)
    /// </summary>
    public string FileName { get; set; } = string.Empty;

    /// <summary>
    /// Original file name (as uploaded)
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
    public string? Description { get; set; }
}
