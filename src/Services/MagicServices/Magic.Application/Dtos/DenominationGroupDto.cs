using System.Collections.Generic;

namespace Magic.Application.Dtos
{
    public record ServiceWithDenominationDto
    {
        public int Id { get; init; }
        public string NameEN { get; init; } = "";
        public string NameAR { get; init; } = "";
        public int SortOrder { get; init; }
        public int ServiceCategoryId { get; init; }
        public bool IsActive { get; init; }
        public string IconName { get; init; } = "";
        public List<DenominationGroupDto> DenominationGroup { get; init; } = new();
    }

    public record DenominationGroupDto
    {
        public int Id { get; init; }
        public string NameEN { get; init; } = "";
        public string NameAR { get; init; } = "";
        public int SortOrder { get; init; }
        public bool IsInquiryRequired { get; init; }
        public bool IsActive { get; init; }
        public int ServiceId { get; init; }    
        public List<DenominationItemDto> Denominations { get; init; } = new();
    }

    public record DenominationItemDto(int Id, string Value);
}
