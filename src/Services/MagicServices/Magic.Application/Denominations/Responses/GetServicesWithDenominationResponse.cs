using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magic.Application.Denominations.Responses
{
    public class GetServicesWithDenominationResponse
    {
        public List<ServiceWithDenominationDto> Services { get; }

        public GetServicesWithDenominationResponse(List<ServiceWithDenominationDto> services)
        {
            Services = services;
        }
    }
}
