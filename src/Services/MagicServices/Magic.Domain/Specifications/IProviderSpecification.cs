using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Magic.Domain.Models.Lookups;

namespace Magic.Application.Interfaces.Specifications
{
    public interface IProviderSpecification
    {
        Task<List<Provider>> GetAllAsync(CancellationToken cancellationToken);
        Task<Provider?> GetByIdAsync(Expression<Func<Provider, bool>> predicate, CancellationToken cancellationToken);
        Task AddAsync(Provider provider, CancellationToken cancellationToken);
        Task DeleteAsync(Provider provider, CancellationToken cancellationToken);
        Task UpdateAsync(Provider provider, CancellationToken cancellationToken);
    }
}
