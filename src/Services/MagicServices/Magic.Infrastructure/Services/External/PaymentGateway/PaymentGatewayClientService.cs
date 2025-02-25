using Magic.Application.Dtos.Common;
using PaymentGateway.Grpc.Protos;

namespace Magic.Infrastructure.Services.External.PaymentGateway
{
    public class PaymentGatewayClientService : IPaymentGatewayClientService
    {
        private readonly PaymentGatewayProtoService.PaymentGatewayProtoServiceClient _paymentGatewayProto;

        public PaymentGatewayClientService(PaymentGatewayProtoService.PaymentGatewayProtoServiceClient paymentGatewayProto)
        {
            _paymentGatewayProto = paymentGatewayProto;
        }

        public async Task<PaymentGatewayResponseDto> ProcessPaymentAsync(PaymentGatewayRequestDto request, CancellationToken cancellationToken)
        {
            PaymentRequest paymentRequestProto = new PaymentRequest
            {
                Provider = request.Provider,
                Amount = request.Amount,
                Currency = request.Currency
            };

            var response = await _paymentGatewayProto.ProcessPaymentAsync(paymentRequestProto);

            return new PaymentGatewayResponseDto(
                Success: response.Success,
                Message: response.Message,
                PaymentProviderTransactionId: response.PaymentprovidertransactionId
           );
        }
    }
}
