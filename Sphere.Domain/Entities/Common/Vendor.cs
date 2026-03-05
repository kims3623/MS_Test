using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.Common;

/// <summary>
/// Vendor entity for managing suppliers and business partners.
/// Maps to SPC_VENDOR table.
/// </summary>
public class Vendor : SphereEntity
{
    /// <summary>
    /// Vendor identifier (PK)
    /// </summary>
    public string VendorId { get; set; } = string.Empty;

    /// <summary>
    /// Vendor name (default/display)
    /// </summary>
    public string VendorName { get; set; } = string.Empty;

    /// <summary>
    /// Vendor name in Korean
    /// </summary>
    public string VendorNameK { get; set; } = string.Empty;

    /// <summary>
    /// Vendor name in English
    /// </summary>
    public string VendorNameE { get; set; } = string.Empty;

    /// <summary>
    /// Vendor name in Chinese
    /// </summary>
    public string VendorNameC { get; set; } = string.Empty;

    /// <summary>
    /// Vendor name in Vietnamese
    /// </summary>
    public string VendorNameV { get; set; } = string.Empty;

    /// <summary>
    /// Vendor type code (references CodeMaster)
    /// </summary>
    public string VendorType { get; set; } = string.Empty;

    /// <summary>
    /// Vendor type display name
    /// </summary>
    public string VendorTypeName { get; set; } = string.Empty;
}
