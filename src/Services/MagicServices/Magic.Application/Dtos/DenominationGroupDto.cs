namespace Magic.Application.Dtos
{
    public record ServiceWithDenominationDto
    {
        public int Id { get; set; }
        public string NameEN { get; set; }
        public string NameAR { get; set; }
        public int SortOrder { get; set; }
        public int ServiceCategoryId { get; set; }
        public bool IsActive { get; set; }
        public string IconName { get; set; }
        public IEnumerable<DenominationGroupDto> DenominationGroup { get; set; }
    }

    public record DenominationGroupDto
    {
        public int Id { get; set; }
        public string NameEN { get; set; }
        public string NameAR { get; set; }
        public int SortOrder { get; set; }
        public bool IsInquiryRequired { get; set; }
        public bool IsActive { get; set; }
        public bool IsPartial { get; set; }
        public int ServiceId { get; init; }
        public IEnumerable<DenominationItemDto> Denominations { get; set; }
    }
    public record DenominationInputParameterDto(string? Key, string? Value, string? Placeholder, string? Type);
    public record DenominationItemDto(int Id, string Value);
}
