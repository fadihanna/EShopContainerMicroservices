namespace Magic.Domain.Models.Lookups
{
    public class InternalErrorCodeLookup : LookUpBase<int>
    {
        public int ErrorCode { get; set; }
        public string? MessageEN { get; set; }
        public string? MessageAR { get; set; }
        public string? Description { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
