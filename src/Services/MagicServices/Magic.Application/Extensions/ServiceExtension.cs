namespace Magic.Application.Extensions
{
    public static class ServiceExtension
    {
        public static IEnumerable<ServiceDto> ToServiceDtoList(this IEnumerable<Service> Services)
        {
            return Services.Select(Service => new ServiceDto(
Id: Service.Id,
                NameEN: Service.NameEN,
                NameAR: Service.NameAR,
                SortOrder: Service.SortOrder,
                IsActive: Service.IsActive,
                ServiceCategoryId : Service.ServiceCategoryId,
                IconName : Service.IconName
            ));
        }

        public static ServiceDto ToServiceDto(this Service Service)
        {
            return DtoFromService(Service);
        }
        private static ServiceDto DtoFromService(Service Service)
        {
            return new ServiceDto(
                Id: Service.Id,
                NameEN: Service.NameEN,
                NameAR: Service.NameAR,
                SortOrder: Service.SortOrder,
                IsActive : Service.IsActive,
                ServiceCategoryId: Service.ServiceCategoryId,
                IconName : Service.IconName
            );
        }
        public static Service DtoToService(this ServiceDto ServiceDto)
        {
            return ServiceFromDto(ServiceDto);
        }
        private static Service ServiceFromDto(ServiceDto dto)
        {
            return Service.Create(
                dto.NameEN,
                dto.NameAR,
                dto.IconName,
                dto.IsActive,
                dto.SortOrder,
                dto.ServiceCategoryId
            );
        }
    }
}
