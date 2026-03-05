using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.Common;

/// <summary>
/// Step entity for managing process steps.
/// Maps to SPC_STEP table.
/// </summary>
public class Step : SphereEntity
{
    /// <summary>
    /// Step identifier (PK)
    /// </summary>
    public string StepId { get; set; } = string.Empty;

    /// <summary>
    /// Step name (default/display)
    /// </summary>
    public string StepName { get; set; } = string.Empty;

    /// <summary>
    /// Step name in Korean
    /// </summary>
    public string StepNameK { get; set; } = string.Empty;

    /// <summary>
    /// Step name in English
    /// </summary>
    public string StepNameE { get; set; } = string.Empty;

    /// <summary>
    /// Step name in Chinese
    /// </summary>
    public string StepNameC { get; set; } = string.Empty;

    /// <summary>
    /// Step name in Vietnamese
    /// </summary>
    public string StepNameV { get; set; } = string.Empty;

    /// <summary>
    /// Display sequence for ordering
    /// </summary>
    public int DspSeq { get; set; }

    /// <summary>
    /// Description of the step
    /// </summary>
    public string Description { get; set; } = string.Empty;
}
