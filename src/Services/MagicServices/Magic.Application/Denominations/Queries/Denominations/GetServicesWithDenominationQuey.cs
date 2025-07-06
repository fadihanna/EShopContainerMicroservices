using Magic.Domain.Models;

namespace Magic.Application.Denominations.Queries.Denominations
{
    public record GetServicesWithDenominationQuery(int categoryId) : IQuery<GetServicesWithDenominationResponse>;
    public record GetServicesWithDenominationResponse(List<ServiceWithDenominationDto> DenominationListDto);

    public class GetServicesWithDenominationHandler
        : IQueryHandler<GetServicesWithDenominationQuery, GetServicesWithDenominationResponse>
    {
        private readonly IServiceSpecification _serviceSpecification;

        public GetServicesWithDenominationHandler(IServiceSpecification serviceSpecification)
        {
            _serviceSpecification = serviceSpecification;
        }

        public async Task<GetServicesWithDenominationResponse> Handle(GetServicesWithDenominationQuery query, CancellationToken cancellationToken)
        {
            var services = await _serviceSpecification.GetServiceDenominationGroupAsync(query.categoryId, cancellationToken);

            var result = services
                        .Select(service => new ServiceWithDenominationDto
                        {
                            Id = service.Id,
                            NameEN = service.NameEN,
                            NameAR = service.NameAR,
                            SortOrder = service.SortOrder,
                            ServiceCategoryId = service.ServiceCategoryId,
                            IsActive = service.IsActive,
                            IconName = service.IconName,
                            DenominationGroup = service.DenominationGroups
                                .Where(group => group.IsActive)
                                .OrderBy(group => group.SortOrder)
                                .Select(group => new DenominationGroupDto
                                {
                                    Id = group?.Id ?? 0,
                                    NameEN = group?.NameEN ?? string.Empty,
                                    NameAR = group?.NameAR ?? string.Empty,
                                    SortOrder = group?.SortOrder ?? 0,
                                    IsInquiryRequired = group?.IsInquiryRequired ?? false,
                                    IsActive = group?.IsActive ?? false,
                                    Denominations = group.Denominations?.Where(d => d.IsActive).Select(d => new DenominationItemDto(d.Id, d.Value.ToString())).ToList()
                                })
                        }).ToList();

            return new GetServicesWithDenominationResponse(result);
        }
    }
}
