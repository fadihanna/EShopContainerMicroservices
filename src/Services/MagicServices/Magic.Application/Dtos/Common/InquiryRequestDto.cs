namespace Magic.Application.Dtos.Common;
//this is the client request coming from "frontend"
public record InquiryRequestDto(
    List<InputParameter> InputParameterList,
    int DenominationId,
    string BillingAccount
);
public record InputParameter(
    string Key,
    string Value
);