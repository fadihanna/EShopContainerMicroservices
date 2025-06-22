namespace Magic.Domain.Models
{
    public class DenominationInputParameter : Entity<int>
    {
        public string Code { get; set; }
        public string NameEN { get; set; }
        public string NameAR { get; set; }
        public int? Sort { get; set; }
        public bool? IsRequired { get; set; }
        public int? MaxLength { get; set; }
        public int? MinLength { get; set; }
        public byte? ParameterType { get; set; }
        public bool? IsVisible { get; set; }
        public bool? IsConfirmRequired { get; set; }
        public int DenominationId { get; set; }
        public string? Key { get; set; }
        public string? Value { get; set; }
        public string? Placeholder { get; set; }
        public string? Type { get; set; }

        public Denomination Denomination { get; set; }


        public static DenominationInputParameter Create(
        string key,
        string value,
        int? minLength,
        int? maxLength,
        string nameEn,
        string nameAr,
        string code,
        int? sort,
        bool? isRequired,
        int denominationId
    )
        {
            return new DenominationInputParameter
            {
                Key = key,
                Value = value,
                MinLength = minLength,
                MaxLength = maxLength,
                NameEN = nameEn,
                NameAR = nameAr,
                Code = code,
                Sort = sort,
                IsRequired = isRequired,
                DenominationId = denominationId
            };
        }

    }
}

