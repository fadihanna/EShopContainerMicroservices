namespace Provider.Domain.Models
{
    public class MasaryService
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }    
        public int ProviderId { get; set; }
        public string Name { get; set; }
        public string NameAr { get; set; }
        public string PriceType { get; set; }
        public double MinValue { get; set; }
        public double MaxValue { get; set; }
        public int SortOrder { get; set; }
        public bool InquiryRequired { get; set; }
        public ICollection<MasaryServiceCharge> ServiceCharges { get; set; } = new List<MasaryServiceCharge>();

        public ICollection<MasaryServiceParameter> ServiceParameter { get; set; } = new List<MasaryServiceParameter>();

    }
}
