namespace Magic.Application.Dtos.Common;

public record PaymentResponseDto(
     string providerTransactionId,
     string transactionId,
     string Status,
     string StatusText,
     string TransactionTime,
     string Amount,
     string Fees,
     string totalAmount,
     string billingAccount,
     List<ResponseDetail> DetailsList
);
