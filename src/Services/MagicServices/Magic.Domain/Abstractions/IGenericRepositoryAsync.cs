using System.Linq.Expressions;

namespace Magic.Domain.Abstractions;

public interface IGenericRepositoryAsync<T> where T : class
{
    Task<T> GetByIdAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);
    Task<List<T>> GetAllAsync(CancellationToken cancellationToken);
    Task<T> InsertAsync(T entity, CancellationToken cancellationToken);
    Task UpdateAsync(T entity, CancellationToken cancellationToken);
    Task DeleteAsync(T entity, CancellationToken cancellationToken);
    Task<List<T>> GetPagedReponseAsync(int page, int size, CancellationToken cancellationToken);
}
