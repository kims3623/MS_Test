using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.Common;

/// <summary>
/// Measurement method entity for managing measurement methods.
/// Maps to SPC_MEASM table.
/// </summary>
public class Measm : SphereEntity
{
    /// <summary>
    /// Measurement method identifier (PK)
    /// </summary>
    public string MeasmId { get; set; } = string.Empty;

    /// <summary>
    /// Measurement method name (default/display)
    /// </summary>
    public string MeasmName { get; set; } = string.Empty;

    /// <summary>
    /// Measurement method name in Korean
    /// </summary>
    public string MeasmNameK { get; set; } = string.Empty;

    /// <summary>
    /// Measurement method name in English
    /// </summary>
    public string MeasmNameE { get; set; } = string.Empty;

    /// <summary>
    /// Measurement method name in Chinese
    /// </summary>
    public string MeasmNameC { get; set; } = string.Empty;

    /// <summary>
    /// Measurement method name in Vietnamese
    /// </summary>
    public string MeasmNameV { get; set; } = string.Empty;

    /// <summary>
    /// Display sequence for ordering
    /// </summary>
    public int DspSeq { get; set; }

    /// <summary>
    /// Description of the measurement method
    /// </summary>
    public string Description { get; set; } = string.Empty;
}
