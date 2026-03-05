using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.Common;

/// <summary>
/// Date range entity for predefined date range presets.
/// Maps to SPC_DATE_RANGE table.
/// </summary>
/// <remarks>
/// Composite PK: DivSeq + RangeId
/// Provides reusable date range definitions for filters and reports.
/// Supports relative date ranges (e.g., "Last 7 days", "This month").
/// </remarks>
public class DateRange : SphereEntity
{
    /// <summary>
    /// Date range identifier (PK)
    /// </summary>
    public string RangeId { get; set; } = string.Empty;

    /// <summary>
    /// Range display name (default locale)
    /// </summary>
    public string RangeName { get; set; } = string.Empty;

    /// <summary>
    /// Range name in Korean
    /// </summary>
    public string RangeNameK { get; set; } = string.Empty;

    /// <summary>
    /// Range name in English
    /// </summary>
    public string RangeNameE { get; set; } = string.Empty;

    /// <summary>
    /// Range type (RELATIVE, FIXED, CUSTOM)
    /// </summary>
    public string RangeType { get; set; } = string.Empty;

    /// <summary>
    /// Days back from today for start date
    /// </summary>
    public int DaysBack { get; set; }

    /// <summary>
    /// Days forward from today for end date
    /// </summary>
    public int DaysForward { get; set; }

    /// <summary>
    /// Display sequence
    /// </summary>
    public int DspSeq { get; set; }
}
