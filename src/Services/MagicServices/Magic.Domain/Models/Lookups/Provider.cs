namespace Magic.Domain.Models.Lookups
{
    public class Provider : LookUpBase<int>
    {
        public ICollection<DenominationProviderCode> DenominationProviderCodes { get; set; }
        // Factory Method for Creation
        public static Provider Create(string nameEn, string nameAr, bool isActive, int sortOrder = 0)
        {
            if (string.IsNullOrWhiteSpace(nameEn)) throw new ArgumentException("English name cannot be empty.", nameof(nameEn));
            if (string.IsNullOrWhiteSpace(nameAr)) throw new ArgumentException("Arabic name cannot be empty.", nameof(nameAr));

            return new Provider
            {
                NameEN = nameEn,
                NameAR = nameAr,
                IsActive = isActive
            };
        }
        public void Update(string nameEn, string nameAr, bool isActive)
        {
            if (string.IsNullOrWhiteSpace(nameEn)) throw new ArgumentException("English name cannot be empty.", nameof(nameEn));
            if (string.IsNullOrWhiteSpace(nameAr)) throw new ArgumentException("Arabic name cannot be empty.", nameof(nameAr));

            NameEN = nameEn;
            NameAR = nameAr;
            IsActive = isActive;
        }
    }
}
