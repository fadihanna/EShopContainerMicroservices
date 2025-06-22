using Magic.Domain.Models.Lookups;
using System.Linq.Expressions;

namespace Magic.Application.Interfaces.Specifications
{
    public interface IDenominationGroupSpecification
    {
        Task<List<DenominationGroup>> GetAllAsync(CancellationToken cancellationToken);
        Task<DenominationGroup?> GetByIdAsync(Expression<Func<DenominationGroup, bool>> predicate, CancellationToken cancellationToken);
        Task AddAsync(DenominationGroup entity, CancellationToken cancellationToken);
        Task UpdateAsync(DenominationGroup entity, CancellationToken cancellationToken);
        Task DeleteAsync(DenominationGroup entity, CancellationToken cancellationToken);
    }
}
