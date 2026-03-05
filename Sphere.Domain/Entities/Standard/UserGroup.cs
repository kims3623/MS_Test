using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.Standard;

/// <summary>
/// User group entity for managing user groups.
/// Maps to SPC_USER_GROUP table.
/// </summary>
public class UserGroup : SphereEntity
{
    /// <summary>
    /// Group identifier (PK)
    /// </summary>
    public string GroupId { get; set; } = string.Empty;

    /// <summary>
    /// Group name
    /// </summary>
    public string GroupName { get; set; } = string.Empty;

    /// <summary>
    /// Group name in Korean
    /// </summary>
    public string GroupNameK { get; set; } = string.Empty;

    /// <summary>
    /// Group name in English
    /// </summary>
    public string GroupNameE { get; set; } = string.Empty;

    /// <summary>
    /// Group type
    /// </summary>
    public string GroupType { get; set; } = string.Empty;

    /// <summary>
    /// Parent group identifier
    /// </summary>
    public string ParentGroupId { get; set; } = string.Empty;

    /// <summary>
    /// Member user IDs (comma-separated)
    /// </summary>
    public string MemberUserIds { get; set; } = string.Empty;

    /// <summary>
    /// Description
    /// </summary>
    public string Description { get; set; } = string.Empty;
}
