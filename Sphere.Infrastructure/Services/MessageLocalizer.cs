using Sphere.Application.Common.Interfaces;
using Sphere.Application.Common.Resources;

namespace Sphere.Infrastructure.Services;

public class MessageLocalizer : IMessageLocalizer
{
    private readonly ILocaleService _localeService;

    public MessageLocalizer(ILocaleService localeService)
    {
        _localeService = localeService;
    }

    public string Get(string key)
    {
        return ErrorMessages.Get(key, _localeService.CurrentLocale);
    }
}
