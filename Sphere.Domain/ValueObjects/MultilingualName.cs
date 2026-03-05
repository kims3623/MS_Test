namespace Sphere.Domain.ValueObjects;

/// <summary>
/// Value object for multilingual name support.
/// Supports Korean, English, Chinese, Vietnamese, and locale-specific names.
/// </summary>
public record MultilingualName
{
    /// <summary>
    /// Korean name (_k suffix in OutSystems)
    /// </summary>
    public string Korean { get; init; } = string.Empty;

    /// <summary>
    /// English name (_e suffix in OutSystems)
    /// </summary>
    public string English { get; init; } = string.Empty;

    /// <summary>
    /// Chinese name (_c suffix in OutSystems)
    /// </summary>
    public string Chinese { get; init; } = string.Empty;

    /// <summary>
    /// Vietnamese name (_v suffix in OutSystems)
    /// </summary>
    public string Vietnamese { get; init; } = string.Empty;

    /// <summary>
    /// Locale-specific name (fallback)
    /// </summary>
    public string Locale { get; init; } = string.Empty;

    /// <summary>
    /// Gets the name for the specified locale code.
    /// </summary>
    /// <param name="localeCode">Locale code (e.g., "ko-KR", "en-US", "zh-CN", "vi-VN")</param>
    /// <returns>The localized name, falling back to Korean if not found</returns>
    public string GetByLocale(string localeCode) => localeCode switch
    {
        "ko-KR" or "ko" => Korean,
        "en-US" or "en" => English,
        "zh-CN" or "zh" => Chinese,
        "vi-VN" or "vi" => Vietnamese,
        _ => !string.IsNullOrEmpty(Locale) ? Locale : Korean
    };

    /// <summary>
    /// Creates a MultilingualName from individual language values.
    /// </summary>
    public static MultilingualName Create(
        string korean = "",
        string english = "",
        string chinese = "",
        string vietnamese = "",
        string locale = "")
    {
        return new MultilingualName
        {
            Korean = korean,
            English = english,
            Chinese = chinese,
            Vietnamese = vietnamese,
            Locale = locale
        };
    }
}
