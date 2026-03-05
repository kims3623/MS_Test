using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.TPS;

/// <summary>
/// Alarm attachment file entity for file metadata.
/// </summary>
public class AlarmAttachFile : SphereEntity
{
    public string FileName { get; set; } = string.Empty;
    public string FileExtension { get; set; } = string.Empty;
    public long FileSize { get; set; }
}
