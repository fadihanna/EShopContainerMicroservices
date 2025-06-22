

namespace Magic.Application.Dtos
{
    public record ServiceDto
    (
        int Id,
        string NameEN,
        string NameAR,
        int SortOrder,
        bool IsActive,
        int ServiceCategoryId,
        string IconName,
        string NavigationScreen 
    );
}
