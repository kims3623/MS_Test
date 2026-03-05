namespace Sphere.Application.DTOs.User;

/// <summary>
/// Personal document master DTO.
/// </summary>
public class PersonalDocMasterDto
{
    public string PersonalDocItem { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string TitleE { get; set; } = string.Empty;
    public string ContentE { get; set; } = string.Empty;
}

/// <summary>
/// Personal document detail DTO.
/// </summary>
public class PersonalDocDetailDto
{
    public string PersonalDocItem { get; set; } = string.Empty;
    public string PersonalDocSubitem { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string TitleE { get; set; } = string.Empty;
    public string ContentE { get; set; } = string.Empty;
}
