using Magic.Domain.Models;

namespace Magic.Application.Denominations.Queries.Denominations
{
    public record GetServicesDenominationsQuery(int categoryId) : IQuery<GetServicesDenominationsResponse>;
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
            var services = await _serviceSpecification.GetServiceDenominationAsync(query.categoryId, cancellationToken);
            var dtoList = services
                        .Select(service => new ServiceDenominationDto
                        {
                            Id = service.Id,
                            NameEN = service.NameEN,
                            NameAR = service.NameAR,
                            SortOrder = service.SortOrder,
                            ServiceCategoryId = service.ServiceCategoryId,
                            IsActive = service.IsActive,
                            IconName = service.IconName,
                            Denominations = service.Denominations
                                .Where(d => d.IsActive && (d.DenominationGroupId == null))
                                .OrderBy(d => d.SortOrder)
                                .Select(d => new DenominationFullDto
                                {
                                    Id = d?.Id ?? 0,
                                    NameEN = d?.NameEN ?? string.Empty,
                                    NameAR = d?.NameAR ?? string.Empty,
                                    SortOrder = d?.SortOrder ?? 0,
                                    IsInquiryRequired = d?.IsInquiryRequired ?? false,
                                    IsActive = d?.IsActive ?? false,
                                    IsPartial = d?.IsPartial ?? false,
                                    IconName = d.IconName,
                                    Value = d.Value != null ? d.Value.ToString() : string.Empty,
                                    InputParameterList = d.DenominationInputParameters
                                        .OrderBy(p => p.Id)
                                        .Select(p => new InputParameterDto
                                        {
                                            Key = p.Key,
                                            Value = p.Value,
                                            Placeholder = p.Placeholder,
                                            Type = p.Type
                                        })
                                        .ToList()
                                })
                                .ToList()
                        }).ToList();
            return new GetServicesDenominationsResponse(dtoList);
        }
    }
}
