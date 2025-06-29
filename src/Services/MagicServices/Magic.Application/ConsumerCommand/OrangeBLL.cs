using BeePaymentDTO.Consumer;
using BeePaymentDTO.Consumer.All;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magic.Application.ConsumerCommand
{
    public class OrangeBLL : IOrangeBLL
    {
        public async Task<List<ServiceCategoryDto>> GetServiceCategory()
        {
            string fileName = @"D:\Work\My Work\BK\prod bk server.2\switch\BeePaymentAPI\Mockups\Consumer\ServiceCategory.json";
            string result = File.ReadAllText(fileName);

            List<ServiceCategoryDto> dataModel = JsonConvert.DeserializeObject<List<ServiceCategoryDto>>(result);
            return dataModel;
        }
        public async Task<List<ServiceDto>> GetServiceByCategory(int serviceCategory)
        {
            string fileName = @"D:\Work\My Work\BK\prod bk server.2\switch\BeePaymentAPI\Mockups\Consumer\Service.json";
            string result = File.ReadAllText(fileName);

            List<ServiceDto> dataModel = JsonConvert.DeserializeObject<List<ServiceDto>>(result);
            dataModel = dataModel.Where(x => x.ServiceCategoryId == serviceCategory)?.ToList();
            return dataModel;
        }
       public async Task<List<ServiceWithDenominationDto>> GetServiceWithDenominationByCategory(int serviceCategory)
        {
            string fileName = @"D:\Work\My Work\BK\prod bk server.2\switch\BeePaymentAPI\Mockups\Consumer\ServiceWithDenomination.json";
            string result = File.ReadAllText(fileName);

            List<ServiceWithDenominationDto> dataModel = JsonConvert.DeserializeObject<List<ServiceWithDenominationDto>>(result);
            dataModel = dataModel.Where(x => x.ServiceCategoryId == serviceCategory)?.ToList();
            return dataModel;
        }
        public async Task<List<ServiceDenominationDto>> GetServiceDenominationByCategory(int serviceCategory)
        {
            string fileName = @"D:\Work\My Work\BK\prod bk server.2\switch\BeePaymentAPI\Mockups\Consumer\ServiceDenominations.json";
            string result = File.ReadAllText(fileName);

            List<ServiceDenominationDto> dataModel = JsonConvert.DeserializeObject<List<ServiceDenominationDto>>(result);
            dataModel = dataModel.Where(x => x.ServiceCategoryId == serviceCategory)?.ToList();
            return dataModel;
        }
    }
}
