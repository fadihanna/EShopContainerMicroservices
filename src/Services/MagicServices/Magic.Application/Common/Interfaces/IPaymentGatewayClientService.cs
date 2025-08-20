namespace Magic.Application.Common.Interfaces;
public interface IPaymentGatewayClientService
{
    Task<PaymentGatewayResponseDto> ProcessPaymentAsync(PaymentGatewayRequestDto request, CancellationToken cancellationToken);
    Task<PaymentGatewayResponseDto> VerifyPaymentAsync(PaymentGatewayRequestDto request, CancellationToken cancellationToken);
}
