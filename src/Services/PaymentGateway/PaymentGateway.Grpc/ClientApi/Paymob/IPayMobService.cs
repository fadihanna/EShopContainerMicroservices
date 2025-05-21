using PaymentGateway.Dto.Login;
using PaymentGateway.Dto.Order.Request;
using PaymentGateway.Dto.Order.Response;
using PaymentGateway.Dto.Payment;

namespace PaymentGateway.Grpc.ClientApi.Paymob
{
    public interface IPayMobService
    {
        public Task<PaymobLoginResponse> Login();
        public Task<PaymobCreateOrderResponse> CreateOrder(PaymobCreateOrderRequest createOrderRequest);
        public Task<PaymobPaymentResponse> Payment(PaymobPaymentRequest paymentRequest);
    }   
}
