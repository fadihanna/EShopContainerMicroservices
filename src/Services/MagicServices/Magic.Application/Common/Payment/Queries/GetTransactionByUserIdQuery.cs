using BuildingBlocks.Exceptions;
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
            if (transactions == null || transactions.Count == 0)
                throw new Exception("No Transactions Found for User");
            List<TransactionDto> transactionDtos = transactions.ToTransactionDtoList();
            return new GetTransactionByUserIdResponse(transactionDtos);
        }
    }
}
