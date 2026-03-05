using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.Common;

/// <summary>
/// Grid column entity for configuring data grid column settings.
/// Maps to SPC_GRID_COLUMN table.
/// </summary>
/// <remarks>
/// Composite PK: DivSeq + GridId + ColumnId
/// Defines column properties including width, alignment, sorting, and filtering options.
/// Supports multi-language column headers.
/// </remarks>
public class GridColumn : SphereEntity
{
    /// <summary>
    /// Grid identifier (PK)
    /// </summary>
    public string GridId { get; set; } = string.Empty;

    /// <summary>
    /// Column identifier (PK)
    /// </summary>
    public string ColumnId { get; set; } = string.Empty;

    /// <summary>
    /// Column display name (default locale)
    /// </summary>
    public string ColumnName { get; set; } = string.Empty;

    /// <summary>
    /// Column name in Korean
    /// </summary>
    public string ColumnNameK { get; set; } = string.Empty;

    /// <summary>
    /// Column name in English
    /// </summary>
    public string ColumnNameE { get; set; } = string.Empty;

    /// <summary>
    /// Data type (string, number, date, boolean)
    /// </summary>
    public string DataType { get; set; } = "string";

    /// <summary>
    /// Column width in pixels
    /// </summary>
    public int Width { get; set; } = 100;

    /// <summary>
    /// Text alignment (left, center, right)
    /// </summary>
    public string Align { get; set; } = "left";

    /// <summary>
    /// Column visibility flag (Y/N)
    /// </summary>
    public string VisibleYn { get; set; } = "Y";

    /// <summary>
    /// Sortable flag (Y/N)
    /// </summary>
    public string SortableYn { get; set; } = "Y";

    /// <summary>
    /// Filterable flag (Y/N)
    /// </summary>
    public string FilterableYn { get; set; } = "Y";

    /// <summary>
    /// Display sequence for column ordering
    /// </summary>
    public int DspSeq { get; set; }

    /// <summary>
    /// Display format pattern (e.g., date format, number format)
    /// </summary>
    public string Format { get; set; } = string.Empty;
}
