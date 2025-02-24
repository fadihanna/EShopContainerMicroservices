namespace Magic.Domain.Specifications
{
    public interface IDenominationSpecification : IGenericRepositoryAsync<Denomination>
    {
        Task<(int ProviderId, string BillerCode, bool IsNullResult)> GetDenominationProviderCodeByIdAsync(int id, CancellationToken cancellationToken);
    }
}
