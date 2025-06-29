using BeePaymentDTO.Consumer;
using BeePaymentDTO.Consumer.All;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magic.Application.Common.Interfaces
{
    // consumer app testing
    public interface IOrangeBLL
    {
        Task<List<ServiceCategoryDto>> GetServiceCategory();
        Task<List<ServiceDto>> GetServiceByCategory(int serviceCategory);
        Task<List<ServiceWithDenominationDto>> GetServiceWithDenominationByCategory(int serviceCategory);
        Task<List<ServiceDenominationDto>> GetServiceDenominationByCategory(int serviceCategory);
       // Task<List<ServiceDenominationDto>> SendInquiryRequest(BeePaymentDTO.Consumer.InquiryRequest inquiryRequest);
    }
}
