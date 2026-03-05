namespace Sphere.Application.Common.Constants;

public static class SupportedLocales
{
    public static readonly Dictionary<string, string> Map = new()
    {
        ["ko"] = "ko-KR",
        ["en"] = "en-US",
        ["vi"] = "vi-VN",
        ["zh"] = "zh-CN",
    };

    public static string Resolve(string? raw)
    {
        if (string.IsNullOrWhiteSpace(raw)) return "ko-KR";
        var short_code = raw.Split(',')[0].Trim().Split('-')[0].ToLowerInvariant();
        return Map.TryGetValue(short_code, out var full) ? full : "ko-KR";
    }
}
