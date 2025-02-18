namespace Magic.Domain.Specifications
{
    public interface IDenominationSpecification
    {

        Task<int> InsertDenominationAsync(Denomination denomination, CancellationToken cancellationToken);
        Task<Denomination> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<List<Denomination>> GetAllAsync(CancellationToken cancellationToken);
        Task<(int ProviderId, string BillerCode, bool IsNullResult)> GetDenominationProviderCodeByIdAsync(int id, CancellationToken cancellationToken);

    }
}
