namespace Provider.Application.Services.Damen.Models
{
    public class DamenPaymentRequest
    {
        public string ser_id { get; set; }
        public string request_type { get; set; }
        public string request_name { get; set; }
        public Dictionary<string, string> serviceDataFileds { get; set; }
        public string inquire_id { get; set; }
        public string entity_transaction_id { get; set; }
    }
}

