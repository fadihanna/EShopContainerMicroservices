namespace Magic.Application.Dtos
{
    public record ServiceCategoryDto
    (
      string NameEN
    , string NameAR
    , int SortOrder
    , bool IsActive,
      string IconName
    );
}
