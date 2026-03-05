using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.Standard;

/// <summary>
/// Yield step entity for managing yield calculation steps.
/// Maps to SPC_YIELD_STEP table.
/// </summary>
public class YieldStep : SphereEntity
{
    /// <summary>
    /// Yield step identifier (PK)
    /// </summary>
    public string YieldStepId { get; set; } = string.Empty;

    /// <summary>
    /// Yield step name
    /// </summary>
    public string YieldStepName { get; set; } = string.Empty;

    /// <summary>
    /// Yield step name in Korean
    /// </summary>
    public string YieldStepNameK { get; set; } = string.Empty;

    /// <summary>
    /// Yield step name in English
    /// </summary>
    public string YieldStepNameE { get; set; } = string.Empty;

    /// <summary>
    /// Yield step name in Chinese
    /// </summary>
    public string YieldStepNameC { get; set; } = string.Empty;

    /// <summary>
    /// Yield step name in Vietnamese
    /// </summary>
    public string YieldStepNameV { get; set; } = string.Empty;

    /// <summary>
    /// Step sequence for ordering
    /// </summary>
    public int StepSeq { get; set; }

    /// <summary>
    /// Calculation formula
    /// </summary>
    public string Formula { get; set; } = string.Empty;

    /// <summary>
    /// Description
    /// </summary>
    public string Description { get; set; } = string.Empty;
}
