using Provider.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Provider.Application.Repositories.Damen
{
    public interface IDamenRepository
    {
          Task<double> GetServiceChargeAsync(int serviceId, double amount);
        Task<List<DamenServiceRequestDto>> GetServiceRequestsWithInputFieldsAsync(int serviceId);
    }
}
