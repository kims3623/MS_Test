using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.Common;

/// <summary>
/// Code master entity for managing common codes across the system.
/// Maps to SPC_CODE_MST table.
/// </summary>
public class CodeMaster : SphereEntity
{
    /// <summary>
    /// Code identifier (PK)
    /// </summary>
    public string CodeId { get; set; } = string.Empty;

    /// <summary>
    /// Code class identifier (PK) - groups related codes
    /// </summary>
    public string CodeClassId { get; set; } = string.Empty;

    /// <summary>
    /// Code alias for display or alternative reference
    /// </summary>
    public string CodeAlias { get; set; } = string.Empty;

    /// <summary>
    /// Code name in Korean
    /// </summary>
    public string CodeNameK { get; set; } = string.Empty;

    /// <summary>
    /// Code name in English
    /// </summary>
    public string CodeNameE { get; set; } = string.Empty;

    /// <summary>
    /// Code name in Chinese
    /// </summary>
    public string CodeNameC { get; set; } = string.Empty;

    /// <summary>
    /// Code name in Vietnamese
    /// </summary>
    public string CodeNameV { get; set; } = string.Empty;

    /// <summary>
    /// Locale-specific code name
    /// </summary>
    public string CodeNameLocale { get; set; } = string.Empty;

    /// <summary>
    /// Display sequence for ordering
    /// </summary>
    public int DspSeq { get; set; }

    /// <summary>
    /// Optional code value
    /// </summary>
    public string CodeOpt { get; set; } = string.Empty;

    /// <summary>
    /// Description of the code
    /// </summary>
    public string Description { get; set; } = string.Empty;
}
