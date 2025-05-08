using Magic.Application.Interfaces.Specifications;
using Magic.Domain.Models.Lookups;
 
using System.Linq.Expressions;

namespace Magic.Infrastructure.Data.Specifications
{
    public class ProviderSpecification : IProviderSpecification
    {
        private readonly ApplicationDbContext _dbContext;

        public ProviderSpecification(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Magic.Domain.Models.Lookups.Provider>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.Providers
                 .ToListAsync(cancellationToken);
        }

        public async Task<Magic.Domain.Models.Lookups.Provider?> GetByIdAsync(Expression<Func<Magic.Domain.Models.Lookups.Provider, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _dbContext.Providers
                 .FirstOrDefaultAsync(predicate, cancellationToken);
        }

        public async Task AddAsync( Magic.Domain.Models.Lookups.Provider provider, CancellationToken cancellationToken)
        {
            await _dbContext.Providers.AddAsync(provider, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync( Magic.Domain.Models.Lookups.Provider provider, CancellationToken cancellationToken)
        {
            _dbContext.Providers.Remove(provider);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
        public async Task UpdateAsync(Magic.Domain.Models.Lookups.Provider provider, CancellationToken cancellationToken)  // ✅ أضفنا هذه الدالة
        {
            _dbContext.Providers.Update(provider);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
