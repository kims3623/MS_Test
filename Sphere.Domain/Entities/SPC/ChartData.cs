using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.SPC;

/// <summary>
/// Chart data entity for SPC chart plotting.
/// Maps to SPC chart data views/queries.
/// </summary>
public class ChartData : SphereEntity
{
    /// <summary>
    /// Specification system identifier
    /// </summary>
    public string SpecSysId { get; set; } = string.Empty;

    /// <summary>
    /// Data point sequence
    /// </summary>
    public int PointSeq { get; set; }

    /// <summary>
    /// Work date
    /// </summary>
    public string WorkDate { get; set; } = string.Empty;

    /// <summary>
    /// Shift
    /// </summary>
    public string Shift { get; set; } = string.Empty;

    /// <summary>
    /// X value (typically date/time or sequence)
    /// </summary>
    public string XValue { get; set; } = string.Empty;

    /// <summary>
    /// Y value (measurement or statistic)
    /// </summary>
    public decimal? YValue { get; set; }

    /// <summary>
    /// Chart type (X-bar, R, S, p, np, c, u)
    /// </summary>
    public string ChartType { get; set; } = string.Empty;

    /// <summary>
    /// Series name for multiple data series
    /// </summary>
    public string Series { get; set; } = string.Empty;

    /// <summary>
    /// Upper control limit at this point
    /// </summary>
    public decimal? Ucl { get; set; }

    /// <summary>
    /// Center line at this point
    /// </summary>
    public decimal? Cl { get; set; }

    /// <summary>
    /// Lower control limit at this point
    /// </summary>
    public decimal? Lcl { get; set; }

    /// <summary>
    /// Out of control flag
    /// </summary>
    public string OocYn { get; set; } = "N";

    /// <summary>
    /// Run rule violation flag
    /// </summary>
    public string RunRuleYn { get; set; } = "N";

    /// <summary>
    /// Violated run rule IDs
    /// </summary>
    public string ViolatedRules { get; set; } = string.Empty;
}
