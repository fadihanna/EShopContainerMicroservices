namespace Magic.Application.ServiceCategories.Queries.ServiceCategories
{
    public class GetServiceCategoriesQuery : IQuery<GetServiceCategoriesResponse>;
    public record GetServiceCategoriesResponse(List<ServiceCategoryDto> serviceCategoryListDto);
    public class GetServiceCategoriesHandler
    : IQueryHandler<GetServiceCategoriesQuery, GetServiceCategoriesResponse>
    {
        private readonly IServiceCategorySpecification _serviceCategorySpecification;
        public GetServiceCategoriesHandler(IServiceCategorySpecification serviceCategorySpecification)
        {
            _serviceCategorySpecification = serviceCategorySpecification;
        }
        public async Task<GetServiceCategoriesResponse> Handle(GetServiceCategoriesQuery query, CancellationToken cancellationToken)
        {
            var serviceCategoryList = await _serviceCategorySpecification.GetAllAsync(cancellationToken);
            return new GetServiceCategoriesResponse(serviceCategoryList!.ToServiceCategoryDtoList().ToList());
        }
    }
}