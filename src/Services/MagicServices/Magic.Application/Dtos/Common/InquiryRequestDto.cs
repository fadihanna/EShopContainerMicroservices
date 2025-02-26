namespace BuildingBlocks.Models;
//this is the client request coming from "frontend"
public record InquiryRequestDto(
    List<InputParameter> InputParameterList,
    int DenominationId,
    string BillingAccount
);
