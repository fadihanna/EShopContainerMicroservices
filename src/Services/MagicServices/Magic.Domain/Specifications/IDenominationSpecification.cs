

namespace Magic.Domain.Specifications
{
    public interface IDenominationSpecification
    {
        Task<Denomination> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<List<Denomination>> GetAllAsync(CancellationToken cancellationToken);
    }
}
