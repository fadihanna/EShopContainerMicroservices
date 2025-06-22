using Magic.Application.Denominations.Responses;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Magic.Application.Denominations.Queries.Denominations
{
    public class GetServicesDenominationsQuery : IQuery<GetServicesDenominationsResponse>
    {
    }

    public record GetServicesDenominationsResponse(List<ServiceDenominationDto> Services);

    public class GetServicesDenominationsHandler
        : IQueryHandler<GetServicesDenominationsQuery, GetServicesDenominationsResponse>
    {
        private readonly IServiceSpecification _serviceSpecification;

        public GetServicesDenominationsHandler(IServiceSpecification serviceSpecification)
        {
            _serviceSpecification = serviceSpecification;
        }

        public async Task<GetServicesDenominationsResponse> Handle(
            GetServicesDenominationsQuery query,
            CancellationToken cancellationToken)
        {
            var services = await _serviceSpecification.GetAllWithDenominationsAsync(cancellationToken);

            var dtoList = services.Select(service => new ServiceDenominationDto
            {
                Id = service.Id,
                NameEN = service.NameEN,
                NameAR = service.NameAR,
                SortOrder = service.SortOrder,
                ServiceCategoryId = service.ServiceCategoryId,
                IsActive = service.IsActive,
                IconName = service.IconName,
                Denominations = service.Denominations?
            .Select(d => new DenominationFullDto
            {
                Id = d?.Id ?? 0,
                NameEN = d?.NameEN ?? string.Empty,
                NameAR = d?.NameAR ?? string.Empty,
                SortOrder = d?.SortOrder ?? 0,
                IsInquiryRequired = d?.IsInquiryRequired ?? false,
                IsActive = d?.IsActive ?? false,
                IsPartial = d?.IsPartial ?? false,
                Value = d?.Value.ToString() ?? string.Empty,
                InputParameterList = d?.DenominationInputParameters?
                    .Select(p => new InputParameterDto
                    {
                        Key = p?.Key ?? string.Empty,
                        Value = p?.Value ?? string.Empty,
                        Placeholder = p?.Placeholder ?? string.Empty,
                        Type = p?.Type ?? string.Empty
                    })
                    .ToList() ?? new List<InputParameterDto>()
            })
            .ToList() ?? new List<DenominationFullDto>()
            }).ToList();
            return new GetServicesDenominationsResponse(dtoList);
        }
    }
}
