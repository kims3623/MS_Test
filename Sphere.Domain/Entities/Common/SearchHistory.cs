using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.Common;

/// <summary>
/// Search history entity for user search history.
/// </summary>
public class SearchHistory : SphereEntity
{
    public string HistoryId { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string ScreenId { get; set; } = string.Empty;
    public string SearchText { get; set; } = string.Empty;
    public string SearchJson { get; set; } = string.Empty;
    public DateTime? SearchDate { get; set; }
    public int UseCount { get; set; }
}
