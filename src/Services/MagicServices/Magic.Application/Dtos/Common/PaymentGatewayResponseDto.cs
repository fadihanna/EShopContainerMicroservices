namespace Magic.Application.Dtos.Common;

public record PaymentGatewayResponseDto
(
    bool Success,
    string Message,
    string PaymentProviderTransactionId
);
