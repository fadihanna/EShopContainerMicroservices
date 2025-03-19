

namespace Magic.Application.Dtos
{
    public record ServiceDto
    (
        string NameEN,
        string NameAR,
        int SortOrder,
        bool IsActive,
        int ServiceCategoryId,
        string IconName
    );
}
