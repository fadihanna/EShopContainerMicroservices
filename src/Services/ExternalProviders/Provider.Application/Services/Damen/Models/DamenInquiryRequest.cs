namespace Provider.Application.Services.Damen.Models
{
    public class DamenInquiryRequest
    {
        public string ser_id { get; set; }
        public string request_type { get; set; }
        public string request_name { get; set; }
        public Dictionary<string, string> serviceDataFileds { get; set; }
    }


}
