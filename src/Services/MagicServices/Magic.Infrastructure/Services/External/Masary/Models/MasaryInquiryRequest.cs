namespace Magic.Infrastructure.Services.External.Masary.Models
{
    public class MasaryInquiryRequest
    {
        public string Lang { get; set; }
        public int ServiceId { get; set; }
        public double Amount { get; set; }
        public int? Quantity { get; set; }
        public string ParameterInput { get; set; }
        public string ExternalId { get; set; }
        public bool IsPayAfterInquire { get; set; }
    }
}
