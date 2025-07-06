namespace BuildingBlocks.Models;
public record InquiryRequestModel(
    List<InputParameter> InputParameterList,
    int DenominationId,
    string BillingAccount,
    string RequestId,
    int ProviderId,
    string ProviderCode
);
