using PaymentGateway.DTO.Login;
using PaymentGateway.DTO.Order.Request;
using PaymentGateway.DTO.Order.Response;
using PaymentGateway.DTO.Payment;

namespace PaymentGateway.Grpc.Services.PayMob
{
    public interface IPayMobService
    {
        public Task<PaymobLoginResponse> Login();
        public Task<PaymobCreateOrderResponse> CreateOrder(PaymobCreateOrderRequest createOrderRequest);
        public Task<PaymobPaymentResponse> Payment(PaymobPaymentRequest paymentRequest);
    }   
}
