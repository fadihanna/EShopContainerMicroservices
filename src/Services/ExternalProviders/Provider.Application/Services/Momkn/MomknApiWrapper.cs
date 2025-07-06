using BuildingBlocks.Models;
using Provider.Application.Common.Interfaces;
using Provider.Application.Services.Momkn.Extensions;

namespace Provider.Application.Services.Momkn
{
    public class MomknApiWrapper : IExternalApiProvider
    {
        private readonly IMomknApiClient _client;

        public MomknApiWrapper(IMomknApiClient client)
        {
            _client = client;
        }

        public async Task<InquiryResponseModel> SendInquiryRequestAsync(InquiryRequestModel providerRequest)
        {
            var response = await _client.SendInquiryRequestAsync(providerRequest.ToMomknRequest());
            return response.MomknToStandard();
        }
        public async Task<PaymentResponseModel> SendPaymentRequestAsync(PaymentRequestModel providerRequest)
        {
            var response = await _client.SendPaymentRequestAsync(providerRequest.ToMomknRequest());
            return response.MomknToStandard();
        }
        public async Task<FeesResponseModel> SendInquiryFeesRequestAsync(FeesRequestModel feesRequestModel)
        {
            FeesResponseModel feesResponseModel = new FeesResponseModel("0","0",DateTime.Now.ToString(), 10,10,10, string.Empty);
          //  var response = await _client.SendPaymentRequestAsync(providerRequest.ToMomknRequest());
            return feesResponseModel;
        }
    }
}
