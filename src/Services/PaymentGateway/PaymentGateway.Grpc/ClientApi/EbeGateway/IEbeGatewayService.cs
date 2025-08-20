using PaymentGateway.Grpc.Dto.Ebe;
namespace PaymentGateway.Grpc.ClientApi.EbeGateway
{
    public interface IEbeGatewayService
    {
        Task<PrepareCheckoutResponseDto> PrepareCheckoutAsync(PrepareCheckoutRequestDto request, string baseUrl, string token);
        Task<PaymentResponseDto> GetPaymentStatusAsync(string checkoutId, string entityId, string baseUrl, string token);
        Task<PaymentResponseDto> RefundPaymentAsync(RefundRequestDto request, string baseUrl, string token);
    }
}
