using Sphere.Domain.Common;

namespace Sphere.Domain.Entities.Screen;

/// <summary>
/// SPH5110 Grid entity for personal document grid.
/// </summary>
public class SPH5110Grid : SphereEntity
{
    public string DocId { get; set; } = string.Empty;
    public string DocName { get; set; } = string.Empty;
    public string DocType { get; set; } = string.Empty;
    public string DocTypeName { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string FilePath { get; set; } = string.Empty;
    public string FileName { get; set; } = string.Empty;
    public string FileExtension { get; set; } = string.Empty;
    public long FileSize { get; set; }
    public string FileSizeStr { get; set; } = string.Empty;
    public DateTime? UploadDate { get; set; }
    public string UploadDateStr { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ShareYn { get; set; } = "N";
    public string PublicYn { get; set; } = "N";
}
