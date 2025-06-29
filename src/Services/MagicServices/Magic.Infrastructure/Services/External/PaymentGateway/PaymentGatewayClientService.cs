using Grpc.Net.Client;
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
            PaymentResponse response = null;
            try
            {
                var channel = GrpcChannel.ForAddress("https://localhost:44373"); // <- Your gRPC server address
                var _paymentGatewayProto = new PaymentGatewayProtoService.PaymentGatewayProtoServiceClient(channel);

                PaymentRequest paymentRequestProto = new PaymentRequest
                {
                    Provider = request.Provider,
                    Amount = request.Amount,
                    Currency = request.Currency
                };

                 response = await _paymentGatewayProto.ProcessPaymentAsync(paymentRequestProto);
            }
            catch (Exception ex)
            {
                return new PaymentGatewayResponseDto(
                    Success: response.Success,
                    Message: response.Message,
                    PaymentProviderTransactionId: response.PaymentprovidertransactionId
               );
            }
            return null;
        }
    }
}
