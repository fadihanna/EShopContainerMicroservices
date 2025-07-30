using BuildingBlocks.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Provider.Application.Common;
using Provider.Application.Common.Interfaces;
using Provider.Application.Configuration;
using Provider.Application.Dtos;
using Provider.Application.Services.Masary.Extensions;
using Provider.Application.Services.Masary.Models;
using Provider.Domain.Repositories.Masary;

namespace Provider.Application.Services.Masary
{
    public class MasaryApiWrapper : IExternalApiProvider
    {
        private readonly IMasaryApiClient _client;
        private readonly IMasaryRepository _masaryRepository;
        private readonly AppSettings _masarySettings;
        private readonly ApiExceptionHandler _exceptionHandler;

        public MasaryApiWrapper(
            IMasaryApiClient client,
            IMasaryRepository masaryRepository,
            IOptions<AppSettings> masarySettings,
            ApiExceptionHandler exceptionHandler)
        {
            _client = client;
            _masaryRepository = masaryRepository;
            _masarySettings = masarySettings.Value;
            _exceptionHandler = exceptionHandler;
        }

        public async Task<InquiryResponseModel> SendInquiryRequestAsync(InquiryRequestModel providerRequest)
        {
          return await _exceptionHandler.HandleApiExceptionsAsync(async () =>
          {
            //  var serviceParameters = await GetServiceParametersAsync(int.Parse(providerRequest.ProviderCode));
             // var masaryInquiryRequest = providerRequest.ToMasaryRequest(_masarySettings.ProviderSettings.MasarySettings, serviceParameters);
              var response = await _client.SendInquiryRequestAsync(null, _masarySettings.ProviderSettings.MasarySettings.MasaryURLTransaction);
              return response.MasaryToStandard();
          }, "SendInquiryRequestAsync");
        }

        /*public async Task<InquiryResponseModel> SendInquiryRequestAsync(InquiryRequestModel providerRequest)
        {
            return await _exceptionHandler.HandleApiExceptionsAsync(async () =>
            {
                var serviceParameters = await GetServiceParametersAsync(int.Parse(providerRequest.ProviderCode));
                var masaryInquiryRequest = providerRequest.ToMasaryRequest(_masarySettings.ProviderSettings.MasarySettings, serviceParameters);
                var response = await _client.SendInquiryRequestAsync(masaryInquiryRequest, _masarySettings.ProviderSettings.MasarySettings.MasaryURLTransaction);
                return response.MasaryToStandard();
            }, "SendInquiryRequestAsync");
        }*/

        /*        public async Task<InquiryResponseModel> SendInquiryRequestAsync(InquiryRequestModel providerRequest)
                {
                    // Simulate a static response like from Masary
                    var jsonResponse = @"{
                   ""success"": true,
                   ""language"": ""ar"",    
                   ""action"": ""TransactionInquiry"",
                   ""version"": 2,
                   ""data"": {
                     ""transaction_id"": ""406414792908"",
                     ""status"": 2,
                     ""status_text"": ""ناجح"",
                     ""date_time"": ""05/03/2023 14:43:45"",
                     ""info_text"": ""Billing Account: 03100058253\nClient Name: ساره احمد سعدالدين محمود\nDue Date: 2023-04-02\nالقيمة المستحقة للفاتورة 1267"",
                     ""amount"": 1267.0,
                     ""min_amount"": 1.0,
                     ""max_amount"": 100000.0
                   }
            }";

                    // Deserialize into your Masary model
                    var masaryResponse = JsonConvert.DeserializeObject<MasaryInquiryResponse>(jsonResponse);

                    // Convert to standard InquiryResponseModel
                    return masaryResponse.MasaryToStandard(providerRequest);
                }
        */

        /*public async Task<PaymentResponseModel> SendPaymentRequestAsync(PaymentRequestModel providerRequest)
        {
            return await _exceptionHandler.HandleApiExceptionsAsync(async () =>
            {
                var serviceParameters = await GetServiceParametersAsync(int.Parse(providerRequest.ProviderCode));
                var masaryPaymentRequest = providerRequest.ToMasaryRequest(_masarySettings.ProviderSettings.MasarySettings, serviceParameters);
                var response = await _client.SendPaymentRequestAsync(masaryPaymentRequest, _masarySettings.ProviderSettings.MasarySettings.MasaryURLTransaction);
                return response.MasaryToStandard(providerRequest);
            }, "SendPaymentRequestAsync", providerRequest);
        }*/
        public async Task<PaymentResponseModel> SendPaymentRequestAsync(PaymentRequestModel providerRequest)
        {
            //return await _exceptionHandler.HandleApiExceptionsAsync(async () =>
            //{
                // Simulated JSON response (from Masary)
                var jsonResponse = @"{
          ""success"": true,
          ""language"": ""ar"",
          ""action"": ""TransactionPayment"",
          ""version"": 2,
          ""data"": {
            ""transaction_id"": ""322702204609"",
            ""status"": ""SUCCESS"",
            ""status_text"": ""ناجح"",
            ""date_time"": ""15/08/2022 02:06:08"",
            ""details_list"": [
              [
                { ""key"": ""pmt_id"", ""value"": ""322702204609"" },
                { ""key"": ""bill_description"", ""value"": ""محفوظ معوض ع الحميد - ش عوضهاشم من ال شاح ا0"" },
                { ""key"": ""bill_date"", ""value"": """" },
                { ""key"": ""amount_due"", ""value"": ""9.0"" },
                { ""key"": ""fee_amount"", ""value"": ""1.14"" },
                { ""key"": ""amount"", ""value"": ""10.14"" }
              ]
            ]
          }
        }";

                // Step 1: Deserialize JSON string into a strongly typed object
                var masaryResponse = JsonConvert.DeserializeObject<MasaryPaymentResponse>(jsonResponse);

                // Step 2: Map it to your standard model
                return masaryResponse.MasaryToStandard(providerRequest);
            //});
        }


        public async Task<List<ServiceParameterDto>> GetServiceParametersAsync(int serviceId)
        {
            return await _exceptionHandler.HandleApiExceptionsAsync(async () =>
            {
                var parameters = await _masaryRepository.GetServiceParametersAsync(serviceId);
                return parameters.Select(p => new ServiceParameterDto
                {
                    Name = p.Name,
                    ParameterType = p.ParameterType
                }).ToList();
            }, "GetServiceParametersAsync");
        }
        public async Task<FeesResponseModel> SendInquiryFeesRequestAsync(FeesRequestModel feesRequestModel)
        {
            return await _exceptionHandler.HandleApiExceptionsAsync(async () =>
            {
                var fees = await _masaryRepository.GetServiceChargeAsync(int.Parse(feesRequestModel.ProviderCode), feesRequestModel.Amount);//int.Parse(feesRequestModel.TransactionId)

                return new FeesResponseModel(
                    Status: "Success",
                    StatusText: "Charge retrieved successfully",
                    DateTime: DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ"),
                    Amount: feesRequestModel.Amount,
                    Fees: fees,
                    TotalAmount: feesRequestModel.Amount + fees,
                    ProviderReferenceNumber: feesRequestModel.RequestId.ToString()
                );
            }, "GetChargeAsync");
        }
    }
}
