using BuildingBlocks.Models;
using Provider.Application.Configuration;
using Provider.Application.Dtos;
using Provider.Application.Services.Masary.Models;
using Provider.Domain.Models;

namespace Provider.Application.Services.Masary.Extensions
{
    public static class MasaryInquiryExtensions
    {

        public static MasaryInquiryRequest ToMasaryRequest(this InquiryRequestModel inquiryRequestModel, MasarySettings settings, List<ServiceParameterDto> serviceParameters)
        {
            return MasaryFromStandard(inquiryRequestModel, settings, serviceParameters);
        }
        private static MasaryInquiryRequest MasaryFromStandard(InquiryRequestModel inquiryRequestModel, MasarySettings settings, List<ServiceParameterDto> serviceParameters)
        {
            List<MasaryInputParameter> inputParameter = new List<MasaryInputParameter>();

            if (serviceParameters != null && serviceParameters.Count > 0 && inquiryRequestModel.InputParameterList != null && inquiryRequestModel.InputParameterList.Count > 0)
            {
                for (int i = 0; i < serviceParameters.Count; i++)
                {
                    inputParameter.Add(new MasaryInputParameter(
                       Key: serviceParameters[i].Name,
                       Value: inquiryRequestModel.InputParameterList[i].Value
                   ));
                }
            }
            return new MasaryInquiryRequest(
                login: settings.MasaryAccountNumber,
                password: settings.MasaryPassword,
                terminal_id: settings.Terminal,
                action: settings.TransactionInquiryAction,
                version: settings.Version,
                language: settings.Language,
                data: new MasaryInquiryRequestData(
                    input_parameter_list: inputParameter,
                    service_version: settings.Version,
                    account_number: settings.MasaryAccountNumber,
                    service_id: int.Parse(inquiryRequestModel.ProviderCode),
                    external_id: inquiryRequestModel.RequestId
                )
            );
        }
        public static InquiryResponseModel MasaryToStandard(this MasaryInquiryResponse masaryInquiryResponse)
        {
            return StandardFromMasary(masaryInquiryResponse);
        }
        private static InquiryResponseModel StandardFromMasary(MasaryInquiryResponse? masaryInquiryResponse)
        {
            var detailsList = masaryInquiryResponse.data.info_text.Split('\n')
                .Select(line =>
                {
                    var colonIndex = line.IndexOf(':');
                    if (colonIndex < 0)
                    {
                        // Handle lines without colons (like the last Arabic line)
                        return new ResponseDetail("Note", line.Trim());
                    }

                    var key = line.Substring(0, colonIndex).Trim();
                    var value = line.Substring(colonIndex + 1).Trim();
                    return new ResponseDetail(key.Trim(), value);
                })
                .ToList();
             return new InquiryResponseModel(
                TransactionId: masaryInquiryResponse?.data?.transaction_id ?? "N/A",
                Status: masaryInquiryResponse?.data.status == 2 ? "Success" : "Fail",
                StatusText: masaryInquiryResponse?.data?.status_text,
                DateTime: DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"),
                Amount: masaryInquiryResponse?.data?.amount ?? 0.0,
                Fees: 0.0,
                DetailsList: detailsList
            );
        }
    }
}
