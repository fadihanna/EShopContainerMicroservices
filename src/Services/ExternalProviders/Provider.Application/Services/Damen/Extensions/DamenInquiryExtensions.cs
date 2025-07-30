using BuildingBlocks.Models;
using Provider.Application.Dtos;
using Provider.Application.Services.Damen.Models;

namespace Provider.Application.Services.Damen.Extensions
{
    public static class DamenInquiryExtensions
    {
        public static DamenInquiryRequest ToDamenRequest(
            this InquiryRequestModel inquiryRequestModel,
            DamenServiceRequestDto mainRequest)
        {
            return DamenFromStandard(inquiryRequestModel, mainRequest);
        }

        private static DamenInquiryRequest DamenFromStandard(
            InquiryRequestModel inquiryRequestModel,
            DamenServiceRequestDto mainRequest)
        {
            Dictionary<string, string> serviceDataFields = new();

            if (mainRequest?.Inputs != null && inquiryRequestModel.InputParameterList != null)
            {
                for (int i = 0; i < mainRequest.Inputs.Count && i < inquiryRequestModel.InputParameterList.Count; i++)
                {
                    serviceDataFields[mainRequest.Inputs[i].FieldName] = inquiryRequestModel.InputParameterList[i].Value;
                }
            }

            return new DamenInquiryRequest
            {
                ser_id = inquiryRequestModel.ProviderCode,
                request_type = mainRequest?.RequestType ?? "inquiry",
                request_name = mainRequest?.Name ?? "inquiry_request",
                serviceDataFileds = serviceDataFields
            };
        }

        public static InquiryResponseModel DamenToStandard(this DamenInquiryResponse damenInquiryResponse)
        {
            return StandardFromDamen(damenInquiryResponse);
        }

        private static InquiryResponseModel StandardFromDamen(DamenInquiryResponse response)
        {
            var detailsList = response.result.userServiceDataFileds
                .Select(kvp => new ResponseDetail(kvp.Key, kvp.Value))
                .ToList();

            return new InquiryResponseModel(
                TransactionId: response.result.inquire_id ?? "N/A",
                Status: response.status.code == "200" ? "Success" : "Fail",
                StatusText: response.status.msg?.UserMessageAr ?? response.status.msg?.UserMessage ?? "No message",
                DateTime: DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"),
                Amount: 0.0,
                Fees: 0.0,
                DetailsList: detailsList
            );
        }
    }
}
