using BuildingBlocks.Models;
using Provider.Application.Common.Interfaces;
using Provider.Application.Services.Masary.Extensions;

namespace Provider.Application.Services.Masary
{
    public class MasaryApiWrapper : IExternalApiProvider
    {
        private readonly IMasaryApiClient _client;
        public MasaryApiWrapper(IMasaryApiClient client)
        {
            _client = client;
        }

        public async Task<InquiryResponseModel> SendInquiryRequestAsync(InquiryRequestModel providerRequest)
        {
            var response = await _client.SendInquiryRequestAsync(providerRequest.ToMasaryRequest());
            return response.MasaryToStandard();
        }
        public async Task<PaymentResponseModel> SendPaymentRequestAsync(PaymentRequestModel providerRequest)
        {
            var response = await _client.SendPaymentRequestAsync(providerRequest.ToMasaryRequest());
            return response.MasaryToStandard();
        }
    }
}
