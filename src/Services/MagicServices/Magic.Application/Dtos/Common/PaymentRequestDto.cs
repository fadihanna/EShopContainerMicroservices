namespace Magic.Application.Dtos.Common;
public record PaymentRequestDto
(
     double Amount,
     double Fees,
     int DenominationId,
     string BillingAccount,
     string RequestId,
     string ProviderReferenceNumber,
     int Quantity,
     int ProviderId,
    List<InputParameter> InputParameterList
);