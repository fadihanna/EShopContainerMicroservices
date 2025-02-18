using Magic.Domain.Specifications;

namespace Magic.Application.Common.Payment.Queries
{
    public record GetTransactionByUserIdQuery(string UserId)
    : IQuery<GetTransactionByUserIdResponse>;
    public record GetTransactionByUserIdResponse(List<TransactionDto> transactionDto);
    public class GetTransactionByUserIdHandler
    : IQueryHandler<GetTransactionByUserIdQuery, GetTransactionByUserIdResponse>
    {
        private readonly ITransactionSpecification _transactionSpecification;
        public GetTransactionByUserIdHandler(ITransactionSpecification transactionSpecification)
        {
            _transactionSpecification = transactionSpecification;
        }
        public async Task<GetTransactionByUserIdResponse> Handle(GetTransactionByUserIdQuery query, CancellationToken cancellationToken)
        {
            List<Transaction> transactions = await _transactionSpecification.GetByUserId(query.UserId, cancellationToken);
            List<TransactionDto> transactionDtos = transactions.ToTransactionDtoList();
            return new GetTransactionByUserIdResponse(transactionDtos);
        }
    }
}
