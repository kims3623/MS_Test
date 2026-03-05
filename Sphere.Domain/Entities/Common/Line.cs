using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.Common;

/// <summary>
/// Production line entity for managing production lines.
/// Maps to SPC_LINE table.
/// </summary>
public class Line : SphereEntity
{
    /// <summary>
    /// Line identifier (PK)
    /// </summary>
    public string LineId { get; set; } = string.Empty;

    /// <summary>
    /// Line name (default/display)
    /// </summary>
    public string LineName { get; set; } = string.Empty;

    /// <summary>
    /// Line name in Korean
    /// </summary>
    public string LineNameK { get; set; } = string.Empty;

    /// <summary>
    /// Line name in English
    /// </summary>
    public string LineNameE { get; set; } = string.Empty;

    /// <summary>
    /// Line name in Chinese
    /// </summary>
    public string LineNameC { get; set; } = string.Empty;

    /// <summary>
    /// Line name in Vietnamese
    /// </summary>
    public string LineNameV { get; set; } = string.Empty;

    /// <summary>
    /// Display sequence for ordering
    /// </summary>
    public int DspSeq { get; set; }
}
