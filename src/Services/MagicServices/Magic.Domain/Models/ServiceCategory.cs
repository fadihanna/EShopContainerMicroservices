namespace Magic.Domain.Models
{
    public class ServiceCategory : Entity<int>
    {
        // Properties
        public string NameEN { get; private set; }
        public string NameAR { get; private set; }
        public int SortOrder { get; private set; }
        public bool IsActive { get; private set; }

        // Factory Method for Creation
        public static ServiceCategory Create(string nameEn, string nameAr, bool isActive, int sortOrder = 0)
        {
            if (string.IsNullOrWhiteSpace(nameEn)) throw new ArgumentException("English name cannot be empty.", nameof(nameEn));
            if (string.IsNullOrWhiteSpace(nameAr)) throw new ArgumentException("Arabic name cannot be empty.", nameof(nameAr));

            return new ServiceCategory
            {
                NameEN = nameEn,
                NameAR = nameAr,
                IsActive = isActive,
                SortOrder = sortOrder
            };
        }

        // Method for Updating
        public void Update(string nameEn, string nameAr, bool isActive, int sortOrder)
        {
            if (string.IsNullOrWhiteSpace(nameEn)) throw new ArgumentException("English name cannot be empty.", nameof(nameEn));
            if (string.IsNullOrWhiteSpace(nameAr)) throw new ArgumentException("Arabic name cannot be empty.", nameof(nameAr));

            NameEN = nameEn;
            NameAR = nameAr;
            IsActive = isActive;
            SortOrder = sortOrder;
        }
    }
}