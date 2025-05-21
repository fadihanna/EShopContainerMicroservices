using Magic.Domain.Specifications;

namespace Magic.Infrastructure.Data.Specifications
{
    public class ServiceSpecification : GenericRepository<Service>, IServiceSpecification
    {
        public ServiceSpecification(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
