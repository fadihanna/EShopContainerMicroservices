namespace Magic.Application.Dtos;

public record DenominationDto(
    string NameEN
    , string NameAR
   // , decimal Value
    , decimal MaxValue
    , decimal MinValue
    , bool IsInquiryRequired
    , int SortOrder
    , int ServiceId
    , int PriceType
    , int ProviderId
    , bool IsActive,
    List<DenominationInputParameterList> InputParamterList
);
public record DenominationInputParameterList(
    string Key,
    string Value,
   // string Label,
    int? MinLength,
    int? MaxLength,
    string? NameEn,
    string? NameAr,
    string Code,
    int? Sort,
    bool? IsRequired 
    );

public record AmountDto
    (
    int Id,
    decimal Value
    );
