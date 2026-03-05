using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.Common;

/// <summary>
/// Frequency entity for managing inspection frequencies.
/// Maps to SPC_FREQUENCY table.
/// </summary>
public class Frequency : SphereEntity
{
    /// <summary>
    /// Frequency identifier (PK)
    /// </summary>
    public string FrequencyId { get; set; } = string.Empty;

    /// <summary>
    /// Frequency name (default/display)
    /// </summary>
    public string FrequencyName { get; set; } = string.Empty;

    /// <summary>
    /// Frequency name in Korean
    /// </summary>
    public string FrequencyNameK { get; set; } = string.Empty;

    /// <summary>
    /// Frequency name in English
    /// </summary>
    public string FrequencyNameE { get; set; } = string.Empty;

    /// <summary>
    /// Frequency name in Chinese
    /// </summary>
    public string FrequencyNameC { get; set; } = string.Empty;

    /// <summary>
    /// Frequency name in Vietnamese
    /// </summary>
    public string FrequencyNameV { get; set; } = string.Empty;

    /// <summary>
    /// Display sequence for ordering
    /// </summary>
    public int DspSeq { get; set; }

    /// <summary>
    /// Frequency value (e.g., times per day)
    /// </summary>
    public int FrequencyValue { get; set; }

    /// <summary>
    /// Description of the frequency
    /// </summary>
    public string Description { get; set; } = string.Empty;
}
