using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.Standard;

/// <summary>
/// Alarm process entity for alarm processing configuration.
/// </summary>
public class AlmProc : SphereEntity
{
    public string AlmProcId { get; set; } = string.Empty;
    public string AlmProcName { get; set; } = string.Empty;
    public string AlmProcNameK { get; set; } = string.Empty;
    public string AlmProcNameE { get; set; } = string.Empty;
    public string ProcType { get; set; } = string.Empty;
    public int Priority { get; set; }
    public string NotifyMethod { get; set; } = string.Empty;
    public string EscalationRule { get; set; } = string.Empty;
    public int ResponseTime { get; set; }
    public string Description { get; set; } = string.Empty;
}
