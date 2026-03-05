using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.Common;

/// <summary>
/// Actual product entity for managing production information.
/// Maps to SPC_ACT_PROD table.
/// </summary>
public class ActProd : SphereEntity
{
    /// <summary>
    /// Actual product identifier (PK)
    /// </summary>
    public string ActProdId { get; set; } = string.Empty;

    /// <summary>
    /// Actual product name (default/display)
    /// </summary>
    public string ActProdName { get; set; } = string.Empty;

    /// <summary>
    /// Actual product name in Korean
    /// </summary>
    public string ActProdNameK { get; set; } = string.Empty;

    /// <summary>
    /// Actual product name in English
    /// </summary>
    public string ActProdNameE { get; set; } = string.Empty;

    /// <summary>
    /// Actual product name in Chinese
    /// </summary>
    public string ActProdNameC { get; set; } = string.Empty;

    /// <summary>
    /// Actual product name in Vietnamese
    /// </summary>
    public string ActProdNameV { get; set; } = string.Empty;

    /// <summary>
    /// Display sequence for ordering
    /// </summary>
    public int DspSeq { get; set; }

    /// <summary>
    /// Description of the product
    /// </summary>
    public string Description { get; set; } = string.Empty;
}
