namespace Magic.Infrastructure.Services.External;

public interface IExternalModelMapper<TInquiryRequest, TInquiryResponse, TPaymentRequest, TPaymentResponse>
{
    TInquiryRequest MapInquiryRequest(InquiryRequestDto inquiryRequest);
    InquiryResponseDto MapInquiryResponse(TInquiryResponse providerResponse);
    TPaymentRequest MapPaymentRequest(PaymentRequestDto inquiryRequest);
    PaymentResponseDto MapPaymentResponse(TPaymentResponse providerResponse);
}