using BuildingBlocks.Models;
using Provider.Grpc.Protos;
using Details = BuildingBlocks.Models.Details;
using InputParameter = Provider.Grpc.Protos.InputParameter;

namespace Magic.Infrastructure.Services.External
{
    public class ExternalProviderInquiryService : IExternalProviderInquiryService
    {
        private readonly ProviderInquiryProtoService.ProviderInquiryProtoServiceClient _providerInquiryProto;

        public ExternalProviderInquiryService(ProviderInquiryProtoService.ProviderInquiryProtoServiceClient providerInquiryProto)
        {
            _providerInquiryProto = providerInquiryProto;
        }

        public async Task<InquiryResponseModel> InquiryAsync(InquiryRequestModel request, CancellationToken cancellationToken)
        {
            InquiryRequest inquiryRequestProto = new InquiryRequest
            {
                BillingAccount = request.BillingAccount,
                DenominationId = request.DenominationId,
                RequestId = request.RequestId,
                BillerCode = request.BillerCode,
                ProviderId = request.ProviderId
            };

            // Map InputParameterList from InquiryRequestModel to InquiryRequest
            inquiryRequestProto.InputParameterList.AddRange(
                request.InputParameterList.Select(p => new InputParameter { Key = p.Key, Value = p.Value })
            );

            var response = await _providerInquiryProto.InquiryAsync(inquiryRequestProto);

            // Map the response to InquiryResponseModel
            return new InquiryResponseModel(
                TransactionId: response.TransactionId,
                Status: response.Status,
                StatusText: response.StatusText,
                DateTime: response.DateTime,
                DetailsList: response.DetailsList.Select(d => new Details (Key : d.Key, Value : d.Value)).ToList()
           );
        }
    }
}
