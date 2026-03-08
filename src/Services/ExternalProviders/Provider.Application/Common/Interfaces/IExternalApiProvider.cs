using BuildingBlocks.Models;

namespace Provider.Application.Common.Interfaces
{
    public interface IExternalApiProvider
    {
        Task<InquiryResponseModel> SendInquiryRequestAsync(InquiryRequestModel providerRequest);
        Task<InquiryResponseModel> SendInquiryMockupAsync() => throw new NotImplementedException("Mockup not available for this provider");
        Task<FeesResponseModel> SendInquiryFeesRequestAsync(FeesRequestModel feesRequestModel);
        Task<PaymentResponseModel> SendPaymentRequestAsync(PaymentRequestModel providerRequest);
    }
}
