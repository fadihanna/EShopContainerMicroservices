namespace Magic.Domain.Specifications
{
    public interface IDenominationSpecification : IGenericRepositoryAsync<Denomination>
    {
        Task<(int ProviderId, string ProviderCode, bool IsNullResult)> GetDenominationProviderCodeByIdAsync(int id, CancellationToken cancellationToken);
    }
}
