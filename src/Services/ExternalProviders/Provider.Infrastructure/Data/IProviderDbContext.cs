using Microsoft.EntityFrameworkCore;
using Provider.Domain.Models;

namespace Provider.Application.Data
{
    public interface IProviderDbContext
    {
        public DbSet<MasaryService> MasaryService { get; }
        public DbSet<MasaryServiceCharge> MasaryServiceCharge { get; }
        public DbSet<MasaryServiceParameter> MasaryServiceParameter { get; }
        public DbSet<DamenService> DamenServices { get; set; }
        public DbSet<DamenServiceField> DamenServiceFields { get; set; }
        public DbSet<DamenServiceCharge> DamenServiceCharges { get; set; }
        public DbSet<DamenServiceRequest> DamenServiceRequests { get; set; }
        public DbSet<FawryBiller> FawryBillers { get; set; }
        public DbSet<FawryBillerInfo> FawryBillerInfos { get; set; }
        public DbSet<FawryServiceCharge> ServiceCharge { get; set; }
        public DbSet<FawryPaymentItem> FawryPaymentItems { get; set; }
    }
}
