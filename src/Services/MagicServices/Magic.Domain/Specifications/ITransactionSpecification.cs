using Magic.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Magic.Domain.Specifications
{
    public interface ITransactionSpecification
    {
        Task<int> InsertAsync(Transaction transaction, CancellationToken cancellationToken);
        Task<Transaction> GetByInvoiceId(int invoiceId, CancellationToken cancellationToken);
        Task<List<Transaction>> GetByUserId(string userId, CancellationToken cancellationToken);
        Task<List<Transaction>> GetAllAsync(CancellationToken cancellationToken);
        Task<Transaction> GetByDenominationId(DateTime CreatedAt, int DenominationId);
        Task<Transaction> GetByStatusId(int Status, CancellationToken cancellationToken);
    }
}
