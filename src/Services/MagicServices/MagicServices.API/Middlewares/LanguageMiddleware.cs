using Magic.Application.Common;
using Magic.Application.Common.Interfaces;

namespace MagicServices.API.Middlewares
{
    public class LanguageMiddleware
    {
        private readonly RequestDelegate _next;

        public LanguageMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, ILanguageService languageService, IConfiguration configuration)
        {
            var defaultCulture = configuration["Localization:DefaultCulture"] ?? Language.Arabic;
            var language = context.Request.Headers["Accept-Language"].FirstOrDefault()
                       ?? context.Request.RouteValues["culture"]?.ToString()
                       ?? defaultCulture;

            languageService.SetLanguage(language);
            await _next(context);
        }
    }
}
