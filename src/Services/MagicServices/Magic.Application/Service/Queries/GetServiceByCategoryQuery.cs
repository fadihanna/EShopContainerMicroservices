namespace Magic.Application.Queries
{
    public record GetServiceByCategoryQuery(int CategoryId)
        : IQuery<GetServiceByCategoryResponse>;

    public record GetServiceByCategoryResponse(List<ServiceDto> ServiceListDto);

    public class GetServiceByCategoryHandler
        : IQueryHandler<GetServiceByCategoryQuery, GetServiceByCategoryResponse>
    {
        private readonly IServiceSpecification _serviceSpecification;

        public GetServiceByCategoryHandler(IServiceSpecification serviceSpecification)
        {
            _serviceSpecification = serviceSpecification;
        }

        public async Task<GetServiceByCategoryResponse> Handle(GetServiceByCategoryQuery query, CancellationToken cancellationToken)
        {
            var allServices = await _serviceSpecification.GetAllAsync(cancellationToken);

            var filteredServices = allServices
                .Where(s => s.IsActive && s.ServiceCategoryId == query.CategoryId)
                .ToList();

            return new GetServiceByCategoryResponse(filteredServices.ToServiceDtoList().ToList());
        }
    }
}
