namespace Magic.Application.Dtos.Common;
public record PaymentResponseDto (
     string providerTransactionId,
     string invoiceId,
     string code,
     string message,
     DateTime transactionTime,
     string totalAmount,
     string billingAccount,
     int denominationId,
     string userId,
     List<Details> DetailsList
);
