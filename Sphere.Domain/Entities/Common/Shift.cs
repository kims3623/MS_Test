using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.Common;

/// <summary>
/// Shift entity for managing work shifts.
/// Maps to SPC_SHIFT table.
/// </summary>
public class Shift : SphereEntity
{
    /// <summary>
    /// Shift identifier (PK)
    /// </summary>
    public string ShiftId { get; set; } = string.Empty;

    /// <summary>
    /// Shift name (default/display)
    /// </summary>
    public string ShiftName { get; set; } = string.Empty;

    /// <summary>
    /// Shift name in Korean
    /// </summary>
    public string ShiftNameK { get; set; } = string.Empty;

    /// <summary>
    /// Shift name in English
    /// </summary>
    public string ShiftNameE { get; set; } = string.Empty;

    /// <summary>
    /// Shift name in Chinese
    /// </summary>
    public string ShiftNameC { get; set; } = string.Empty;

    /// <summary>
    /// Shift name in Vietnamese
    /// </summary>
    public string ShiftNameV { get; set; } = string.Empty;

    /// <summary>
    /// Display sequence for ordering
    /// </summary>
    public int DspSeq { get; set; }

    /// <summary>
    /// Shift start time
    /// </summary>
    public string StartTime { get; set; } = string.Empty;

    /// <summary>
    /// Shift end time
    /// </summary>
    public string EndTime { get; set; } = string.Empty;
}
