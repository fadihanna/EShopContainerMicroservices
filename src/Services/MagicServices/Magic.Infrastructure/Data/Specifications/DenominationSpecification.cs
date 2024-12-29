using Magic.Application.Data;
using Magic.Domain.Specifications;

namespace Magic.Infrastructure.Data.Specifications
{
    public class DenominationSpecification : IDenominationSpecification
    {
        private readonly IApplicationDbContext _dbContext;
        public DenominationSpecification(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> InsertDenominationAsync(Denomination denomination, CancellationToken cancellationToken)
        {
            _dbContext.Denominations.Add(denomination);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return denomination.Id;
        }

        public async Task<List<Denomination>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.Denominations.AsNoTracking().ToListAsync();
        }

        public async Task<Denomination> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _dbContext.Denominations.AsNoTracking().Where(o => o.IsActive && o.Id.Equals(id)).FirstOrDefaultAsync(cancellationToken);
        }
    }
}
