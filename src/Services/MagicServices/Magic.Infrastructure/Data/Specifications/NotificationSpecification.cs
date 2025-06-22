using Magic.Domain.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Magic.Infrastructure.Data.Specifications
{
    public class NotificationSpecification : INotificationSpecification
    {
        private readonly ApplicationDbContext _context;

        public NotificationSpecification(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Notification>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Notifications.ToListAsync(cancellationToken);
        }

        public async Task<Notification?> GetByIdAsync(Expression<Func<Notification, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _context.Notifications.FirstOrDefaultAsync(predicate, cancellationToken);
        }

        public async Task AddAsync(Notification notification, CancellationToken cancellationToken)
        {
            await _context.Notifications.AddAsync(notification, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Notification notification, CancellationToken cancellationToken)
        {
            _context.Notifications.Remove(notification);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(Notification notification, CancellationToken cancellationToken)
        {
            _context.Notifications.Update(notification);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
