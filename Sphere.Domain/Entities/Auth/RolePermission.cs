using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.Auth;

/// <summary>
/// Role permission entity for role-based access control.
/// </summary>
public class RolePermission : SphereEntity
{
    public string RoleCode { get; set; } = string.Empty;
    public string PermissionCode { get; set; } = string.Empty;
    public string PermissionName { get; set; } = string.Empty;
    public string ResourceType { get; set; } = string.Empty;
    public string ResourceId { get; set; } = string.Empty;
    public string ActionType { get; set; } = "READ";
    public string GrantedYn { get; set; } = "Y";
}
