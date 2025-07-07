using BuildingBlocks.Models;

namespace Magic.Application.Extensions
{
    public static class TransactionExtensions
    {
        public static TransactionDto ToTransactionDto(this Transaction transaction)
        {
            return DtoFromTransaction(transaction);
        }
        private static TransactionDto DtoFromTransaction(this Transaction transaction)
        {
            return new TransactionDto(
                Id: transaction.Id,
                UserId: transaction.UserId,
                Amount: transaction.Amount,
                Fees: transaction.Fees,
                TotalAmount: transaction.TotalAmount,
                RequestId: transaction.RequestId,
                DenominationId: transaction.DenominationId,
                PaymentProviderId: transaction.PaymentProviderId,
                Status: transaction.Status,
                BillingAccount: transaction.BillingAccount,
                quantity: 0,
                IsRefunded: transaction.IsRefunded
            );
        }
        public static Transaction DtoToTransaction(TransactionDto transactionDto)
        {
            return TransactionFromDto(transactionDto);
        }
        private static Transaction TransactionFromDto(TransactionDto dto)
        {
            return Transaction.Create(
                dto.IsRefunded,
                dto.UserId,
                dto.Amount,
                dto.Fees,
                dto.TotalAmount,
                dto.RequestId,
                dto.DenominationId,
                dto.PaymentProviderId,
                dto.Status,
                dto.BillingAccount
            );
        }
        public static List<TransactionDto> ToTransactionDtoList(this IEnumerable<Transaction> transactions)
        {
            return transactions?.Select(x => x.ToTransactionDto()).ToList() ?? new List<TransactionDto>();
        }
        public static Magic.Domain.Models.Transaction CreateTransaction(PaymentRequestModel transactionDto)
        {
            var newTransaction = Magic.Domain.Models.Transaction.Create(
                isRefunded: false,
                userId: transactionDto.UserId,
                amount: transactionDto.Amount,
                fees: transactionDto.Fees,
                totalAmount: transactionDto.TotalAmount,
                requestId: 3,
                denominationId: transactionDto.DenominationId,
                paymentProviderId: 1,
                status: 1,
                billingAccount: transactionDto.BillingAccount
            );
            return newTransaction;
        }
    }
}
