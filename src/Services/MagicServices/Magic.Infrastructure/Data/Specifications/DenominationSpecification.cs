using Magic.Domain.Specifications;
using System.Linq.Expressions;

namespace Magic.Infrastructure.Data.Specifications
{
    public class DenominationSpecification : GenericRepository<Denomination>, IDenominationSpecification
    {
        public DenominationSpecification(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public override async Task<List<Denomination>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.Denominations
                .Include(d => d.Amounts)
                .Include(d => d.DenominationInputParameters).ToListAsync();
        }
        public override async Task<Denomination?> GetByIdAsync(Expression<Func<Denomination, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _dbContext.Denominations
                .Include(d => d.Amounts)  
                .Include(d => d.DenominationInputParameters)
                .FirstOrDefaultAsync(predicate, cancellationToken);
        }

        public async Task<(int ProviderId, string BillerCode, bool IsNullResult)> GetDenominationProviderCodeByIdAsync(int id, CancellationToken cancellationToken)
        {
            var result = await (from d in _dbContext.Denominations
                                join dpc in _dbContext.DenominationProviderCodes
                                on d.Id equals dpc.DenominationId
                                where d.ProviderId == dpc.ProviderId && d.Id == id
                                select new
                                {
                                    d.ProviderId,
                                    dpc.BillerCode
                                })
                        .FirstOrDefaultAsync(cancellationToken);

            if (result == null)
                return (default, default, true);

            return (result.ProviderId, result.BillerCode, false);
        }
    }
}
