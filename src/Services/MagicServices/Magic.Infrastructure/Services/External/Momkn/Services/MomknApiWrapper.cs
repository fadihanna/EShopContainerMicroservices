using Magic.Infrastructure.Services.External.Momkn.Models;
using MagicPaymentAPI.DTO.Login;
using MagicPaymentAPI.DTO.Order.Request;
using MagicPaymentAPI.DTO.Order.Response;
using MagicPaymentAPI.DTO.Payment;

namespace Magic.Infrastructure.Services.External.Momkn.Services
{
    public class MomknApiWrapper : IExternalApiProvider,
        IExternalModelMapper<MomknInquiryRequest, MomknInquiryResponse, MomknPaymentRequest, MomknPaymentResponse>
    {
        private readonly MomknApiClient _client;

        public MomknApiWrapper(MomknApiClient client)
        {
            _client = client;
        }

        public MomknInquiryRequest MapInquiryRequest(InquiryRequestDto inquiryRequest)
        {
            throw new NotImplementedException();
        }
        public async Task<InquiryResponseDto> SendInquiryRequestAsync(InquiryRequestDto providerRequest)
        {
            var request = MapInquiryRequest(providerRequest);
            var response = await _client.SendInquiryRequestAsync(request);
            return MapInquiryResponse(response);
        }
        public InquiryResponseDto MapInquiryResponse(MomknInquiryResponse providerResponse)
        {
            throw new NotImplementedException();
        }

        public MomknPaymentRequest MapPaymentRequest(PaymentRequestDto inquiryRequest)
        {
            throw new NotImplementedException();
        }
        public async Task<PaymentResponseDto> SendPaymentRequestAsync(PaymentRequestDto providerRequest)
        {
            var request = MapPaymentRequest(providerRequest);
            var response = await _client.SendPaymentRequestAsync(request);
            return MapPaymentResponse(response);
        }
        public PaymentResponseDto MapPaymentResponse(MomknPaymentResponse providerResponse)
        {
            throw new NotImplementedException();
        }

        public Task<LoginResponse> SendLoginPayMobRequestAsync()
        {
            throw new NotImplementedException();
        }

        public Task<CreateOrderResponse> SendCreateOrderPayMobRequestAsync(CreateOrderRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<PaymentResponse> SendPaymentPayMobRequestAsync(PaymentRequest request)
        {
            throw new NotImplementedException();
        }
    }
}