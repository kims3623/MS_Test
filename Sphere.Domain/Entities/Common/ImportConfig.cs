using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.Common;

/// <summary>
/// Import configuration entity for data import settings.
/// Maps to SPC_IMPORT_CONFIG table.
/// </summary>
/// <remarks>
/// Composite PK: DivSeq + ImportId
/// Defines import mappings, validation rules, and template specifications.
/// Supports Excel, CSV import types.
/// </remarks>
public class ImportConfig : SphereEntity
{
    /// <summary>
    /// Import configuration identifier (PK)
    /// </summary>
    public string ImportId { get; set; } = string.Empty;

    /// <summary>
    /// Import configuration name
    /// </summary>
    public string ImportName { get; set; } = string.Empty;

    /// <summary>
    /// Import type (EXCEL, CSV)
    /// </summary>
    public string ImportType { get; set; } = "EXCEL";

    /// <summary>
    /// Target screen/page identifier
    /// </summary>
    public string ScreenId { get; set; } = string.Empty;

    /// <summary>
    /// Column mapping configuration as JSON
    /// </summary>
    public string ColumnMapping { get; set; } = string.Empty;

    /// <summary>
    /// Validation rules as JSON
    /// </summary>
    public string ValidationRules { get; set; } = string.Empty;

    /// <summary>
    /// Import template file path
    /// </summary>
    public string TemplateFile { get; set; } = string.Empty;
}
