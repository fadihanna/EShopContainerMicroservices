using Provider.Application.Services.Masary.Models;

namespace Provider.Application.Services.Masary
{
    public interface IMasaryApiClient
    {
        Task<MasaryInquiryResponse> SendInquiryRequestAsync(MasaryInquiryRequest providerRequest, string url);
        Task<MasaryPaymentResponse> SendPaymentRequestAsync(MasaryPaymentRequest providerRequest , string url);
    }
}
