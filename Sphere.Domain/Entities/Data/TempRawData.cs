using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.Data;

/// <summary>
/// Temporary raw data entity for staging data before confirmation.
/// Maps to SPC_TEMP_RAWDATA table.
/// </summary>
public class TempRawData : SphereEntity
{
    /// <summary>
    /// Temporary data identifier (PK)
    /// </summary>
    public string TempId { get; set; } = string.Empty;

    /// <summary>
    /// Specification system identifier
    /// </summary>
    public string SpecSysId { get; set; } = string.Empty;

    /// <summary>
    /// Work date
    /// </summary>
    public string WorkDate { get; set; } = string.Empty;

    /// <summary>
    /// Shift identifier
    /// </summary>
    public string Shift { get; set; } = string.Empty;

    /// <summary>
    /// Material identifier (FK)
    /// </summary>
    public string MtrlId { get; set; } = string.Empty;

    /// <summary>
    /// Material name
    /// </summary>
    public string MtrlName { get; set; } = string.Empty;

    /// <summary>
    /// Vendor identifier (FK)
    /// </summary>
    public string VendorId { get; set; } = string.Empty;

    /// <summary>
    /// Vendor name
    /// </summary>
    public string VendorName { get; set; } = string.Empty;

    /// <summary>
    /// Raw data values
    /// </summary>
    public string RawDataValue { get; set; } = string.Empty;

    /// <summary>
    /// Input quantity
    /// </summary>
    public string InputQty { get; set; } = string.Empty;

    /// <summary>
    /// Defect quantity
    /// </summary>
    public string DefectQty { get; set; } = string.Empty;

    /// <summary>
    /// Upload status
    /// </summary>
    public string UploadStatus { get; set; } = string.Empty;

    /// <summary>
    /// Error message if upload failed
    /// </summary>
    public string ErrorMessage { get; set; } = string.Empty;

    /// <summary>
    /// Batch identifier for grouping uploads
    /// </summary>
    public string BatchId { get; set; } = string.Empty;

    /// <summary>
    /// Upload timestamp
    /// </summary>
    public DateTime? UploadDate { get; set; }
}
