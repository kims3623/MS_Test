using System.Globalization;
using Sphere.Application.Common.Interfaces;

namespace Sphere.Infrastructure.Services;

public class LocaleService : ILocaleService
{
    public string CurrentLocale => CultureInfo.CurrentCulture.Name switch
    {
        var n when n.StartsWith("ko") => "ko-KR",
        var n when n.StartsWith("en") => "en-US",
        var n when n.StartsWith("vi") => "vi-VN",
        var n when n.StartsWith("zh") => "zh-CN",
        _ => "ko-KR"
    };
}
