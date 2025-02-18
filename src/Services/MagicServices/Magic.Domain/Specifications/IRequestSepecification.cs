namespace Magic.Domain.Specifications
{
    public interface IRequestSepecification
    {
        Task<int> InsertRequestAsync(Request requests, CancellationToken cancellationToken);
        Task<int> UpdateRequestAsync(Request requests, CancellationToken cancellationToken);
        Task<List<Request>> GetRequestByBillingAccount(string billingAccount, CancellationToken cancellationToken);
        Task<List<Request>> GetRequestByUserId(string userId, CancellationToken cancellationToken);
        Task<List<Request>> GetRequestByDenominationId(int denominationId, CancellationToken cancellationToken);
        Task UpdateRequestStatusAsync(int requestId, int requestStatus, CancellationToken cancellationToken);
    }
}
