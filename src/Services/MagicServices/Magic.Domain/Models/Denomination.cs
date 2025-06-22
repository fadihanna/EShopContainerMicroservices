namespace Magic.Domain.Models
{
    public class Denomination : Entity<int>
    {
        // Properties

        public string NameEN { get; private set; } = string.Empty;
        public string NameAR { get; private set; } = string.Empty;
      //  public decimal Value { get; private set; }
        public decimal MaxValue { get; private set; }
        public decimal MinValue { get; private set; }
        public bool IsInquiryRequired { get; private set; }
        public int SortOrder { get; private set; }
        public int ServiceId { get; private set; }  
        public int PriceType { get; private set; }
        public int ProviderId { get; private set; }
        public bool IsActive { get; private set; }
        public bool IsPartial { get; private set; }
        public decimal Value { get; private set; }  
        public Service Service { get; private set; } = default!;
        public Provider Provider { get; private set; } = default!;
        public ICollection<DenominationFee> DenominationFees { get; set; }
        public ICollection<DenominationInputParameter> DenominationInputParameters { get; set; }
        public ICollection<DenominationProviderCode> DenominationProviderCodes { get; set; }
        public int? DenominationGroupId { get; private set; }  // Nullable FK
        public DenominationGroup? DenominationGroup { get; private set; }

        // Factory Method for Creation
        public static Denomination Create(
            string nameEn,
            string nameAr,
            decimal value,
            decimal minValue,
            decimal maxValue,
            bool isInquiryRequired,
            int sortOrder,
            int serviceId,
            int priceType,
            int providerId,
            bool isActive,
            int? denominationGroupId) // Nullable

        {
            if (string.IsNullOrWhiteSpace(nameEn))
                throw new ArgumentException("English name cannot be empty.", nameof(nameEn));

            if (string.IsNullOrWhiteSpace(nameAr))
                throw new ArgumentException("Arabic name cannot be empty.", nameof(nameAr));

            if (minValue > maxValue)
                throw new ArgumentException("MinValue cannot be greater than MaxValue.");

            return new Denomination
            {
                NameEN = nameEn,
                NameAR = nameAr,
                //Value = value,
                MaxValue = maxValue,
                MinValue = minValue,
                IsInquiryRequired = isInquiryRequired,
                SortOrder = sortOrder,
                ServiceId = serviceId, // Foreign Key Value
                PriceType = priceType,
                ProviderId = providerId,
                IsActive = isActive,
                DenominationGroupId = denominationGroupId  

            };
        }

        // Method for Updating
        public void Update(
            string nameEn,
            string nameAr,
           // decimal value,
            decimal maxValue,
            decimal minValue,
            bool isInquiryRequired,
            int sortOrder,
            int serviceId,
            int priceType,
            int providerId,
            bool isActive)
        {
            if (string.IsNullOrWhiteSpace(nameEn))
                throw new ArgumentException("English name cannot be empty.", nameof(nameEn));

            if (string.IsNullOrWhiteSpace(nameAr))
                throw new ArgumentException("Arabic name cannot be empty.", nameof(nameAr));

            if (minValue > maxValue)
                throw new ArgumentException("MinValue cannot be greater than MaxValue.");

            NameEN = nameEn;
            NameAR = nameAr;
           // Value = value;
            MaxValue = maxValue;
            MinValue = minValue;
            IsInquiryRequired = isInquiryRequired;
            SortOrder = sortOrder;
            ServiceId = serviceId; // Update FK
            PriceType = priceType;
            ProviderId = providerId;
            IsActive = isActive;
        }
     
        public Denomination WithInputParameters(List<DenominationInputParameter> inputParameters)
        {
            DenominationInputParameters = inputParameters;
            return this;
        }

       

        public void UpdateInputParameters(List<DenominationInputParameter> newInputParameters)
        {
            DenominationInputParameters.Clear();
            foreach (var param in newInputParameters)
            {
                DenominationInputParameters.Add(param);
            }
        }
    }
}
