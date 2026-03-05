using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.Common;

/// <summary>
/// Pagination entity for grid pagination settings.
/// </summary>
public class Pagination : SphereEntity
{
    public string ConfigId { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string ScreenId { get; set; } = string.Empty;
    public int PageSize { get; set; } = 20;
    public string PageSizeOptions { get; set; } = "10,20,50,100";
    public string ShowTotal { get; set; } = "Y";
}
