using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.Auth;

/// <summary>
/// Authority filter entity for data-level access control.
/// Maps to USP_SPC_AUTHORITY_FILTER_SELECT result.
/// </summary>
public class AuthorityFilter : SphereEntity
{
    /// <summary>
    /// User identifier (PK)
    /// </summary>
    public string UserId { get; set; } = string.Empty;

    /// <summary>
    /// Filter type (e.g., VENDOR, MATERIAL, PROJECT)
    /// </summary>
    public string FilterType { get; set; } = string.Empty;

    /// <summary>
    /// Filter value identifier
    /// </summary>
    public string FilterValue { get; set; } = string.Empty;

    /// <summary>
    /// Filter value name
    /// </summary>
    public string FilterValueName { get; set; } = string.Empty;

    /// <summary>
    /// Access level (ALL, PARTIAL, NONE)
    /// </summary>
    public string AccessLevel { get; set; } = "ALL";

    /// <summary>
    /// Vendor IDs the user can access (comma-separated)
    /// </summary>
    public string VendorIds { get; set; } = string.Empty;

    /// <summary>
    /// Material class IDs the user can access (comma-separated)
    /// </summary>
    public string MtrlClassIds { get; set; } = string.Empty;

    /// <summary>
    /// Project IDs the user can access (comma-separated)
    /// </summary>
    public string ProjectIds { get; set; } = string.Empty;
}
