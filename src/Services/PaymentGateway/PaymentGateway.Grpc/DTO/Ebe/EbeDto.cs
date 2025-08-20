namespace PaymentGateway.Grpc.Dto.Ebe
{
    public class PrepareCheckoutRequestDto
    {
        public string EntityId { get; set; }
        public string EntityType { get; set; } 
        public string MerchantId { get; set; }
        public string CheckoutType { get; set; }
        public string CheckoutId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string PaymentType { get; set; } 
    }
    public class PrepareCheckoutResponseDto
    {
        public string Id { get; set; }
    }
    public class PaymentRequestDto
    {
        public string EntityId { get; set; }
        public string EntityType { get; set; }
        public string MerchantId { get; set; }
        public string PaymentType { get; set; } 
        public string PaymentBrand { get; set; } 
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string CardHolder { get; set; }
        public string CardNumber { get; set; }
        public string CardExpiryMonth { get; set; }
        public string CardExpiryYear { get; set; }
        public string CardCvv { get; set; }
        public string MerchantTransactionId { get; set; }
    }
    public class PaymentResponseDto
    {
        public string Id { get; set; }
        public string PaymentType { get; set; }
        public PaymentResult Result { get; set; }
        public string BuildNumber { get; set; }
        public DateTime Timestamp { get; set; }
        public string Ndc { get; set; }
    }
    public class PaymentResult
    {
        public string Code { get; set; }
        public string Description { get; set; }
    }
    public class RefundRequestDto
    {
        public string EntityId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string PaymentType { get; set; } = "RF";
        public string OriginalTransactionId { get; set; }
        public string MerchantTransactionId { get; set; }
    }
}
