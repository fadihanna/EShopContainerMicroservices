using Magic.Application.Denominations.Responses;
using Magic.Application.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Magic.Application.Denominations.Queries.Denominations
{
    public class GetServicesWithDenominationQuery : IQuery<GetServicesWithDenominationResponse>
    {
    }

    public record GetServicesWithDenominationResponse(List<ServiceWithDenominationDto> DenominationListDto);

    public class GetServicesWithDenominationHandler
        : IQueryHandler<GetServicesWithDenominationQuery, GetServicesWithDenominationResponse>
    {
        private readonly IServiceSpecification _serviceSpecification;

        public GetServicesWithDenominationHandler(IServiceSpecification serviceSpecification)
        {
            _serviceSpecification = serviceSpecification;
        }

        public async Task<GetServicesWithDenominationResponse> Handle(
            GetServicesWithDenominationQuery query,
            CancellationToken cancellationToken)
        {
            var services = await _serviceSpecification.GetAllWithDenominationsAsync(cancellationToken);

            var dtoList = services.Select(service => new ServiceWithDenominationDto
            {
                Id = service.Id,
                NameEN = service.NameEN,
                NameAR = service.NameAR,
                SortOrder = service.SortOrder,
                ServiceCategoryId = service.ServiceCategoryId,
                IsActive = service.IsActive,
                IconName = service.IconName,
                DenominationGroup = service.Denominations
         .GroupBy(d => d.DenominationGroup)
         .Select(group => new DenominationGroupDto
         {
             Id = group.Key?.Id ?? 0,  
             NameEN = group.Key?.NameEN ?? string.Empty,
             NameAR = group.Key?.NameAR ?? string.Empty,
             SortOrder = group.Key?.SortOrder ?? 0,
             IsInquiryRequired = group.Key?.IsInquiryRequired ?? false,
             IsActive = group.Key?.IsActive ?? false,
             Denominations = group.Select(d => new DenominationItemDto(d.Id, d.Value.ToString())).ToList()
         }).ToList()
            }).ToList();

            return new GetServicesWithDenominationResponse(dtoList);
        }
    }
}
