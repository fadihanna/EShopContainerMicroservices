using PaymentGateway.Dto.Order.Request;
using PaymentGateway.Dto.Payment;
using PaymentGateway.Dto;
using PaymentGateway.Grpc.Protos;

namespace PaymentGateway.Grpc.ClientApi.Paymob
{
    public class PaymobPaymentProvider : IPaymentProvider
    {
        private readonly IPayMobService _payMobService;
        public PaymobPaymentProvider(IPayMobService payMobService)
        {
            _payMobService = payMobService;
        }
        public async Task<PaymentResult> ProcessPayment(PaymentRequest request)
        {
            try
            {
                // authenticate
                var loginResponse = await _payMobService.Login();
                if (loginResponse == null || string.IsNullOrEmpty(loginResponse.token))
                {
                    return new PaymentResult
                    {
                        Success = false,
                        TransactionId = null,
                        Message = "Failed to authenticate with Paymob."
                    };
                }

                // create order
                var createOrderRequest = new PaymobCreateOrderRequest()
                {
                    auth_token = loginResponse.token,
                    delivery_needed = "false",
                    amount_cents = Convert.ToString(request.Amount * 100),
                    currency = "EGP",
                    merchant_order_id = 16,
                    items = new List<Item>() {
                     new Item() {
                     name = "test",
                     amount_cents = Convert.ToString(request.Amount * 100),
                     description = "Test",
                     quantity = "1"
                 }
                    },
                    shipping_data = new ShippingData()
                    {
                        first_name = "George",
                        last_name = "Akladyos",
                        email = "georgemaged@hotmail.com",
                        phone_number = "01229411164",
                        apartment = "NA",
                        floor = "NA",
                        street = "NA",
                        building = "NA",
                        city = "NA",
                        state = "NA",
                        country = "NA",
                        postal_code = "01898",
                        extra_description = "TEST"
                    },
                    shipping_details = new ShippingDetails()
                    {
                        notes = "test",
                        number_of_packages = 1,
                        weight = 1,
                        weight_unit = "Kilogram",
                        length = 1,
                        width = 1,
                        height = 1,
                        contents = "product of some sorts"
                    }
                };
                var createOrderResponse = await _payMobService.CreateOrder(createOrderRequest);

                if (createOrderResponse == null || createOrderResponse.merchant_order_id == "0")
                {
                    return new PaymentResult
                    {
                        Success = false,
                        TransactionId = null,
                        Message = "Failed to create order with Paymob."
                    };
                }

                // payment

                var paymentRequest = new PaymobPaymentRequest
                {
                    auth_token = loginResponse.token,
                    amount_cents = Convert.ToString((request.Amount * 100)),
                    currency = request.Currency,
                    order_id = createOrderResponse.id.ToString(),
                    integration_id = 3418638,
                    billing_data = new BillingData
                    {
                        first_name = "George",
                        last_name = "Akladyos",
                        email = "georgemaged@hotmail.com",
                        phone_number = "01229411164",
                        apartment = "NA",
                        floor = "NA",
                        street = "NA",
                        building = "NA",
                        city = "NA",
                        state = "NA",
                        country = "NA",
                        postal_code = "01898",
                        shipping_method = "TEST"
                    },
                    lock_order_when_paid = "false",
                    expiration = 3600
                };
                var paymentResponse = await _payMobService.Payment(paymentRequest);

                if (paymentResponse != null && !string.IsNullOrEmpty(paymentResponse.token))
                {
                    PaymentResult paymentResult = new PaymentResult()
                    {
                        Message = "Payment processed successfully with paymob",
                        Success = true,
                        TransactionId = paymentResponse.token.ToString()
                    };
                    return paymentResult;
                }
                else
                {
                    PaymentResult paymentResult = new PaymentResult()
                    {
                        Message = "Failed payment with paymob",
                        Success = false,
                        TransactionId = "-1"
                    };
                    return paymentResult;
                }
            }
            catch (Exception ex)
            {
                return new PaymentResult
                {
                    Success = false,
                    TransactionId = null,
                    Message = $"An error occurred: {ex.Message}"
                };
            }
        }
    }
}
