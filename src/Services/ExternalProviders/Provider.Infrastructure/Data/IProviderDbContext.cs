using Microsoft.EntityFrameworkCore;
using Provider.Domain.Models;

namespace Provider.Application.Data
{
    public interface IProviderDbContext
    {
        public DbSet<MasaryService> MasaryService { get; }
        public DbSet<MasaryServiceCharge> MasaryServiceCharge { get; }
        public DbSet<MasaryServiceParameter> MasaryServiceParameter { get; }
    }
}
