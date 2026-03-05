using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.Common;

/// <summary>
/// Project entity for managing projects.
/// Maps to SPC_PROJECT table.
/// </summary>
public class Project : SphereEntity
{
    /// <summary>
    /// Project identifier (PK)
    /// </summary>
    public string ProjectId { get; set; } = string.Empty;

    /// <summary>
    /// Project name (default/display)
    /// </summary>
    public string ProjectName { get; set; } = string.Empty;

    /// <summary>
    /// Project name in Korean
    /// </summary>
    public string ProjectNameK { get; set; } = string.Empty;

    /// <summary>
    /// Project name in English
    /// </summary>
    public string ProjectNameE { get; set; } = string.Empty;

    /// <summary>
    /// Project name in Chinese
    /// </summary>
    public string ProjectNameC { get; set; } = string.Empty;

    /// <summary>
    /// Project name in Vietnamese
    /// </summary>
    public string ProjectNameV { get; set; } = string.Empty;
}
