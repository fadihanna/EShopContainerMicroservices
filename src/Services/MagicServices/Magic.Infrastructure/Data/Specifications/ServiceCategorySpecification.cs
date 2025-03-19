using Magic.Domain.Specifications;

namespace Magic.Infrastructure.Data.Specifications
{
    public class ServiceCategorySpecification : GenericRepository<ServiceCategory>, IServiceCategorySpecification
    {
        public ServiceCategorySpecification(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
      
    }
}
