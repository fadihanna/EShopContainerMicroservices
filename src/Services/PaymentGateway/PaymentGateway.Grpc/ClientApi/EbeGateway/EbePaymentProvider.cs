using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PaymentGateway.Dto;
using PaymentGateway.Grpc.ClientApi;
using PaymentGateway.Grpc.ClientApi.EbeGateway;
using PaymentGateway.Grpc.Dto.Ebe;
using PaymentGateway.Grpc.Protos;

    public class EbePaymentProvider : IPaymentProvider
    {
        private readonly IEbeGatewayService _ebeGatewayService;
        public EbePaymentProvider(IEbeGatewayService ebeGatewayService) 
        { 
            _ebeGatewayService = ebeGatewayService;
        }
        public async Task<PaymentGateway.Dto.PaymentResult> ProcessPayment(PaymentRequest request)
        {
            PrepareCheckoutRequestDto prepareCheckoutRequestDto = new PrepareCheckoutRequestDto()
            { 
                Amount = Convert.ToDecimal(request.Amount),
                EntityId = "EB000001",
                Currency = "EGP",
                PaymentType = "DB",
                EntityType = "PARTICIPATOR"
            };
            string checkOutUrl = "https://ebe.sandbox.efaka.net/gateway/v3/checkouts";
            string paymentUrlTemplate = "https://ebe.sandbox.efaka.net/gateway/v3/checkouts/{0}/payment?entityId=EB000001";
            string token = "123123123123123";
            string checkoutId = string.Empty;
            try
            {
                var checkoutResponse =  await _ebeGatewayService.PrepareCheckoutAsync(prepareCheckoutRequestDto, checkOutUrl, token);

                checkoutId = checkoutResponse.Id;
                if (string.IsNullOrEmpty(checkoutId))
                {
                    return new PaymentGateway.Dto.PaymentResult { Success = false, Message = "Checkout ID not returned from EBE" };
                }
            }
            catch (Exception ex)
            {
                return new PaymentGateway.Dto.PaymentResult
                {
                    Success = false,
                    Message = $"EBE Payment Error: {ex.Message}"
                };
            }
            return new PaymentGateway.Dto.PaymentResult() { Success = true,Message = "Checkout Fetched",TransactionId = checkoutId};
        }

        public async Task<PaymentGateway.Dto.PaymentResult> VerifyPayment(string checkoutId)
        {
            string entityId = "EB000001";
            string token = "123123123123123";
            string baseurl = "https://ebe.sandbox.efaka.net/gateway/v3/";
            var response = await _ebeGatewayService.GetPaymentStatusAsync(checkoutId,entityId,baseurl,token);
            return new PaymentGateway.Dto.PaymentResult() { Success = true, Message = JsonConvert.SerializeObject(response), TransactionId = checkoutId };
        }
    }
