namespace Magic.Domain.Models
{
    public class ServiceCategory : Entity<int>
    {
        // Properties
        public string NameEN { get;  set; }
        public string NameAR { get;  set; }
        public int SortOrder { get; set; }
        public bool IsActive { get;  set; }
        public string IconName { get;  set; }
        public string NavigationScreen { get; set; }
        // Factory Method for Creation
        public static ServiceCategory Create(string nameEn, string nameAr, string iconName, bool isActive, string navigationScreen , int sortOrder = 0)
        {
            if (string.IsNullOrWhiteSpace(nameEn)) throw new ArgumentException("English name cannot be empty.", nameof(nameEn));
            if (string.IsNullOrWhiteSpace(nameAr)) throw new ArgumentException("Arabic name cannot be empty.", nameof(nameAr));

            return new ServiceCategory
            {
                NameEN = nameEn,
                NameAR = nameAr,
                IconName = iconName,
                IsActive = isActive,
                SortOrder = sortOrder,
                NavigationScreen = navigationScreen
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