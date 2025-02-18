using Provider.Application.Common.Interfaces;

namespace Magic.Application.Common.Interfaces
{
    public interface IExternalApiProviderFactory
    {
        IExternalApiProvider GetProvider(DomainEnums.Provider provider);
    }
}
