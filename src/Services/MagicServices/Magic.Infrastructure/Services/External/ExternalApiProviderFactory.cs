using Magic.Infrastructure.Services.External.Masary.Services;
using Magic.Infrastructure.Services.External.Momkn.Services;

namespace Magic.Infrastructure.Services.External;
public class ExternalApiProviderFactory : IExternalApiProviderFactory
{
    private readonly MasaryApiWrapper _masaryApiWrapper;
    private readonly MomknApiWrapper _momknApiWrapper;

    public ExternalApiProviderFactory(MasaryApiWrapper masaryApiWrapper, MomknApiWrapper momknApiWrapper)
    {
        _masaryApiWrapper = masaryApiWrapper;
        _momknApiWrapper = momknApiWrapper;
    }
    public IExternalApiProvider GetProvider(DomainEnums.Provider provider)
    {
        return provider switch
        {
            DomainEnums.Provider.Masary => _masaryApiWrapper,
            DomainEnums.Provider.Momkn => _momknApiWrapper,
            _ => throw new ArgumentException("Invalid provider specified")
        };
    }
}