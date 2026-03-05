using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.System;

/// <summary>
/// System code master entity for system-level codes.
/// Maps to SPC_SYSTEM_CODE_MST table.
/// </summary>
public class SystemCodeMaster : SphereEntity
{
    /// <summary>
    /// System code identifier (PK)
    /// </summary>
    public string SysCodeId { get; set; } = string.Empty;

    /// <summary>
    /// System code class identifier (PK)
    /// </summary>
    public string SysCodeClassId { get; set; } = string.Empty;

    /// <summary>
    /// Code name
    /// </summary>
    public string CodeName { get; set; } = string.Empty;

    /// <summary>
    /// Code name in Korean
    /// </summary>
    public string CodeNameK { get; set; } = string.Empty;

    /// <summary>
    /// Code name in English
    /// </summary>
    public string CodeNameE { get; set; } = string.Empty;

    /// <summary>
    /// Code value
    /// </summary>
    public string CodeValue { get; set; } = string.Empty;

    /// <summary>
    /// Display sequence
    /// </summary>
    public int DspSeq { get; set; }

    /// <summary>
    /// Description
    /// </summary>
    public string Description { get; set; } = string.Empty;
}
