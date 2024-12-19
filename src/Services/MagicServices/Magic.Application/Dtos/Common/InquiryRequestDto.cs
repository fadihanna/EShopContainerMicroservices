namespace Magic.Application.Dtos.Common;
public record InquiryRequestDto(
    List<InputParameter> InputParameterList,
    int DenominationId,
    string BillingAccount,
    string ExternalId
);
public record InputParameter(
    string Key,
    string Value
);