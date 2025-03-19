using Magic.Infrastructure.Data.Identity.Entity;

namespace Magic.Infrastructure.Data;

public interface IApplicationDbContext
{
    DbSet<DenominationFee> DenominationFees { get; }
    DbSet<DenominationGroup> DenominationGroups { get; }
    DbSet<DenominationInputParameter> DenominationInputParameters { get; }
    DbSet<DenominationProviderCode> DenominationProviderCodes { get; }
    DbSet<Lookups.Provider> Providers { get; }
    DbSet<InternalErrorCodeLookup> InternalErrorCodeLookups { get; }
    DbSet<ServiceCategory> ServiceCategories { get; }
    DbSet<Service> Services { get; }
    DbSet<Denomination> Denominations { get; }
    DbSet<Transaction> Transactions { get; }
    DbSet<PaymentProvider> PaymentProviders { get; }
    DbSet<Request> Requests { get; }
    DbSet<RefreshToken> RefreshTokens { get; }
    DbSet<ConsumerUser> ConsumerUsers { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}