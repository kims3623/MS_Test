using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.Auth;

/// <summary>
/// Authority entity for managing user permissions.
/// Maps to SPC_AUTHORITY table.
/// </summary>
public class Authority : SphereEntity
{
    /// <summary>
    /// User identifier (PK)
    /// </summary>
    public string UserId { get; set; } = string.Empty;

    /// <summary>
    /// Authority type code
    /// </summary>
    public string AuthType { get; set; } = string.Empty;

    /// <summary>
    /// Authority type name
    /// </summary>
    public string AuthTypeName { get; set; } = string.Empty;

    /// <summary>
    /// Menu identifier
    /// </summary>
    public string MenuId { get; set; } = string.Empty;

    /// <summary>
    /// Menu name
    /// </summary>
    public string MenuName { get; set; } = string.Empty;

    /// <summary>
    /// Read permission flag
    /// </summary>
    public string CanRead { get; set; } = "Y";

    /// <summary>
    /// Write permission flag
    /// </summary>
    public string CanWrite { get; set; } = "N";

    /// <summary>
    /// Delete permission flag
    /// </summary>
    public string CanDelete { get; set; } = "N";

    /// <summary>
    /// Export permission flag
    /// </summary>
    public string CanExport { get; set; } = "N";

    /// <summary>
    /// Admin permission flag
    /// </summary>
    public string CanAdmin { get; set; } = "N";
}
