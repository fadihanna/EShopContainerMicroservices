namespace BuildingBlocks.Models
{
    public class PaymentRequestModel
    {
        public List<InputParameter> InputParameterList { get; set; }
        public decimal Amount { get; set; }
        public decimal Fees { get; set; }
        public decimal TotalAmount { get; set; }
        public int DenominationId { get; set; }
        public string BillingAccount { get; set; }
       // public string BillerCode { get; set; }
        public string RequestId { get; set; }
        public int quantity { get; set; }
        public int PaymentProviderId { get; set; }
        public string InquiryReferenceNumber { get; set; }
        public string ProviderTransactionId { get; set; }
        public string ProviderCode { get; set; }
        public int ProviderId { get; set; }
        public string UserId { get; set; }
    }
}
