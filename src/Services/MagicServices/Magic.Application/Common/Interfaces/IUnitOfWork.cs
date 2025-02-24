namespace Magic.Application.Common.Interfaces;

public interface IUnitOfWork
{
    DbSet<TEntity> Set<TEntity>() where TEntity : class;
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = new());
}
