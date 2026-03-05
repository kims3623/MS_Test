using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.Common;

/// <summary>
/// End user entity for managing end users/consumers.
/// Maps to SPC_END_USER table.
/// </summary>
public class EndUser : SphereEntity
{
    /// <summary>
    /// End user identifier (PK)
    /// </summary>
    public string EndUserId { get; set; } = string.Empty;

    /// <summary>
    /// End user name (default/display)
    /// </summary>
    public string EndUserName { get; set; } = string.Empty;

    /// <summary>
    /// End user name in Korean
    /// </summary>
    public string EndUserNameK { get; set; } = string.Empty;

    /// <summary>
    /// End user name in English
    /// </summary>
    public string EndUserNameE { get; set; } = string.Empty;

    /// <summary>
    /// End user name in Chinese
    /// </summary>
    public string EndUserNameC { get; set; } = string.Empty;

    /// <summary>
    /// End user name in Vietnamese
    /// </summary>
    public string EndUserNameV { get; set; } = string.Empty;

    /// <summary>
    /// End user type code
    /// </summary>
    public string EndUserType { get; set; } = string.Empty;

    /// <summary>
    /// Display sequence for ordering
    /// </summary>
    public int DspSeq { get; set; }
}
