using Grpc.Net.Client;
using MagicPaymentAPI.DTO.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magic.Infrastructure.Services.Microservices
{
    public class PaymentIntegrationService
    {
        // private readonly PaymentService.PaymentServiceClient _client;

        public PaymentIntegrationService()
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:5001");
           // var client = channel
         //  var client = 
        }


    }
}
