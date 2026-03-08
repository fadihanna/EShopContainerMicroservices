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
                IsRefunded: transaction.IsRefunded,
                ProviderTransactionId : transaction.ProviderTransactionId,
                PaymentProviderTransactionId : transaction.PaymentProviderTransactionId
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
                dto.BillingAccount,
                dto.ProviderTransactionId,
                dto.PaymentProviderTransactionId

            );
        }
        public static List<TransactionDto> ToTransactionDtoList(this IEnumerable<Transaction> transactions)
        {
            return transactions?.Select(x => x.ToTransactionDto()).ToList() ?? new List<TransactionDto>();
        }
        public static Magic.Domain.Models.Transaction CreateTransaction(PaymentRequestModel transactionDto, int requestId)
        {
            var newTransaction = Magic.Domain.Models.Transaction.Create(
                isRefunded: false,
                userId: transactionDto.UserId,
                amount: transactionDto.Amount,
                fees: transactionDto.Fees,
                totalAmount: transactionDto.TotalAmount,
                requestId: Convert.ToInt32(transactionDto.RequestId),
                denominationId: transactionDto.DenominationId,
                paymentProviderId: transactionDto.PaymentProviderId,
                status: 1,
                billingAccount: transactionDto.BillingAccount,
                providerTransactionId : transactionDto.ProviderTransactionId,
                paymentProviderTransactionId : transactionDto.PaymentProviderTransactionId
            );
            return newTransaction;
        }
    }
}
