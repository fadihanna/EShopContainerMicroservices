using BuildingBlocks.Models;
using Microsoft.Extensions.Options;
using Provider.Application.Common;
using Provider.Application.Common.Helpers;
using Provider.Application.Common.Interfaces;
using Provider.Application.Configuration;
using Provider.Application.DTOs;
using Provider.Application.Services.Masary.Extensions;
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
                var serviceParameters = await GetServiceParametersAsync(int.Parse(providerRequest.BillerCode));
                var masaryInquiryRequest = providerRequest.ToMasaryRequest(_masarySettings.ProviderSettings.MasarySettings, serviceParameters);
                var response = await _client.SendInquiryRequestAsync(masaryInquiryRequest, _masarySettings.ProviderSettings.MasarySettings.MasaryURLTransaction);
                return response.MasaryToStandard();
            }, "SendInquiryRequestAsync");
        }

        public async Task<PaymentResponseModel> SendPaymentRequestAsync(PaymentRequestModel providerRequest)
        {
            return await _exceptionHandler.HandleApiExceptionsAsync(async () =>
            {
                var serviceParameters = await GetServiceParametersAsync(int.Parse(providerRequest.BillerCode));
                var masaryPaymentRequest = providerRequest.ToMasaryRequest(_masarySettings.ProviderSettings.MasarySettings, serviceParameters);
                var response = await _client.SendPaymentRequestAsync(masaryPaymentRequest, _masarySettings.ProviderSettings.MasarySettings.MasaryURLTransaction);
                return response.MasaryToStandard(providerRequest);
            }, "SendPaymentRequestAsync", providerRequest);
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

        public async Task<double> GetChargeAsync(FeesRequestModel feesRequestModel)
        {
            return await _exceptionHandler.HandleApiExceptionsAsync(async () =>
            {
                return await _masaryRepository.GetServiceChargeAsync(feesRequestModel.ServiceId, feesRequestModel.Amount);
            }, "GetChargeAsync");
        }
    }
}
