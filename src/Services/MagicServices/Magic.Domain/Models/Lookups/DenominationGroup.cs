namespace Magic.Domain.Models.Lookups
{
    public class DenominationGroup : LookUpBase<int>
    {
        public int SortOrder { get; set; }
        public bool IsInquiryRequired { get; set; }

    }
}
