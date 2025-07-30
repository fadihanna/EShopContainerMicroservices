using BuildingBlocks.Models;
using Microsoft.Extensions.Options;
using Provider.Application.Common;
using Provider.Application.Common.Interfaces;
using Provider.Application.Configuration;
using Provider.Application.Dtos;
using Provider.Application.Repositories.Damen;
using Provider.Application.Services.Damen.Extensions;

namespace Provider.Application.Services.Damen
{
    public class DamenApiWrapper : IExternalApiProvider
    {
        private readonly IDamenApiClient _client;
        private readonly IDamenRepository _damenRepository;
        private readonly AppSettings _damenSettings;

        private readonly ApiExceptionHandler _exceptionHandler;

        public DamenApiWrapper(
            IDamenApiClient client,
            IDamenRepository damenRepository,
            IOptions<AppSettings> damenSettings,
            ApiExceptionHandler exceptionHandler)
        {
            _client = client;
            _damenRepository = damenRepository;
            _damenSettings = damenSettings.Value;
            _exceptionHandler = exceptionHandler;
        }

        public async Task<InquiryResponseModel> SendInquiryRequestAsync(InquiryRequestModel providerRequest)
        {
            return await _exceptionHandler.HandleApiExceptionsAsync(async () =>
            {
                var serviceRequests = await _damenRepository.GetServiceRequestsWithInputFieldsAsync(int.Parse(providerRequest.ProviderCode));

                //if (serviceRequests == null || !serviceRequests.Any())
                //    return ProviderResponseModel.Failed("Service configuration not found");

              var mainRequest = serviceRequests.FirstOrDefault(s => s.RequestType == "I");
                //if (mainRequest == null)
                //    return ProviderResponseModel.Failed("Payment request configuration not found");
                var damenInquiryRequest = providerRequest.ToDamenRequest(mainRequest);
                var response = await _client.SendInquiryRequestAsync(damenInquiryRequest, _damenSettings.ProviderSettings.DamenSettings);
                return response.DamenToStandard();
            }, "SendInquiryRequestAsync");
        }

        public async Task<PaymentResponseModel> SendPaymentRequestAsync(PaymentRequestModel providerRequest)
        {
            return await _exceptionHandler.HandleApiExceptionsAsync(async () =>
            {

                var serviceRequests = await _damenRepository.GetServiceRequestsWithInputFieldsAsync(int.Parse(providerRequest.ProviderCode));

                //if (serviceRequests == null || !serviceRequests.Any())
                //    return ProviderResponseModel.Failed("Service configuration not found");

                 var mainRequest = serviceRequests.FirstOrDefault(s => s.RequestType == "P");
                //if (mainRequest == null)
                //    return ProviderResponseModel.Failed("Payment request configuration not found");


                var damenPaymentRequest = providerRequest.ToDamenRequest( mainRequest);
                var response = await _client.SendPaymentRequestAsync(damenPaymentRequest, _damenSettings.ProviderSettings.DamenSettings);
                return response.DamenToStandard(providerRequest);
            }, "SendPaymentRequestAsync", providerRequest);
        }

        public async Task<List<ServiceParameterDto>> GetServiceParametersAsync(int serviceId)
        {
            return await _exceptionHandler.HandleApiExceptionsAsync(async () =>
            {
                var requestFields = await _damenRepository.GetServiceRequestsWithInputFieldsAsync(serviceId);

                return requestFields
                    .SelectMany(r => r.Inputs)
                    .Select(f => new ServiceParameterDto
                    {
                        Name = f.FieldName,
                        ParameterType = f.InputType
                    })
                    .Distinct()
                    .ToList();
            }, "GetServiceParametersAsync");
        }

        public async Task<FeesResponseModel> SendInquiryFeesRequestAsync(FeesRequestModel feesRequestModel)
        {
            return await _exceptionHandler.HandleApiExceptionsAsync(async () =>
            {
                var fees = await _damenRepository.GetServiceChargeAsync(
                    int.Parse(feesRequestModel.ProviderCode),
                    feesRequestModel.Amount
                );

                return new FeesResponseModel(
                    Status: "Success",
                    StatusText: "Charge retrieved successfully",
                    DateTime: DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ"),
                    Amount: feesRequestModel.Amount,
                    Fees: fees,
                    TotalAmount: feesRequestModel.Amount + fees,
                    ProviderReferenceNumber: feesRequestModel.RequestId.ToString()
                );
            }, "SendInquiryFeesRequestAsync");
        }
    }
}
