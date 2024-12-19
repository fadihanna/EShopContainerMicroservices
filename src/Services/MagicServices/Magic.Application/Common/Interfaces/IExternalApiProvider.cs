namespace Magic.Application.Common.Interfaces;
public interface IExternalApiProvider
{
    Task<InquiryResponseDto> SendInquiryRequestAsync(InquiryRequestDto providerRequest);
    Task<PaymentResponseDto> SendPaymentRequestAsync(PaymentRequestDto providerRequest);
}
