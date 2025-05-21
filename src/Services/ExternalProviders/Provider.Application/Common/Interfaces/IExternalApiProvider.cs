using BuildingBlocks.Models;

namespace Provider.Application.Common.Interfaces
{
    public interface IExternalApiProvider
    {
        Task<InquiryResponseModel> SendInquiryRequestAsync(InquiryRequestModel providerRequest);
        Task<FeesResponseModel> SendInquiryFeesRequestAsync(FeesRequestModel feesRequestModel);
        Task<PaymentResponseModel> SendPaymentRequestAsync(PaymentRequestModel providerRequest);
    }
}
