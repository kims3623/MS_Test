using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.SPC;

/// <summary>
/// SPH3010 chart entity for X-bar R chart data.
/// Maps to SPH3010 screen data.
/// </summary>
public class SPH3010Chart : SphereEntity
{
    /// <summary>
    /// Chart data identifier
    /// </summary>
    public string ChartId { get; set; } = string.Empty;

    /// <summary>
    /// Specification system identifier
    /// </summary>
    public string SpecSysId { get; set; } = string.Empty;

    /// <summary>
    /// Work date
    /// </summary>
    public string WorkDate { get; set; } = string.Empty;

    /// <summary>
    /// Shift
    /// </summary>
    public string Shift { get; set; } = string.Empty;

    /// <summary>
    /// Sample group number
    /// </summary>
    public int GroupNo { get; set; }

    /// <summary>
    /// X-bar value (average)
    /// </summary>
    public decimal? XBar { get; set; }

    /// <summary>
    /// R value (range)
    /// </summary>
    public decimal? RValue { get; set; }

    /// <summary>
    /// S value (standard deviation)
    /// </summary>
    public decimal? SValue { get; set; }

    /// <summary>
    /// X-bar UCL
    /// </summary>
    public decimal? XBarUcl { get; set; }

    /// <summary>
    /// X-bar CL
    /// </summary>
    public decimal? XBarCl { get; set; }

    /// <summary>
    /// X-bar LCL
    /// </summary>
    public decimal? XBarLcl { get; set; }

    /// <summary>
    /// R UCL
    /// </summary>
    public decimal? RUcl { get; set; }

    /// <summary>
    /// R CL
    /// </summary>
    public decimal? RCl { get; set; }

    /// <summary>
    /// R LCL
    /// </summary>
    public decimal? RLcl { get; set; }

    /// <summary>
    /// Out of control flag for X-bar
    /// </summary>
    public string XBarOocYn { get; set; } = "N";

    /// <summary>
    /// Out of control flag for R
    /// </summary>
    public string ROocYn { get; set; } = "N";

    /// <summary>
    /// Run rule violation flag
    /// </summary>
    public string RunRuleYn { get; set; } = "N";
}
