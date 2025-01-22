/*using Grpc.Core;
using MagicPaymentAPI.DTO;
using MagicPaymentAPI.DTO.Login;
using MagicPaymentAPI.DTO.Order.Request;
using MagicPaymentAPI.DTO.Order.Response;
using MagicPaymentAPI.DTO.Payment;
using MagicPaymentAPI.Service.PayMob;

public class PayMobService : IPayMobService
{
    public override Task<LoginResponse> SendLoginRequestAsync()
    {
        // Simulate a login response
        return Task.FromResult(new LoginResponse { Token = "sample-token", Message = "Login successful" });
    }

    public override Task<CreateOrderResponse> SendCreateOrderRequestAsync(CreateOrderRequest request, ServerCallContext context)
    {
        // Simulate an order creation response
        return Task.FromResult(new CreateOrderResponse { Status = "Success", OrderId = request.OrderId });
    }

    public override Task<PaymentResponse> SendPaymentRequestAsync(PaymentRequest request, ServerCallContext context)
    {
        // Simulate a payment response
        return Task.FromResult(new PaymentResponse { Status = "Completed", TransactionId = "txn12345" });
    }
}
*/