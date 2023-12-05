using powerpage.Services;

using Microsoft.AspNetCore.Mvc.Razor.Internal;

using System.ComponentModel;
using System.Threading;
using Microsoft.AspNetCore.Http;

namespace powerpage.Framework;

public sealed class LocalizedDisplayNameAttribute : DisplayNameAttribute
{
    private readonly string _resourceKey;

    public LocalizedDisplayNameAttribute(string resourceKey) : base(resourceKey)
    {
        _resourceKey = resourceKey;
    }

    public override string DisplayName
    {
        get
        {
            var httpContext = new HttpContextAccessor().HttpContext;
            if (httpContext == null)
                return _resourceKey;

            ILanguageService languageService = (ILanguageService)httpContext.RequestServices.GetService(typeof(ILanguageService));
            ILocalizationService localizationService = (ILocalizationService)httpContext.RequestServices.GetService(typeof(ILocalizationService));

            var currentCulture = Thread.CurrentThread.CurrentUICulture.Name;
            var language = languageService.GetLanguageByCulture(currentCulture);

            if (language != null)
            {
                var stringResource = localizationService.GetStringResource(_resourceKey, _resourceKey, language.Id).Result;
                if (!string.IsNullOrEmpty(stringResource?.Value))
                {
                    return stringResource.Value;
                }
            }

            return _resourceKey;
        }
    }

    public string ResourceKey { get; }
}
