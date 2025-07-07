namespace BuildingBlocks.Models
{
    public record PaymentResponseModel
    (
        bool IsSuccess,
        string Status,
        string StatusText,
        string TransactionTime,
        int TransactionId,
        string ProviderTransactionId,
        string UserId,
        string Amount,
        string Fees,
        string TotalAmount,
        string BillingAccount,
        List<ResponseDetail> DetailsList
    );
}
