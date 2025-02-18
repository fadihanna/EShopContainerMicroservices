using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magic.Domain.Specifications
{
    public interface IBalanceSpecification
    {
        Task<int> DeductAsync(Balance balance, CancellationToken cancellationToken);
        Task<int> RefundAsync(Balance balance, CancellationToken cancellationToken);
        Task<Balance> GetByInvoiceId(string invoiceId, CancellationToken cancellationToken);
        Task<Balance> GetByUserId(string userId, CancellationToken cancellationToken);
        Task<List<Balance>> GetAllAsync(CancellationToken cancellationToken);

    }
}
