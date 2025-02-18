using Magic.Domain.Specifications;

namespace Magic.Application.Common.Payment.Queries
{
    public record GetTransactionByIdQuery(int Id)
    : IQuery<GetTransactionByIdResponse>;
    public record GetTransactionByIdResponse(TransactionDto transactionDto);
    public class GetTransactionByIdHandler
    : IQueryHandler<GetTransactionByIdQuery, GetTransactionByIdResponse>
    {
        private readonly ITransactionSpecification _transactionSpecification;
        public GetTransactionByIdHandler(ITransactionSpecification transactionSpecification)
        {
            _transactionSpecification = transactionSpecification;
        }
        public async Task<GetTransactionByIdResponse> Handle(GetTransactionByIdQuery query, CancellationToken cancellationToken)
        {
            var transaction = await _transactionSpecification.GetByInvoiceId(query.Id, cancellationToken);
            return new GetTransactionByIdResponse(transaction!.ToTransactionDto());
        }
    }
}
