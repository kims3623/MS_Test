using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.Common;

/// <summary>
/// Sort configuration entity for grid sorting.
/// </summary>
public class SortConfig : SphereEntity
{
    public string ConfigId { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string ScreenId { get; set; } = string.Empty;
    public string ColumnId { get; set; } = string.Empty;
    public string SortDirection { get; set; } = "ASC";
    public int SortPriority { get; set; }
}
