namespace BuildingBlocks.Models
{
    public class PaymentResponseModel
    {
        public int TransactionId { get; set; }
        public string ProviderTransactionId { get; set; }
        public string UserId { get; set; }
        public decimal TotalAmount { get; set; }
        public string Message { get; set; }
        public string Code { get; set; }
    }
}
