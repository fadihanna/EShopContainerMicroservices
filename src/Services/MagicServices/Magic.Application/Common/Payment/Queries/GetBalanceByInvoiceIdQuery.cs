using Magic.Domain.Specifications;
namespace Magic.Application.Common.Payment.Queries
{
    public record GetBalanceByInvoiceIdQuery(string invoiceId)
    : IQuery<GetBalanceByInvoiceIdResponse>;
    public record GetBalanceByInvoiceIdResponse(BalanceDto balanceDto);
    public class GetBalanceByInvoiceIdHandler
    : IQueryHandler<GetBalanceByInvoiceIdQuery, GetBalanceByInvoiceIdResponse>
    {
        private readonly IBalanceSpecification _balanceSpecification;
        public GetBalanceByInvoiceIdHandler(IBalanceSpecification balanceSpecification)
        {
            _balanceSpecification = balanceSpecification;
        }
        public async Task<GetBalanceByInvoiceIdResponse> Handle(GetBalanceByInvoiceIdQuery query, CancellationToken cancellationToken)
        {
            var balance = await _balanceSpecification.GetByInvoiceId(query.invoiceId, cancellationToken);
            return new GetBalanceByInvoiceIdResponse(balance!.ToBalanceDto());
        }
    }
}
