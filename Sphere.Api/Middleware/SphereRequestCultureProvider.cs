using Microsoft.AspNetCore.Localization;
using Sphere.Application.Common.Constants;

namespace Sphere.Api.Middleware;

public class SphereRequestCultureProvider : RequestCultureProvider
{
    public override Task<ProviderCultureResult?> DetermineProviderCultureResult(HttpContext httpContext)
    {
        var acceptLang = httpContext.Request.Headers["Accept-Language"].FirstOrDefault();
        var resolved = SupportedLocales.Resolve(acceptLang);
        return Task.FromResult<ProviderCultureResult?>(new ProviderCultureResult(resolved, resolved));
    }
}
