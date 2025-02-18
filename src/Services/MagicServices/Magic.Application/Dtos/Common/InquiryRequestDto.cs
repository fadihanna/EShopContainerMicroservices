namespace Magic.Application.Dtos.Common;
public record InquiryRequestDto(
    List<InputParameter> InputParameterList,
    int DenominationId,
    string BillingAccount);
public record InputParameter(
    string Key,
    string Value
);