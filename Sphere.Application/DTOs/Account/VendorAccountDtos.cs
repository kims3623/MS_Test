namespace Sphere.Application.DTOs.Account;

#region Vendor Account Request DTOs

/// <summary>
/// Request DTO for vendor account registration.
/// </summary>
public class VendorAccountRequestDto
{
    /// <summary>
    /// Vendor ID (if existing vendor).
    /// </summary>
    public string? VendorId { get; set; }

    /// <summary>
    /// Vendor/Company name.
    /// </summary>
    public string VendorName { get; set; } = string.Empty;

    /// <summary>
    /// Contact person name.
    /// </summary>
    public string ContactPerson { get; set; } = string.Empty;

    /// <summary>
    /// Contact email address.
    /// </summary>
    public string ContactEmail { get; set; } = string.Empty;

    /// <summary>
    /// Contact phone number.
    /// </summary>
    public string? ContactPhone { get; set; }

    /// <summary>
    /// Reason for account request.
    /// </summary>
    public string RequestReason { get; set; } = string.Empty;

    /// <summary>
    /// Additional information or notes.
    /// </summary>
    public string? AdditionalInfo { get; set; }
}

/// <summary>
/// Response DTO for vendor account request.
/// </summary>
public class VendorAccountRequestResultDto
{
    /// <summary>
    /// Whether the request was successful.
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// Result message.
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Generated request ID.
    /// </summary>
    public string? RequestId { get; set; }
}

/// <summary>
/// Vendor account request entity for tracking.
/// </summary>
public class VendorAccountRequestDetailDto
{
    public string RequestId { get; set; } = string.Empty;
    public string VendorId { get; set; } = string.Empty;
    public string VendorName { get; set; } = string.Empty;
    public string ContactPerson { get; set; } = string.Empty;
    public string ContactEmail { get; set; } = string.Empty;
    public string ContactPhone { get; set; } = string.Empty;
    public string RequestReason { get; set; } = string.Empty;
    public string AdditionalInfo { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string StatusName { get; set; } = string.Empty;
    public DateTime RequestDate { get; set; }
    public string? ProcessedBy { get; set; }
    public DateTime? ProcessedDate { get; set; }
    public string? ProcessNote { get; set; }
}

#endregion
