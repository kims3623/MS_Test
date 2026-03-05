using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.Auth;

/// <summary>
/// Vendor account request entity.
/// Tracks vendor registration requests submitted through the RequestAccount4Vendor popup.
/// </summary>
public class VendorAccountRequest : SphereEntity
{
    /// <summary>
    /// Unique request ID (e.g., VAR20260203123456ABC123)
    /// </summary>
    public string RequestId { get; set; } = string.Empty;

    /// <summary>
    /// Vendor company name
    /// </summary>
    public string VendorName { get; set; } = string.Empty;

    /// <summary>
    /// Business registration number
    /// </summary>
    public string? BusinessNumber { get; set; }

    /// <summary>
    /// Contact person name
    /// </summary>
    public string ContactName { get; set; } = string.Empty;

    /// <summary>
    /// Contact email address
    /// </summary>
    public string ContactEmail { get; set; } = string.Empty;

    /// <summary>
    /// Contact phone number
    /// </summary>
    public string? ContactPhone { get; set; }

    /// <summary>
    /// Vendor address
    /// </summary>
    public string? Address { get; set; }

    /// <summary>
    /// Request description or reason
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Request status: PENDING, APPROVED, REJECTED
    /// </summary>
    public string Status { get; set; } = "PENDING";

    /// <summary>
    /// Date when the request was submitted
    /// </summary>
    public DateTime? RequestedAt { get; set; }

    /// <summary>
    /// Date when the request was processed (approved/rejected)
    /// </summary>
    public DateTime? ProcessedAt { get; set; }

    /// <summary>
    /// User ID who processed the request
    /// </summary>
    public string? ProcessedBy { get; set; }

    /// <summary>
    /// Rejection reason if status is REJECTED
    /// </summary>
    public string? RejectionReason { get; set; }

    /// <summary>
    /// Associated vendor ID once account is created
    /// </summary>
    public string? VendorId { get; set; }

    /// <summary>
    /// Associated user ID once account is created
    /// </summary>
    public string? UserId { get; set; }
}
