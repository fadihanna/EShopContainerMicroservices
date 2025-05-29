using Magic.Domain.Specifications;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace Magic.Infrastructure.Data.Specifications
{
    public class ServiceSpecification : GenericRepository<Service>, IServiceSpecification
    {
        public ServiceSpecification(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
        public async Task<List<Service>> GetAllWithDenominationsAsync(CancellationToken cancellationToken)
        {
            return await  _dbContext.Services
                .Include(s => s.Denominations)
                    .ThenInclude(g => g.DenominationGroup)
                .ToListAsync(cancellationToken);
        }
    }
}
