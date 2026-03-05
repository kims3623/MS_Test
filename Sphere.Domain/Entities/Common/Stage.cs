using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.Common;

/// <summary>
/// Stage entity for managing production stages.
/// Maps to SPC_STAGE table.
/// </summary>
public class Stage : SphereEntity
{
    /// <summary>
    /// Stage identifier (PK)
    /// </summary>
    public string StageId { get; set; } = string.Empty;

    /// <summary>
    /// Stage name (default/display)
    /// </summary>
    public string StageName { get; set; } = string.Empty;

    /// <summary>
    /// Stage name in Korean
    /// </summary>
    public string StageNameK { get; set; } = string.Empty;

    /// <summary>
    /// Stage name in English
    /// </summary>
    public string StageNameE { get; set; } = string.Empty;

    /// <summary>
    /// Stage name in Chinese
    /// </summary>
    public string StageNameC { get; set; } = string.Empty;

    /// <summary>
    /// Stage name in Vietnamese
    /// </summary>
    public string StageNameV { get; set; } = string.Empty;

    /// <summary>
    /// Display sequence for ordering
    /// </summary>
    public int DspSeq { get; set; }
}
