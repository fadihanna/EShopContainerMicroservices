public interface IExternalApiProvider
{
    Task<InquiryResponseDto> SendInquiryRequestAsync(InquiryRequestDto providerRequest);
    Task<PaymentResponseDto> SendPaymentRequestAsync(PaymentRequestDto providerRequest);
}
