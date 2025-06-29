using BeePaymentDTO.Consumer;
using BeePaymentDTO.Consumer.All;
using Magic.Application.Common.Interfaces;
using Magic.Application.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace MagicServices.API.Controllers
{
    public class ConsumerController : ApiControllerBase
    {
        readonly IHostEnvironment environment;
        private readonly IOrangeBLL _orangeBLL;
        public ConsumerController(IHostEnvironment environment, IOrangeBLL orangeBLL) : base(environment) {

            _orangeBLL = orangeBLL;
        }
        [HttpGet("GetCategories")]
        public async Task<List<ServiceCategoryDto>> GetCategories()
        {
            try
            {
                var categories = await _orangeBLL.GetServiceCategory();
                return categories;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /*[HttpGet("GetServiceByCategory")]
        public async Task<List<ServiceDto>> GetServiceByCategory(int serviceCategory)
        {
            var services = await _orangeBLL.GetServiceByCategory(serviceCategory);
            return services;
        }
        [HttpGet("GetServiceWithDenominationByCategory")]
        public async Task<List<ServiceWithDenominationDto>> GetServiceWithDenominationByCategory(int serviceCategory)
        {
            var services = await _orangeBLL.GetServiceWithDenominationByCategory(serviceCategory);
            return services;
        }
        [HttpGet("GetServiceDenominationByCategory")]
        public async Task<List<ServiceDenominationDto>> GetServiceDenominationByCategory(int serviceCategory)
        {
            var services = await _orangeBLL.GetServiceDenominationByCategory(serviceCategory);
            return services;
        }*/


/*        [HttpPost("inquiry")]
        public async Task<List<ServiceDenominationDto>> Inquiry(InquiryRequest inquiryRequest)
        {
            return _orangeBLL.SendInquiryRequest();
        }*/
    }
}
