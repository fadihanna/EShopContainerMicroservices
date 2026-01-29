using Microsoft.EntityFrameworkCore;
using Provider.Domain.Models;

namespace Provider.Application.Data
{
    public interface IProviderDbContext
    {
        DbSet<MasaryService> MasaryService { get; }
        DbSet<MasaryServiceCharge> MasaryServiceCharge { get; }
        DbSet<MasaryServiceParameter> MasaryServiceParameter { get; }
    }
}
