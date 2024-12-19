namespace Magic.Application.Extensions;

public static class DenominationExtensions
{
    public static IEnumerable<DenominationDto> ToDenominationDtoList(this IEnumerable<Denomination> denominations)
    {
        return denominations.Select(denomination => new DenominationDto(
            Id: denomination.Id,
            NameEN: denomination.NameEN,
            NameAR: denomination.NameAR,
            Value: denomination.Value,
            MaxValue: denomination.MaxValue,
            MinValue: denomination.MinValue,
            IsInquiryRequired: denomination.IsInquiryRequired,
            SortOrder: denomination.SortOrder,
            ServiceId: denomination.ServiceId,
            PriceType: denomination.PriceType,
            ProviderId: denomination.ProviderId,
            IsActive: denomination.IsActive
        ));
    }

    public static DenominationDto ToDenominationDto(this Denomination denomination)
    {
        return DtoFromDenomination(denomination);
    }
    private static DenominationDto DtoFromDenomination(Denomination denomination)
    {
        return new DenominationDto(
            Id: denomination.Id,
            NameEN: denomination.NameEN,
            NameAR: denomination.NameAR,
            Value: denomination.Value,
            MaxValue: denomination.MaxValue,
            MinValue: denomination.MinValue,
            IsInquiryRequired: denomination.IsInquiryRequired,
            SortOrder: denomination.SortOrder,
            ServiceId: denomination.ServiceId,
            PriceType: denomination.PriceType,
            ProviderId: denomination.ProviderId,
            IsActive: denomination.IsActive
        );
    }
    public static Denomination DtoToDenomination(this DenominationDto denominationDto)
    {
        return DenominationFromDto(denominationDto);
    }
    private static Denomination DenominationFromDto(DenominationDto dto)
    {
        return Denomination.Create(
            dto.NameEN,
            dto.NameAR,
            dto.Value,
            dto.MaxValue,
            dto.MinValue,
            dto.IsInquiryRequired,
            dto.SortOrder,
            dto.ServiceId,
            dto.PriceType,
            dto.ProviderId,
            dto.IsActive
        );
    }
}
