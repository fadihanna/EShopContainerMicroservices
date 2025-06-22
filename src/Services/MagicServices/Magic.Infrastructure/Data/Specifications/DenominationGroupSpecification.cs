using Magic.Application.Interfaces.Specifications;
using Magic.Domain.Models.Lookups;
using Magic.Domain.Specifications;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Magic.Infrastructure.Data.Specifications
{
    public class DenominationGroupSpecification : IDenominationGroupSpecification
    {
        private readonly ApplicationDbContext _dbContext;

        public DenominationGroupSpecification(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<DenominationGroup>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.DenominationGroups
                 .ToListAsync(cancellationToken);
        }

        public async Task<DenominationGroup?> GetByIdAsync(Expression<Func<DenominationGroup, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _dbContext.DenominationGroups
                 .FirstOrDefaultAsync(predicate, cancellationToken);
        }

        public async Task AddAsync(DenominationGroup entity, CancellationToken cancellationToken)
        {
            await _dbContext.DenominationGroups.AddAsync(entity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(DenominationGroup entity, CancellationToken cancellationToken)
        {
            _dbContext.DenominationGroups.Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(DenominationGroup entity, CancellationToken cancellationToken)
        {
            _dbContext.DenominationGroups.Update(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
