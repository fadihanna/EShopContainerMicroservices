using Magic.Application.Data;
using Magic.Domain.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Magic.Infrastructure.Data.Specifications
{
    public class RequestSpecification : IRequestSepecification
    {
        private readonly IApplicationDbContext _dbContext;

        public RequestSpecification(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Request>> GetRequestByBillingAccount(string billingAccount, CancellationToken cancellationToken)
        {
            return await _dbContext.Requests.Where(x => x.BillingAccount == billingAccount).ToListAsync();
        }

        public async Task<List<Request>> GetRequestByDenominationId(int denominationId, CancellationToken cancellationToken)
        {
            return await _dbContext.Requests.Where(x => x.DenominationId == denominationId).ToListAsync();
        }

        public async Task<List<Request>> GetRequestByUserId(string userId, CancellationToken cancellationToken)
        {
            return await _dbContext.Requests.Where(x => x.UserId == userId).ToListAsync();
        }

        public async Task<int> InsertRequestAsync(Request requests, CancellationToken cancellationToken)
        {
            _dbContext.Requests.Add(requests);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return requests.Id;
        }

        public async Task<int> UpdateRequestAsync(Request requests, CancellationToken cancellationToken)
        {
            _dbContext.Requests.Update(requests);
            _dbContext.SaveChangesAsync(cancellationToken);

            return requests.Id;
        }

        public async Task UpdateRequestStatusAsync(int requestId, int requestStatus, CancellationToken cancellationToken)
        {
            await _dbContext.Requests
                   .Where(r => r.Id == requestId)
                   .ExecuteUpdateAsync(r => r.SetProperty(r => r.Status, Convert.ToInt32(requestStatus))
                                              .SetProperty(r => r.ResponseDate, DateTime.UtcNow),
                                       cancellationToken);
        }
    }
}
