using BuildingBlocks.Models;

namespace Magic.Application.Common.Interfaces
{
    public interface IExternalProviderInquiryService
    {
        Task<InquiryResponseModel> InquiryAsync(InquiryRequestModel request, CancellationToken cancellationToken);
    }
}
