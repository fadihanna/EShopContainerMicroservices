using Provider.Application.Services.Damen.Models.Payment;
using Provider.Application.Services.Damen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Provider.Application.Configuration;

namespace Provider.Application.Services.Damen
{
    public interface IDamenApiClient
    {
        Task<DamenPaymentResponse> SendPaymentRequestAsync(DamenPaymentRequest request, DamenSettings damenSettings);
        Task<DamenInquiryResponse> SendInquiryRequestAsync(DamenInquiryRequest request, DamenSettings damenSettings);
    }
}
