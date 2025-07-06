namespace Magic.Application.Dtos;

public record DenominationDto(
    int Id
    ,string NameEN
    , string NameAR
    , decimal MaxValue
    , decimal MinValue
    , bool IsInquiryRequired
    , int SortOrder
    , int ServiceId
    , int PriceType
    , int ProviderId
    , bool IsActive,
     int? DenominationGroupID,
     bool IsPartial,
     decimal Value ,
    List<DenominationInputParameterList> InputParamterList
);
public record DenominationInputParameterList(
    string Key,
    string Value,
    int? MinLength,
    int? MaxLength,
    string? NameEn,
    string? NameAr,
    string Code,
    int? Sort,
    bool? IsRequired ,
    string Placeholder ,
    string Type
    );

public record AmountDto
    (
    int Id,
    decimal Value
    );
