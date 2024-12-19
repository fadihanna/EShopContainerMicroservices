namespace Magic.Application.Common.Interfaces
{
    public interface IExternalApiProviderFactory
    {
        IExternalApiProvider GetProvider(DomainEnums.Provider provider);
    }
}
