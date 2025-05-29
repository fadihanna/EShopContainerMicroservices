namespace Magic.Application.Extensions
{
    public static class DenominationGroupExtensions
    {
        public static IEnumerable<DenominationGroupDto> ToDenominationGroupDtoList(this IEnumerable<DenominationGroup> groups)
        {
            return groups.Select(ToDenominationGroupDto);
        }

        public static DenominationGroupDto ToDenominationGroupDto(this DenominationGroup group)
        {
            return new DenominationGroupDto(
                Id: group.Id,
                NameEN: group.NameEN,
                NameAR: group.NameAR,
                IsActive: group.IsActive,
                SortOrder: group.SortOrder,
                IsInquiryRequired: group.IsInquiryRequired
            );
        }

        public static DenominationGroup DtoToDenominationGroup(this DenominationGroupDto dto)
        {
            var group = new DenominationGroup
            {
                Id = dto.Id,
                NameEN = dto.NameEN,
                NameAR = dto.NameAR,
                IsActive = dto.IsActive,
                SortOrder = dto.SortOrder,
                IsInquiryRequired = dto.IsInquiryRequired
            };

            return group;
        }
    }
}
