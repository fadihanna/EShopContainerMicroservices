using Microsoft.Extensions.Configuration;
using System.Globalization;
using System.Resources;

namespace Magic.Infrastructure.Services.Internal
{
    public class LocalizationService : ILocalizationService
    {
        private readonly ResourceManager _resourceManager;
        private string _language;

        public LocalizationService(ResourceManager resourceManager, IConfiguration configuration)
        {
            _resourceManager = resourceManager;
            _language = configuration["Localization:DefaultCulture"] ?? Language.Arabic;
        }

        public string GetLocalizedMessage(string key)
        {
            var previousCulture = CultureInfo.CurrentUICulture;
            try
            {
                CultureInfo.CurrentUICulture = new CultureInfo(_language);
                return _resourceManager.GetString(key) ?? key;
            }
            finally
            {
                CultureInfo.CurrentUICulture = previousCulture;
            }
        }
    }
}
