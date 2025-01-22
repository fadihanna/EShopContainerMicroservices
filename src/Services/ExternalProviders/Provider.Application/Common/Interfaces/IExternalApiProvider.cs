using BuildingBlocks.Models;

namespace Provider.Application.Common.Interfaces
{
    public interface IExternalApiProvider
    {
        Task<InquiryResponseModel> SendInquiryRequestAsync(InquiryRequestModel providerRequest);
        Task<PaymentResponseModel> SendPaymentRequestAsync(PaymentRequestModel providerRequest);
    }
}
