namespace BuildingBlocks.Models
{
    public class PaymentResponseModel
    {
        public bool IsSuccess { get; set; }
        public Status Status { get; set; }
        public PaymentResponseData? PaymentResponseData { get; set; }

    }
    public class PaymentResponseData
    {
        public string? RequestID { get; set; }
        public double? Amount { get; set; }
        public double? Fees { get; set; }
        public double? TotalAmount { get; set; }
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
