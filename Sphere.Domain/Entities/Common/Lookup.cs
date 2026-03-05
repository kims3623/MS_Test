using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.Common;

/// <summary>
/// Lookup entity for generic lookup/reference data.
/// Maps to SPC_LOOKUP table.
/// </summary>
/// <remarks>
/// Composite PK: DivSeq + LookupId
/// Provides flexible key-value storage for reference data.
/// Supports hierarchical lookups through parent code relationship.
/// </remarks>
public class Lookup : SphereEntity
{
    /// <summary>
    /// Lookup identifier (PK)
    /// </summary>
    public string LookupId { get; set; } = string.Empty;

    /// <summary>
    /// Lookup category/type
    /// </summary>
    public string LookupType { get; set; } = string.Empty;

    /// <summary>
    /// Lookup code value
    /// </summary>
    public string LookupCode { get; set; } = string.Empty;

    /// <summary>
    /// Lookup display name (default locale)
    /// </summary>
    public string LookupName { get; set; } = string.Empty;

    /// <summary>
    /// Lookup name in Korean
    /// </summary>
    public string LookupNameK { get; set; } = string.Empty;

    /// <summary>
    /// Lookup name in English
    /// </summary>
    public string LookupNameE { get; set; } = string.Empty;

    /// <summary>
    /// Lookup value for processing
    /// </summary>
    public string LookupValue { get; set; } = string.Empty;

    /// <summary>
    /// Display sequence
    /// </summary>
    public int DspSeq { get; set; }

    /// <summary>
    /// Parent lookup code for hierarchical structure
    /// </summary>
    public string ParentCode { get; set; } = string.Empty;
}
