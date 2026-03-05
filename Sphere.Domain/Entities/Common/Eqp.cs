using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.Common;

/// <summary>
/// Equipment entity for managing equipment/machines.
/// Maps to SPC_EQP table.
/// </summary>
public class Eqp : SphereEntity
{
    /// <summary>
    /// Equipment identifier (PK)
    /// </summary>
    public string EqpId { get; set; } = string.Empty;

    /// <summary>
    /// Equipment name (default/display)
    /// </summary>
    public string EqpName { get; set; } = string.Empty;

    /// <summary>
    /// Equipment name in Korean
    /// </summary>
    public string EqpNameK { get; set; } = string.Empty;

    /// <summary>
    /// Equipment name in English
    /// </summary>
    public string EqpNameE { get; set; } = string.Empty;

    /// <summary>
    /// Equipment name in Chinese
    /// </summary>
    public string EqpNameC { get; set; } = string.Empty;

    /// <summary>
    /// Equipment name in Vietnamese
    /// </summary>
    public string EqpNameV { get; set; } = string.Empty;

    /// <summary>
    /// Equipment type code
    /// </summary>
    public string EqpType { get; set; } = string.Empty;

    /// <summary>
    /// Display sequence for ordering
    /// </summary>
    public int DspSeq { get; set; }

    /// <summary>
    /// Description of the equipment
    /// </summary>
    public string Description { get; set; } = string.Empty;
}
