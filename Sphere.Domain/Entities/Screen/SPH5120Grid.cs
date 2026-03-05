using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.Screen;

/// <summary>
/// SPH5120 Grid entity for user management grid.
/// </summary>
public class SPH5120Grid : SphereEntity
{
    public string UserId { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string UserNameK { get; set; } = string.Empty;
    public string UserNameE { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string DeptId { get; set; } = string.Empty;
    public string DeptName { get; set; } = string.Empty;
    public string RoleId { get; set; } = string.Empty;
    public string RoleName { get; set; } = string.Empty;
    public string AuthorityId { get; set; } = string.Empty;
    public string AuthorityName { get; set; } = string.Empty;
    public string ActiveYn { get; set; } = "Y";
    public DateTime? LastLoginDate { get; set; }
    public string LastLoginDateStr { get; set; } = string.Empty;
}
