using RabbitMQ.Client;

namespace Magic.Application.Dtos.Common;
public record PaymentRequestDto(
    decimal amount,
    decimal fees,
    string brn,
    int denominationId,
    string Btc,
    string billingAccount,
    int quantity,
    string InquiryReferenceNumber,
    List<InputParameter> InputParameterList,
    int ProviderId,
    string RequestId,
    string UserId,
    int CenterId
);
