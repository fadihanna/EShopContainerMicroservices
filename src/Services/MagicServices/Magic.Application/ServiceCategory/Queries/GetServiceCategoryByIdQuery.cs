namespace Magic.Application.ServiceCategories.Queries.ServiceCategories
{
    public record GetServiceCategoryByIdQuery(int Id)
    : IQuery<GetServiceCategoryIdResponse>;
    public record GetServiceCategoryIdResponse(ServiceCategoryDto serviceCategoryDto);
    public class GetServiceCategoryByIdHandler
    : IQueryHandler<GetServiceCategoryByIdQuery, GetServiceCategoryIdResponse>
    {
        private readonly IServiceCategorySpecification _serviceCategorySpecification;
        public GetServiceCategoryByIdHandler(IServiceCategorySpecification serviceCategorySpecification)
        {
            _serviceCategorySpecification = serviceCategorySpecification;
        }
        public async Task<GetServiceCategoryIdResponse> Handle(GetServiceCategoryByIdQuery query, CancellationToken cancellationToken)
        {
            var serviceCategory = await _serviceCategorySpecification.GetByIdAsync(o => o.IsActive && o.Id.Equals(query.Id), cancellationToken);

            if (serviceCategory == null)
                throw new InquiryResponseException(DomainEnums.InternalErrorCode.EntityNotFound);

            return new GetServiceCategoryIdResponse(serviceCategory!.ToServiceCategoryDto());
        }
    }
}