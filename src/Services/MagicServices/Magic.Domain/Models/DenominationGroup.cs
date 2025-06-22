namespace Magic.Domain.Models
{
    public class DenominationGroup : LookUpBase<int>
    {
        // Properties
        public int SortOrder { get;  set; }
        public bool IsInquiryRequired { get;   set; }
        public int ServiceId { get;   set; }
        public Service Service { get; private set; } = default!;

        // Navigation property for related Denominations
        public ICollection<Denomination> Denominations { get; private set; } = new List<Denomination>();

        // Factory Method for Creation
        public static DenominationGroup Create(
            string nameEN,
            string nameAR,
            int sortOrder,
            bool isInquiryRequired,
            int serviceId, 
            bool isActive)
        {
            if (string.IsNullOrWhiteSpace(nameEN))
                throw new ArgumentException("English name cannot be empty.", nameof(nameEN));

            if (string.IsNullOrWhiteSpace(nameAR))
                throw new ArgumentException("Arabic name cannot be empty.", nameof(nameAR));

            return new DenominationGroup
            {
                NameEN = nameEN,
                NameAR = nameAR,
                SortOrder = sortOrder,
                IsInquiryRequired = isInquiryRequired,
                ServiceId = serviceId,
            };
        }

        // Method for Updating
        public void Update(
            string nameEN,
            string nameAR,
            int sortOrder,
            bool isInquiryRequired,
            int serviceId)
        {
            if (string.IsNullOrWhiteSpace(nameEN))
                throw new ArgumentException("English name cannot be empty.", nameof(nameEN));

            if (string.IsNullOrWhiteSpace(nameAR))
                throw new ArgumentException("Arabic name cannot be empty.", nameof(nameAR));

            NameEN = nameEN;
            NameAR = nameAR;
            SortOrder = sortOrder;
            IsInquiryRequired = isInquiryRequired;
            ServiceId = serviceId;
        }
    }
}
