using Magic.Domain.Specifications;
namespace Magic.Infrastructure.Data.Specifications
{
    public class TransactionSpecification : ITransactionSpecification
    {
        private readonly IApplicationDbContext _context;
        public TransactionSpecification(IApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }
        public Task<List<Transaction>> GetAllAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        public Task<Transaction> GetByDenominationId(DateTime CreatedAt, int DenominationId)
        {
            throw new NotImplementedException();
        }
        public async Task<Transaction> GetByInvoiceId(int invoiceId, CancellationToken cancellationToken)
        {
            return await _context.Transactions.FirstOrDefaultAsync(x => x.Id == invoiceId);
        }
        public Task<Transaction> GetByStatusId(int Status, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        public async Task<List<Transaction>> GetByUserId(string userId, CancellationToken cancellationToken)
        {
            return await _context.Transactions.Where(x=>x.UserId == userId).ToListAsync();
        }
        public async Task<int> InsertAsync(Transaction transaction, CancellationToken cancellationToken)
        {
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync(cancellationToken);
            return transaction.Id;
        }
        
    }
}
