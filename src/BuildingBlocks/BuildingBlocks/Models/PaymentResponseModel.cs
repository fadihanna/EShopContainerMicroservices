namespace BuildingBlocks.Models
{
    public class PaymentResponseModel
    {
        public bool IsSuccess { get; set; }
        public Status Status { get; set; }
        public PaymentResponseData? PaymentResponseData { get; set; }

        public int TransactionId { get; set; }
        public string ProviderTransactionId { get; set; }
        public string UserId { get; set; }
        public decimal TotalAmount { get; set; }
        public string Message { get; set; }
        public string Code { get; set; }
    }
    public class PaymentResponseData
    {
        public string? RequestID { get; set; }
        public decimal? Amount { get; set; }
        public decimal? Fees { get; set; }
        public decimal? TotalAmount { get; set; }
        public string? TransactionId { get; set; }
        public string? Datetime { get; set; }
        public string? ResponseCode { get; set; }
        public List<PaymentResponseDetails>? paymentResponseDetails { get; set; }

    }
    public class PaymentResponseDetails
    {
        public string? Key { get; set; }
        public string? Value { get; set; }
    }
    public class Status
    {
        public string? StatusCode { get; set; }
        public string? StatusText { get; set; }
    }
}
