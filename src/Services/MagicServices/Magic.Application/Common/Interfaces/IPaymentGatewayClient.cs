using PaymentGateway.Grpc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magic.Application.Common.Interfaces
{
    public interface IPaymentGatewayClient
    {
        Task<PaymentResponse> ProcessPaymentAsync(PaymentRequest request);
    }

}
