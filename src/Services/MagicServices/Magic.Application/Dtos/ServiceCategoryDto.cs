namespace Magic.Application.Dtos
{
    public record ServiceCategoryDto
    (
        int Id,
      string NameEN
    , string NameAR
    , int SortOrder
    , bool IsActive,
      string IconName
    );
}
