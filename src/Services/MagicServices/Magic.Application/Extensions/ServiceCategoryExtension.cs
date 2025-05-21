namespace Magic.Application.Extensions
{
    public static class ServiceCategoryExtension
    {
        public static ServiceCategoryDto ToServiceCategoryDto(this ServiceCategory
            serviceCategory)
        {
            return DtoFromServiceCategory(serviceCategory);
        }
        private static ServiceCategoryDto DtoFromServiceCategory(this ServiceCategory serviceCategory)
        {
            return new ServiceCategoryDto(
                Id:serviceCategory.Id,
                NameAR: serviceCategory.NameAR,
                NameEN: serviceCategory.NameEN,
                SortOrder: serviceCategory.SortOrder,
                IsActive: serviceCategory.IsActive,
                IconName:serviceCategory.IconName
            );
        }
        public static ServiceCategory DtoToServiceCategory(ServiceCategoryDto serviceCategoryDto)
        {
            return ServiceCategoryFromDto(serviceCategoryDto);
        }
        private static ServiceCategory ServiceCategoryFromDto(ServiceCategoryDto dto)
        {
            return ServiceCategory.Create(
                dto.NameAR,
                dto.NameEN,
                dto.IconName,
                dto.IsActive,
                dto.SortOrder
            );
        }
        public static List<ServiceCategoryDto> ToServiceCategoryDtoList(this IEnumerable<ServiceCategory> serviceCategories)
        {
            return serviceCategories?.Select(x => x.ToServiceCategoryDto()).ToList() ?? new List<ServiceCategoryDto>();
        }
        public static ServiceCategory CreateServiceCategory(ServiceCategoryDto serviceCategoryDto)
        {
            var newServiceCategory = ServiceCategory.Create(
                nameEn: serviceCategoryDto.NameEN,
                nameAr: serviceCategoryDto.NameAR,
                iconName : serviceCategoryDto.IconName,
                isActive: serviceCategoryDto.IsActive,
                sortOrder: serviceCategoryDto.SortOrder

            );
            return newServiceCategory;
        }
    }
}
