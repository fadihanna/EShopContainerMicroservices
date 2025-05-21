using BuildingBlocks.Models;

namespace Provider.Application.Common
{
    public class ProviderServiceInquiryImplement
    {
        private readonly ExternalApiProviderFactory _providerFactory;
        public ProviderServiceInquiryImplement(ExternalApiProviderFactory providerFactory)
        {
            _providerFactory = providerFactory;
        }
        public async Task<InquiryResponseModel> Inquiry(InquiryRequestModel request)
        {
            var providerService = _providerFactory.GetProviderService((CommonEnums.Provider)request.ProviderId);
            var response = await providerService.SendInquiryRequestAsync(request);
            return response;
        }
        public async Task<FeesResponseModel> GetFees(FeesRequestModel request)
        {
            var providerService = _providerFactory.GetProviderService((CommonEnums.Provider)request.ProviderId);
            var response = await providerService.SendInquiryFeesRequestAsync(request);
            return response;
        }
    }
}
