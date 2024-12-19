using Magic.Infrastructure.Services.External.Masary.Models;

namespace Magic.Infrastructure.Services.External.Masary.Services
{
    public class MasaryApiWrapper : IExternalApiProvider,
        IExternalModelMapper<MasaryInquiryRequest, MasaryInquiryResponse, MasaryPaymentRequest, MasaryPaymentResponse>
    {
        private readonly MasaryApiClient _client;
        public MasaryApiWrapper(MasaryApiClient client)
        {
            _client = client;
        }

        public MasaryInquiryRequest MapInquiryRequest(InquiryRequestDto inquiryRequest)
        {
            throw new NotImplementedException();
        }
        public async Task<InquiryResponseDto> SendInquiryRequestAsync(InquiryRequestDto providerRequest)
        {
            var request = MapInquiryRequest(providerRequest);
            var response = await _client.SendInquiryRequestAsync(request);
            return MapInquiryResponse(response);
        }
        public InquiryResponseDto MapInquiryResponse(MasaryInquiryResponse providerResponse)
        {
            throw new NotImplementedException();
        }

        public MasaryPaymentRequest MapPaymentRequest(PaymentRequestDto inquiryRequest)
        {
            throw new NotImplementedException();
        }
        public async Task<PaymentResponseDto> SendPaymentRequestAsync(PaymentRequestDto providerRequest)
        {
            var request = MapPaymentRequest(providerRequest);
            var response = await _client.SendPaymentRequestAsync(request);
            return MapPaymentResponse(response);
        }
        public PaymentResponseDto MapPaymentResponse(MasaryPaymentResponse providerResponse)
        {
            throw new NotImplementedException();
        }

    }
}