using Magic.Domain.Specifications;

namespace Magic.Infrastructure.Data.Specifications
{
    public class DenominationSpecification : GenericRepository<Denomination>, IDenominationSpecification
    {
        public DenominationSpecification(ApplicationDbContext dbContext) : base(dbContext)
        {
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
