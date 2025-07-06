namespace Magic.Application.Dtos.Common;
public record PaymentRequestDto(
    decimal Amount,
    decimal Fees,
    string Brn,
    int DenominationId,
    string ProviderCode,
    string BillingAccount,
    int Quantity,
    string InquiryReferenceNumber,
    List<InputParameter> InputParameterList,
    int ProviderId,
    string RequestId,
    string UserId,
    int CenterId
);
