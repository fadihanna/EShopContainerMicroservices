namespace Magic.Application.Dtos.Common
{
    public record PaymentGatewayRequestDto(
    string Provider,
    double Amount,
    string Currency);
}
