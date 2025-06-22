using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Magic.Domain.Specifications
{
    public interface INotificationSpecification
    {
        Task<List<Notification>> GetAllAsync(CancellationToken cancellationToken);
        Task<Notification?> GetByIdAsync(Expression<Func<Notification, bool>> predicate, CancellationToken cancellationToken);
        Task AddAsync(Notification notification, CancellationToken cancellationToken);
        Task DeleteAsync(Notification notification, CancellationToken cancellationToken);
        Task UpdateAsync(Notification notification, CancellationToken cancellationToken);
    }

}
