namespace Magic.Application.Dtos;

public record DenominationDto(
    string NameEN
    , string NameAR
    , decimal Value
    , decimal MaxValue
    , decimal MinValue
    , bool IsInquiryRequired
    , int SortOrder
    , int ServiceId
    , int PriceType
    , int ProviderId
    , bool IsActive
);
