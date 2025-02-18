using Magic.Application.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Provider.Application.Common.Interfaces;
using Provider.Application.Services.Masary;
using Provider.Application.Services.Momkn;

namespace Provider.Application.Common;

public class ExternalApiProviderFactory
{
    private readonly IServiceProvider _serviceProvider;

    public ExternalApiProviderFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IExternalApiProvider GetProviderService(CommonEnums.Provider provider)
    {
        return provider switch
        {
            CommonEnums.Provider.Masary => _serviceProvider.GetRequiredService<MasaryApiWrapper>(),
             _ => throw new NotImplementedException($"No provider found for ID: {provider}")
        };
    }
}
