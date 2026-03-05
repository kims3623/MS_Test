using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.Common;

/// <summary>
/// Customer entity for managing customers.
/// Maps to SPC_CUST table.
/// </summary>
public class Cust : SphereEntity
{
    /// <summary>
    /// Customer identifier (PK)
    /// </summary>
    public string CustId { get; set; } = string.Empty;

    /// <summary>
    /// Customer name (default/display)
    /// </summary>
    public string CustName { get; set; } = string.Empty;

    /// <summary>
    /// Customer name in Korean
    /// </summary>
    public string CustNameK { get; set; } = string.Empty;

    /// <summary>
    /// Customer name in English
    /// </summary>
    public string CustNameE { get; set; } = string.Empty;

    /// <summary>
    /// Customer name in Chinese
    /// </summary>
    public string CustNameC { get; set; } = string.Empty;

    /// <summary>
    /// Customer name in Vietnamese
    /// </summary>
    public string CustNameV { get; set; } = string.Empty;

    /// <summary>
    /// Customer type code
    /// </summary>
    public string CustType { get; set; } = string.Empty;

    /// <summary>
    /// Display sequence for ordering
    /// </summary>
    public int DspSeq { get; set; }
}
