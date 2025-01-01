using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Magic.Infrastructure.Services.Internal
{
    public class LanguageService : ILanguageService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private string _language;
        public LanguageService(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _language = configuration["Localization:DefaultCulture"] ?? Language.Arabic;
        }

        public string GetLanguage()
        {
            return _httpContextAccessor.HttpContext?.Items["Language"]?.ToString() ?? _language;
        }

        public void SetLanguage(string language)
        {
            _httpContextAccessor.HttpContext.Items["Language"] = language;
        }
    }
}