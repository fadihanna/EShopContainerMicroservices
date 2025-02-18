namespace Magic.Application.Data;
public interface IApplicationDbContext
{
    DbSet<DenominationFee> DenominationFees { get; }
    DbSet<DenominationGroup> DenominationGroups { get; }
    DbSet<DenominationInputParameter> DenominationInputParameters { get; }
    DbSet<DenominationProviderCode> DenominationProviderCodes { get; }
    DbSet<Provider> Providers { get; }
    DbSet<InternalErrorCodeLookup> InternalErrorCodeLookups { get; }
    DbSet<ServiceCategory> ServiceCategories { get; }
    DbSet<Service> Services { get; }
    DbSet<Denomination> Denominations { get; }
    DbSet<Balance> Balances { get; }
    DbSet<Transaction> Transactions { get; }
    DbSet<PaymentProvider> PaymentProviders { get; }
    DbSet<Request> Requests { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}