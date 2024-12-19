namespace Magic.Infrastructure.Services.External;

public abstract class ExternalModelMapperBase<TInquiryRequest, TInquiryResponse, TPaymentRequest, TPaymentResponse>
{
    public abstract TInquiryRequest MapInquiryRequest(InquiryRequestDto inquiryRequest);
    public abstract InquiryResponseDto MapInquiryResponse(TInquiryResponse providerResponse);
    public abstract TPaymentRequest MapPaymentRequest(PaymentRequestDto inquiryRequest);
    public abstract PaymentResponseDto MapPaymentResponse(TPaymentResponse providerResponse);
}