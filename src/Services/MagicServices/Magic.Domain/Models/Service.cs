namespace Magic.Domain.Models
{
    public class Service : Entity<int>
    {
        // Properties
        public string NameEN { get;  set; }
        public string NameAR { get;  set; }
        public int SortOrder { get;  set; }
        public bool IsActive { get;  set; }
        public int ServiceCategoryId { get;  set; }
        public ServiceCategory ServiceCategory { get; set; }
        public string IconName { get; set; }
        // Factory Method for Creation
        public static Service Create(string nameEn, string nameAr,string iconName ,bool isActive, int sortOrder = 0, int serviceCategoryId = 0)
        {
            if (string.IsNullOrWhiteSpace(nameEn)) throw new ArgumentException("English name cannot be empty.", nameof(nameEn));
            if (string.IsNullOrWhiteSpace(nameAr)) throw new ArgumentException("Arabic name cannot be empty.", nameof(nameAr));

            return new Service
            {
                NameEN = nameEn,
                NameAR = nameAr,
                IsActive = isActive,
                SortOrder = sortOrder,
                ServiceCategoryId = serviceCategoryId,
                IconName = iconName
            };
        }

        // Method for Updating
        public void Update(string nameEn, string nameAr,string iconName,bool isActive, int sortOrder)
        {
            if (string.IsNullOrWhiteSpace(nameEn)) throw new ArgumentException("English name cannot be empty.", nameof(nameEn));
            if (string.IsNullOrWhiteSpace(nameAr)) throw new ArgumentException("Arabic name cannot be empty.", nameof(nameAr));
            if (string.IsNullOrWhiteSpace(iconName)) throw new ArgumentException("Icon name cannot be empty.", nameof(iconName));

            NameEN = nameEn;
            NameAR = nameAr;
            IsActive = isActive;
            SortOrder = sortOrder;
            IconName = iconName;
        }
    }
}