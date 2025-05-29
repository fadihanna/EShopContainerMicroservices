namespace Magic.Application.Extensions;

public static class DenominationExtensions
{
    public static IEnumerable<DenominationDto> ToDenominationDtoList(this IEnumerable<Denomination> denominations)
    {
        return denominations.Select(ToDenominationDto);
    }

    public static DenominationDto ToDenominationDto(this Denomination denomination)
    {
        return new DenominationDto(
            NameEN: denomination.NameEN,
            NameAR: denomination.NameAR,
           // Value: denomination.Value,
            MaxValue: denomination.MaxValue,
            MinValue: denomination.MinValue,
            IsInquiryRequired: denomination.IsInquiryRequired,
            SortOrder: denomination.SortOrder,
            ServiceId: denomination.ServiceId,
            PriceType: denomination.PriceType,
            ProviderId: denomination.ProviderId,
            IsActive: denomination.IsActive,
            DenominationGroupID :denomination.DenominationGroupId,
            InputParamterList: denomination.DenominationInputParameters?.Select(ip => new DenominationInputParameterList(
                Key: ip.Key,
                Value: ip.Value,
                MinLength: ip.MinLength,
                MaxLength: ip.MaxLength,
                NameEn: ip.NameEN,
                NameAr: ip.NameAR,
                Code: ip.Code,
                Sort: ip.Sort,
                IsRequired: ip.IsRequired
            )).ToList() ?? new List<DenominationInputParameterList>() 
        );
    }
    public static Denomination DtoToDenomination(this DenominationDto dto)
    {
        var denomination = Denomination.Create(
            dto.NameEN,
            dto.NameAR,
           // dto.Value,
            dto.MinValue,
            dto.MaxValue,
            dto.IsInquiryRequired,
            dto.SortOrder,
            dto.ServiceId,
            dto.PriceType,
            dto.ProviderId,
            dto.IsActive,
            dto.DenominationGroupID
        );


        denomination.WithInputParameters(dto.InputParamterList?.Select(ip =>
      DenominationInputParameter.Create(
          key: ip.Key,
          value: ip.Value,
          minLength: ip.MinLength,
          maxLength: ip.MaxLength,
          nameEn: ip.NameEn,
          nameAr: ip.NameAr,
          code: ip.Code,
          sort: ip.Sort,
          isRequired: ip.IsRequired,
          denominationId: denomination.Id 
      )).ToList() ?? new List<DenominationInputParameter>());

        return denomination;
    }
    public static List<DenominationInputParameter> ToDenominationInputParameters(
        this List<DenominationInputParameterList> inputParameterList)
    {
        return inputParameterList.Select(param => new DenominationInputParameter
        {
            Key = param.Key,
            Value = param.Value,
            MinLength = param.MinLength,
            MaxLength = param.MaxLength,
            NameEN = param.NameEn,
            NameAR = param.NameAr,
            Code = param.Code,
            Sort = param.Sort,
            IsRequired = param.IsRequired
        }).ToList();
    }

}
