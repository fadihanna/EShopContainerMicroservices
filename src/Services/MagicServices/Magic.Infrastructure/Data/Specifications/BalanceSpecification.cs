using Magic.Application.Data;
using Magic.Domain.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magic.Infrastructure.Data.Specifications
{
    public class BalanceSpecification : IBalanceSpecification
    {
        private readonly IApplicationDbContext _context;
        public BalanceSpecification(IApplicationDbContext applicationDbContext) 
        {
            _context = applicationDbContext;
        }
        public async Task<int> DeductAsync(Balance balance, CancellationToken cancellationToken)
        {
            _context.Balances.Add(balance);
            await _context.SaveChangesAsync(cancellationToken);
            return balance.Id;
        }
        public async Task<List<Balance>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Balances.AsNoTracking().ToListAsync();
        }
        public async Task<Balance> GetByCenterId(string userId, CancellationToken cancellationToken)
        {
            return await _context.Balances.AsNoTracking().Where(x => x.UserId == userId).FirstOrDefaultAsync(cancellationToken);
            //return await _context.Balances.AsNoTracking().Where(x=>x.UserId == userId).FirstOrDefaultAsync(cancellationToken);
        }
        public async Task<Balance> GetByUserId(string userId, CancellationToken cancellationToken)
        {
            return await _context.Balances.AsNoTracking().Where(x => x.UserId == userId).FirstOrDefaultAsync(cancellationToken);
        }
        public async Task<Balance> GetByInvoiceId(string invoiceId, CancellationToken cancellationToken)
        {
            return await _context.Balances.AsNoTracking().Where(x => x.InvoiceId == invoiceId).FirstOrDefaultAsync(cancellationToken);
        }
        public async Task<int> RefundAsync(Balance balance, CancellationToken cancellationToken)
        {
            var balanceResult = await _context.Balances.AsNoTracking().Where(x => x.InvoiceId == balance.InvoiceId && x.UserId == x.UserId).FirstOrDefaultAsync(cancellationToken);
            balanceResult.CurrentBalance += balanceResult.Amount;
            _context.Balances.Update(balanceResult);
            return balanceResult.Id;
        } 
    }
}
