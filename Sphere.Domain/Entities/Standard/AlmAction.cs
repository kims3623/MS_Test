using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.Standard;

/// <summary>
/// Alarm action entity for alarm action definitions.
/// </summary>
public class AlmAction : SphereEntity
{
    public string AlmActionId { get; set; } = string.Empty;
    public string AlmActionName { get; set; } = string.Empty;
    public string AlmActionNameK { get; set; } = string.Empty;
    public string AlmActionNameE { get; set; } = string.Empty;
    public string ActionType { get; set; } = string.Empty;
    public string ActionScript { get; set; } = string.Empty;
    public string Parameters { get; set; } = string.Empty;
    public int DspSeq { get; set; }
    public string Description { get; set; } = string.Empty;
}
