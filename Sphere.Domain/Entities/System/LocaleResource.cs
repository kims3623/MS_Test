using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.System;

/// <summary>
/// Locale resource entity for internationalization.
/// </summary>
public class LocaleResource : SphereEntity
{
    public string ResourceKey { get; set; } = string.Empty;
    public string ResourceCategory { get; set; } = string.Empty;
    public string ValueK { get; set; } = string.Empty;
    public string ValueE { get; set; } = string.Empty;
    public string ValueC { get; set; } = string.Empty;
    public string ValueV { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
