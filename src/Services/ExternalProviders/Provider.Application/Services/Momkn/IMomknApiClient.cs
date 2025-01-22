using Provider.Application.Services.Momkn.Models;

namespace Provider.Application.Services.Momkn
{
    public interface IMomknApiClient
    {
        Task<MomknInquiryResponse> SendInquiryRequestAsync(MomknInquiryRequest providerRequest);
        Task<MomknPaymentResponse> SendPaymentRequestAsync(MomknPaymentRequest providerRequest);
    }
}
