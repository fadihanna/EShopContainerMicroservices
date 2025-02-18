namespace BuildingBlocks.Models
{
    public record PaymentRequestModel(
     decimal Amount,
     decimal Fees,
     decimal TotalAmount,
     int DenominationId,
     string BillingAccount,
     int quantity,
     int PaymentProviderId,
     string RefrenceTransactionId // refrenceNumber
    );
}
