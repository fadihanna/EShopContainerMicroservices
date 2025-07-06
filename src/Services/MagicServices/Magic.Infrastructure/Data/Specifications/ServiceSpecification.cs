using Magic.Domain.Specifications;

namespace Magic.Infrastructure.Data.Specifications
{
    public class ServiceSpecification : GenericRepository<Service>, IServiceSpecification
    {
        public ServiceSpecification(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
        public async Task<List<Service>> GetServiceDenominationGroupAsync(int categoryId, CancellationToken cancellationToken)
        {
            return await _dbContext.Services.Where(s => s.ServiceCategoryId == categoryId)
            .Include(s => s.DenominationGroups)
            .ThenInclude(s => s.Denominations)
            .ThenInclude(x=>x.DenominationInputParameters)
            .ToListAsync(cancellationToken);
        }
        public async Task<List<Service>> GetServiceDenominationAsync(int categoryId, CancellationToken cancellationToken)
        {
            return await _dbContext.Services.Where(s => s.ServiceCategoryId == categoryId)
            .Include(s => s.Denominations)
            .ThenInclude(x=>x.DenominationInputParameters)
            .ToListAsync(cancellationToken);
        }
    }
}
