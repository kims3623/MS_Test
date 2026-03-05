using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.Common;

/// <summary>
/// Export configuration entity for data export settings.
/// Maps to SPC_EXPORT_CONFIG table.
/// </summary>
/// <remarks>
/// Composite PK: DivSeq + ExportId
/// Defines export templates including columns, filters, and file format options.
/// Supports Excel, CSV, PDF export types.
/// </remarks>
public class ExportConfig : SphereEntity
{
    /// <summary>
    /// Export configuration identifier (PK)
    /// </summary>
    public string ExportId { get; set; } = string.Empty;

    /// <summary>
    /// Export configuration name
    /// </summary>
    public string ExportName { get; set; } = string.Empty;

    /// <summary>
    /// Export type (EXCEL, CSV, PDF)
    /// </summary>
    public string ExportType { get; set; } = "EXCEL";

    /// <summary>
    /// Target screen/page identifier
    /// </summary>
    public string ScreenId { get; set; } = string.Empty;

    /// <summary>
    /// Column list as JSON array
    /// </summary>
    public string ColumnList { get; set; } = string.Empty;

    /// <summary>
    /// Filter criteria as JSON
    /// </summary>
    public string FilterJson { get; set; } = string.Empty;

    /// <summary>
    /// Template file path for formatted exports
    /// </summary>
    public string TemplateFile { get; set; } = string.Empty;

    /// <summary>
    /// Maximum rows to export
    /// </summary>
    public int MaxRows { get; set; } = 10000;
}
