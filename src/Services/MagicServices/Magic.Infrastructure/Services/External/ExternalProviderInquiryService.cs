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
                ProviderCode = request.ProviderCode,
                ProviderId = request.ProviderId
            };

            inquiryRequestProto.InputParameterList.AddRange(
                request.InputParameterList.Select(p => new InputParameter { Key = p.Key, Value = p.Value })
            );

            var response = await _providerInquiryProto.InquiryAsync(inquiryRequestProto);

            return new InquiryResponseModel(
                TransactionId: response.TransactionId,
                Status: response.Status,
                StatusText: response.StatusText,
                DateTime: response.DateTime,
                Fees: response.Fees,
                Amount: response.Amount,
                DetailsList: response.DetailsList.Select(d => new Details(Key: d.Key, Value: d.Value)).ToList()
           );
        }
    }
}
