using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.System;

/// <summary>
/// System configuration entity for managing application settings.
/// Maps to SPC_SYSTEM_CONFIG table.
/// </summary>
public class SystemConfig : SphereEntity
{
    /// <summary>
    /// Configuration key (PK)
    /// </summary>
    public string ConfigKey { get; set; } = string.Empty;

    /// <summary>
    /// Configuration category
    /// </summary>
    public string Category { get; set; } = string.Empty;

    /// <summary>
    /// Configuration value
    /// </summary>
    public string ConfigValue { get; set; } = string.Empty;

    /// <summary>
    /// Value type (STRING, INT, BOOL, JSON)
    /// </summary>
    public string ValueType { get; set; } = "STRING";

    /// <summary>
    /// Default value
    /// </summary>
    public string DefaultValue { get; set; } = string.Empty;

    /// <summary>
    /// Configuration description
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Editable flag
    /// </summary>
    public string EditableYn { get; set; } = "Y";

    /// <summary>
    /// Display sequence
    /// </summary>
    public int DspSeq { get; set; }
}
