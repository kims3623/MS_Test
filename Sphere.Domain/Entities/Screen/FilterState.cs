using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.Screen;

/// <summary>
/// Filter state entity for storing filter state.
/// </summary>
public class FilterState : SphereEntity
{
    public string FilterId { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string ScreenId { get; set; } = string.Empty;
    public string FilterName { get; set; } = string.Empty;
    public string FilterJson { get; set; } = string.Empty;
    public string DefaultYn { get; set; } = "N";
    public int DspSeq { get; set; }
    public string SharedYn { get; set; } = "N";
    public DateTime? LastUsedDate { get; set; }
    public string Description { get; set; } = string.Empty;
}
