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
