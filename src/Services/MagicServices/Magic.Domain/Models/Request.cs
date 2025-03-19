namespace Magic.Domain.Models
{
    public class Request : Entity<int>
    {
        public string? UserId { get; set; }
        public int? Status { get; set; }
        public DateTime? RequestDate { get; set; }
        public DateTime? ResponseDate { get; set; }
        public decimal? Amount { get; set; }
        public string? BillingAccount { get; set; }
        public string? ProviderTransactionId { get; set; }
        public int? DenominationId { get; set; }
    }
}
