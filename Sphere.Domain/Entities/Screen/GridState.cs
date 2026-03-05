using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.Screen;

/// <summary>
/// Grid state entity for storing grid configuration state.
/// </summary>
public class GridState : SphereEntity
{
    public string StateId { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string ScreenId { get; set; } = string.Empty;
    public string GridId { get; set; } = string.Empty;
    public string ColumnJson { get; set; } = string.Empty;
    public string SortJson { get; set; } = string.Empty;
    public string FilterJson { get; set; } = string.Empty;
    public int PageSize { get; set; } = 20;
}
